using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{
    //GUI用變數
    public float map_size = 1; //地圖的scale大小
    public float block_interval = 5.0f; //grid點之間的間距

    public string[] block_options = new string[] { "開始", "直線", "L型", "T型", "十型", "裝飾" };
    public int block_index = 0;

    public string[] block_options2 = new string[] { "0度", "90度", "180度", "270度" };
    public int block_index2 = 0;

    public string[] group_options = new string[] { "一般樹林", "松樹林", "灌木", "竹林"};
    public int group_index = 0;

    public string[] controller_options = new string[] { "玩家", "移動方向偵測器" };
    public int controller_index = 0;

    private float block_scale = 0;
    private bool Placeable = true;

    //其他變數
    static MapEditor map_editor;
    SceneView.OnSceneFunc onSceneFunc;
    Level_Editor_Grid grid;
    Editor map_editor_preview = null;

    GameObject map_floor = null;
    GameObject A, B, C, D, E, F;

    Texture2D PreviewTex;

    //////////////////////////////////////////////////【程式本體】/////////////////////////////////////////////////

    [MenuItem("Window/Map Editor")]
    public static void ShowWindow()
    {
        if (map_editor == null)
        {
            map_editor = EditorWindow.GetWindow(typeof(MapEditor)) as MapEditor;
            map_editor.onSceneFunc = new SceneView.OnSceneFunc(OnSceneView);
            SceneView.onSceneGUIDelegate += map_editor.onSceneFunc;
        }
    }

    static public void OnSceneView(SceneView sceneView)
    {
        map_editor.SceneObjectPlacer(sceneView);
    }

    //GUI顯示內容的部分
    void OnGUI()
    {
        ////////////////////地圖的屬性////////////////////
        GUILayout.Label("地圖屬性", EditorStyles.boldLabel);

        map_size = EditorGUILayout.IntSlider("地圖尺寸", (int)map_size, 1, 10);
        //block_interval = EditorGUILayout.IntSlider("Block Interval", (int)block_interval, 1, 10);

        if (GUILayout.Button("產生地圖"))
        {
            GenerateMap();
        }

        if (GUILayout.Button("移除整張地圖"))
        {
            RemoveMap();
        }



        /////////////////////圖塊的屬性////////////////////
        GUILayout.Label("圖塊屬性", EditorStyles.boldLabel);
        Placeable = EditorGUILayout.Toggle("啟用點擊放置", Placeable);
        group_index = EditorGUILayout.Popup("圖塊種類", group_index, group_options);
        block_index = EditorGUILayout.Popup("道路種類",block_index, block_options);
        block_index2 = EditorGUILayout.Popup("圖塊角度", block_index2, block_options2);

        string path_temp;

        switch(group_index)//選擇則不同地形
        {
            case 0:
                path_temp = "Assets/PREFAB/地圖/一般樹林/";
                break;
            case 1:
                path_temp = "Assets/PREFAB/地圖/松樹林/";
                break;
            case 2:
                path_temp = "Assets/PREFAB/地圖/灌木/";
                break;
            case 3:
                path_temp = "Assets/PREFAB/地圖/竹林/";
                break;
            default:
                path_temp = "Assets/PREFAB/地圖/一般樹林/";
                break;
        }

        switch (block_index)//生成該地形不同道路的預覽圖
        {
            case 0:
                PreviewTex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath("Assets/PREFAB/map/水.prefab", typeof(GameObject)));
                break;
            case 1:
                PreviewTex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath(path_temp + "一.prefab", typeof(GameObject)));
                break;
            case 2:
                PreviewTex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath(path_temp + "L.prefab", typeof(GameObject)));
                break;
            case 3:
                PreviewTex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath(path_temp + "T.prefab", typeof(GameObject)));
                break;
            case 4:
                PreviewTex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath(path_temp + "十.prefab", typeof(GameObject)));
                break;
            case 5:
                PreviewTex = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath(path_temp + "裝飾.prefab", typeof(GameObject)));
                break;
        }

        EditorGUILayout.BeginVertical();
        GUILayout.SelectionGrid(0, new Texture2D[] { PreviewTex }, 1);
        //GUILayout.Box(PreviewTex);
        EditorGUILayout.EndVertical();



        /////////////////////控制器的屬性////////////////////
        //GUILayout.Label("控制器屬性", EditorStyles.boldLabel);
        //controller_index = EditorGUILayout.Popup("控制器種類", controller_index, controller_options);


    }

    //////////////////////////////////////////////////【功能函式】/////////////////////////////////////////////////

    //產生地圖基底
    void GenerateMap ()
    {
        float floor_pos = (map_size * 10) / 2;
        float floor_scale = map_size + 0.5f;

        if (map_floor == null)
        {
            //地圖基底
            map_floor = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/PREFAB/map/MapFloor.prefab", typeof(GameObject))) as GameObject;
            map_floor.transform.position = new Vector3(floor_pos, 0, floor_pos);
            map_floor.transform.localScale = new Vector3(floor_scale, floor_scale, floor_scale);
            map_floor.gameObject.tag = "floor";
            map_floor.AddComponent(typeof(Level_Editor_Grid));
            grid = map_floor.GetComponent<Level_Editor_Grid>();
            grid.grid_size = map_size * 10;
            grid.size = block_interval;
        }
    }

    //刪除地圖基底
    void RemoveMap()
    {
        if (map_floor != null)
        {
            DestroyImmediate(GameObject.Find("MapFloor"), false);
            map_floor = null;
        }      
    }

    //物件放置器，將物件放置在滑鼠點擊處距離最近的gird座標點
    void PlaceObjectNear(Vector3 click_point)
    {
        string path_temp;
        var place_position = grid.GetNearestPoint(click_point);
        int placed_index = grid.placed_point.IndexOf(place_position); //獲取當前的點在placed_point陣列中的index

        block_scale = 1 / ( (map_size+0.5f)*2 ); //圖塊是floor的child，要去除掉floor的scale的影響，把圖塊的scale從local scale還原成world scale

        if (grid.placed_point_bool[placed_index] == false)
        {
            grid.placed_point_bool[placed_index] = true; //避免重複放置圖塊

            switch (group_index)//偵測選擇的地形種類
            {
                case 0:
                    path_temp = "Assets/PREFAB/地圖/一般樹林/";
                    break;
                case 1:
                    path_temp = "Assets/PREFAB/地圖/松樹林/";
                    break;
                case 2:
                    path_temp = "Assets/PREFAB/地圖/灌木/";
                    break;
                case 3:
                    path_temp = "Assets/PREFAB/地圖/竹林/";
                    break;
                default:
                    path_temp = "Assets/PREFAB/地圖/一般樹林/";
                    break;
            }


            switch (block_index) //y + 0.1f的用意是不要讓圖塊跟floor過度貼合而破圖
            {
                case 0:
                    A = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/PREFAB/map/水.prefab", typeof(GameObject))) as GameObject;
                    SetBlockProperty(A, place_position);
                    break;
                case 1:
                    B = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path_temp + "一.prefab", typeof(GameObject))) as GameObject;
                    SetBlockProperty(B, place_position);
                    break;
                case 2:
                    C = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path_temp + "L.prefab", typeof(GameObject))) as GameObject;
                    SetBlockProperty(C, place_position);
                    break;
                case 3:
                    D = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path_temp + "T.prefab", typeof(GameObject))) as GameObject;
                    SetBlockProperty(D, place_position);
                    break;
                case 4:
                    E = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path_temp + "十.prefab", typeof(GameObject))) as GameObject;
                    SetBlockProperty(E, place_position);
                    break;
                case 5:
                    F = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path_temp + "裝飾.prefab", typeof(GameObject))) as GameObject;
                    SetBlockProperty(F, place_position);
                    break;
            }
        }
    }

    void SetBlockProperty(GameObject obj, Vector3 pos)
    {
        obj.transform.position = pos + new Vector3(0, 0.1f, 0);
        obj.transform.parent = map_floor.transform;
        obj.transform.localScale = new Vector3(block_scale, block_scale, block_scale);
        obj.gameObject.tag = "block";

        //調整角度
        switch (block_index2)
        {
            case 0:
                obj.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                obj.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case 2:
                obj.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 3:
                obj.transform.eulerAngles = new Vector3(0, 270, 0); ;
                break;
        }
    }

    //場景監控器（只有此區段會不斷監聽 Scene 中的動作）
    void SceneObjectPlacer(SceneView sceneView)
    {
        Camera cameara = sceneView.camera;
        GameObject obj = Selection.activeGameObject;

        //取得滑鼠射線終點的資訊，供物件放置器（PlaceObjectNear）使用
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            RaycastHit hit_info; //儲存回傳射線終點的資訊用。
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition); //將螢幕座標轉換成世界座標存於此
            
            if (Physics.Raycast(ray, out hit_info) && Placeable == true)
            {
                if (hit_info.collider.tag == "floor") PlaceObjectNear(hit_info.point);
            }
        }

        //監控當前選擇物件的存在情況
        if (obj != null && obj.tag == "block" && obj.tag != "floor")
        {
            if (Event.current.commandName == "SoftDelete")
            {
                //告知grid已刪除選擇圖塊，並更改該grid點的placed_point_bool值，使其允許再次放置其他圖塊
                //這邊這麼迂迴是因為GetNearestPoint抓到的點座標，看似與obj.transform.position相同實則不同，故只好再呼叫GetNearestPoint處理
                Vector3 click_position = grid.GetNearestPoint(obj.transform.position - new Vector3(0, 0.1f, 0)); 
                int placed_index = grid.placed_point.IndexOf(click_position);

                grid.placed_point_bool[placed_index] = false;
            }
        }

        
    }

}
