using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用Singleton的Design Pattern來實作全域變數
public class GlobalManager : MonoBehaviour {

    public static GlobalManager Instance { get; private set; } //讓此Instance只能透過內部修改

    //用於Player Vital的變數
    public int Health, Hunger, Thirst;

    //用於計算天數
    public int Day;

    //用於處理背包內容物繪製
    public List<Item> inventory; 
    public ItemDatabase itemDatabase;
    public List<int> itemStack;
    public List<int> countLeft;

    //用於紀錄營地建造
    public bool Campfire, Shelter;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Day = 1;

            InitializeVital();
            InitializeInventory();
            InitializeCamp();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    /*-------------------【Player Vital】-------------------*/
    void InitializeVital()
    {
        Health = 100;
        Hunger = 100;
        Thirst = 100;
    }
    /*-------------------【Player Vital】-------------------*/


    /*-------------------【Inventory】-------------------*/
    void InitializeInventory()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        for (int i = 0; i < (3 * 5); i++)
        {
            inventory.Add(new Item());
            itemStack.Add(0);
            countLeft.Add(-1);
        }
        AddItem(7);
        AddItem(1);
        AddItem(20);
        AddItem(12);
        AddItem(2);
        for (int j = 0; j < 130; j++)
        {
            AddItem(7);
            AddItem(6);
        }
        for (int j = 0; j < 30; j++)
        {
            AddItem(8);
            AddItem(9);
        }
        AddItem(4);
        AddItem(11);
    }

    void AddItem(int id)
    {
        Item a = itemDatabase.items.Find(x => x.itemID == id);

        if (a is MaterialItem)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemName == a.itemName && itemStack[i] < ((MaterialItem)a).maxStack)
                {
                    itemStack[i]++;
                    return;
                }
            }
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                if (a is ConsumableItem)
                {
                    countLeft[i] = ((ConsumableItem)a).count;
                }
                if (a is ToolItem)
                {
                    countLeft[i] = ((ToolItem)a).count;
                }
                inventory[i] = a;
                itemStack[i]++;
                break;
            }
        }
    }
    /*-------------------【Inventory】-------------------*/


    /*-------------------【Camp】-------------------*/
    void InitializeCamp()
    {
        Campfire = false;
        Shelter = false;
    }
    /*-------------------【Camp】-------------------*/


}
