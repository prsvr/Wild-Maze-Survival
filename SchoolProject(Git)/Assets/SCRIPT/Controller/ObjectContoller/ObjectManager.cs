using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public AudioManager sound;
    public Inventory inventory;

    //不同物件的class，皆繼承自InteractableObject這個class
    public ConsumableObject[] consumableObjects;
    public MaterialObject[] materialObjects;
    public ToolObject[] toolObjects;

    //初始化列表中的物件
    void Awake() 
    {
        foreach(ConsumableObject Cobj in consumableObjects)
        {
            Cobj.obj.AddComponent<ObjectInteract>(); //掛上讓物件可以互動的腳本
            ObjectInteract cobji = Cobj.obj.GetComponent<ObjectInteract>();
            cobji.type = 1;                          //設定類型編號供判斷   
            Cobj.obj.tag = "InteractableObject";
        }
        foreach (MaterialObject Mobj in materialObjects)
        {
            Mobj.obj.AddComponent<ObjectInteract>(); //掛上讓物件可以互動的腳本
            ObjectInteract mobji = Mobj.obj.GetComponent<ObjectInteract>();
            mobji.type = 2;                          //設定類型編號供判斷 
            Mobj.obj.tag = "InteractableObject";
        }
        foreach (ToolObject Tobj in toolObjects)
        {
            Tobj.obj.AddComponent<ObjectInteract>(); //掛上讓物件可以互動的腳本
            ObjectInteract tobji = Tobj.obj.GetComponent<ObjectInteract>();
            tobji.type = 3;                           //設定類型編號供判斷 
            Tobj.obj.tag = "InteractableObject";
        }
    }
	
	void Start () {
        sound = FindObjectOfType<AudioManager>();
	}

    //此方法用於啟用或關閉物件
    public void SetActive(string name, int type, bool trigger)
    {
        switch(type)
        {
            case 1 :
                ConsumableObject c = Array.Find(consumableObjects, cobj => cobj.name == name);
                if (trigger == true) c.obj.SetActive(true);
                else
                {
                    sound.Play(c.SEName);
                    c.obj.SetActive(false);
                }
                break;
            case 2 :
                MaterialObject m = Array.Find(materialObjects, mobj => mobj.name == name);
                if (trigger == true) m.obj.SetActive(true);
                else
                {
                    sound.Play(m.SEName);
                    m.obj.SetActive(false);
                }
                break;
            case 3:
                ToolObject t = Array.Find(toolObjects, tobj => tobj.name == name);
                if (trigger == true) t.obj.SetActive(true);
                else
                {
                    sound.Play(t.SEName);
                    t.obj.SetActive(false);
                }
                break;
        }
        
    }

    //此方法用於將物品放入背包
    public void PickUp(string name, int type)
    {
        switch (type)
        {
            case 1:
                ConsumableObject c = Array.Find(consumableObjects, cobj => cobj.name == name);
                inventory.AddItem(c.ItemID);
                break;

            case 2:
                MaterialObject m = Array.Find(materialObjects, mobj => mobj.name == name);
                inventory.AddItem(m.ItemID);
                break;

            case 3:
                ToolObject t = Array.Find(toolObjects, tobj => tobj.name == name);
                inventory.AddItem(t.ItemID);
                break;
        }
        
    }



}
