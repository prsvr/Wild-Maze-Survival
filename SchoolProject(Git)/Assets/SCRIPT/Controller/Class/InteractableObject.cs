using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableObject
{

    public string name;

    public GameObject obj;
    public int ItemID;

    [TextArea]
    public string MessageBoxContent; //提示框內容
    public string SEName; // 互動音效

}

///以下三個class為分類方便而設置

[System.Serializable]
public class ConsumableObject : InteractableObject
{

    //public int NutritionValue; //補充多少飢餓值或者水分
    //public int HealValue;      //回復多少生命值
    //public int CanUseHowManyTime; //可以使用幾次

}

[System.Serializable]
public class MaterialObject : InteractableObject
{
    //public int MaxNumberPerStaack; //每一堆可以有幾個重複物件
}

[System.Serializable]
public class ToolObject : InteractableObject
{
    //public bool isConsumable;
    //public int CanUseHowManyTime; //可以使用幾次
}
