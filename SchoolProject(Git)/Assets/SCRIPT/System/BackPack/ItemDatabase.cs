using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items;

    void Awake ()
    {
        foreach (TextAsset f in Resources.LoadAll("Items/Consumable"))   //一次將一個txt檔內的多個物品資料寫入資料庫
        {
            items.AddRange(JsonUtility.FromJson<jsonData>(f.text).c);
        }
        foreach (TextAsset f in Resources.LoadAll("Items/Material"))
        {
            items.AddRange(JsonUtility.FromJson<jsonData>(f.text).m);
        }
        foreach (TextAsset f in Resources.LoadAll("Items/Tool"))
        {
            items.AddRange(JsonUtility.FromJson<jsonData>(f.text).t);
        }
    }

    private class jsonData   //txt檔內的陣列命名規則
    {
        public ConsumableItem[] c;
        public MaterialItem[] m;
        public ToolItem[] t;
    }
}
