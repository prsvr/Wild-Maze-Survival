using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string itemName;
    public int itemID;
    public string itemDesc;
}

[System.Serializable]

public class ConsumableItem : Item
{
    public int count;
    public int valueType;
    public int value;
}

[System.Serializable]

public class MaterialItem : Item
{
    public int maxStack;
}

[System.Serializable]

public class ToolItem : Item
{
    public int count;
}