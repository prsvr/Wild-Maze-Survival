using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Level_Editor_Grid : MonoBehaviour {

    public float size = 2f;
    public float grid_size = 20;

    //確認該點是否已有放置物件
    public List<Vector3> placed_point = new List<Vector3>(); 
    public List<bool> placed_point_bool = new List<bool>();

    //用以取得在grid上最接近滑鼠點擊的點。
    public Vector3 GetNearestPoint(Vector3 position)
    {
        //將滑鼠點擊位置減去grid物件的座標，取得其在gird上的相對位置(offset)。
        //用意是在避免gird物件移動後，放置在上的物件座標會跑掉。
        position -= transform.position;

        //跟據size大小來決定點的數量並取整數(size越小點越多)。
        int count_x = Mathf.RoundToInt(position.x / size);
        int count_y = Mathf.RoundToInt(position.y / size);
        int count_z = Mathf.RoundToInt(position.z / size);

        //乘回size取的處理後的座標。
        Vector3 result = new Vector3(
            (float)count_x * size,
            (float)count_y * size,
            (float)count_z * size
        );

        //將相對的座標還原回實際的世界座標。
        result += transform.position;

        return result;
    }

    //將grid中的座標點可視化。
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(float x = 0; x < grid_size + size; x += size)
        {
            for (float z = 0; z < grid_size + size; z += size)
            {
                var point = GetNearestPoint(new Vector3(x, 0f, z));
                placed_point.Add(point);
                placed_point_bool.Add(false);
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
