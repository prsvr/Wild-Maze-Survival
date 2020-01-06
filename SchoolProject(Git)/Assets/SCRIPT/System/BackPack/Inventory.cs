using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotsX, slotsY;
    public GUISkin skin;
    public List<Item> inventory;   //背包內的所有格子
    public List<int> itemStack;   //格子內的堆疊數
    public List<int> countLeft;   //物品剩餘的使用次數
    public ItemDatabase itemDatabase;
    bool showDetail;
    Item selected;   //玩家所點選的物品
    Rect pos;   //玩家所選物品的畫面位置
    int index;   //玩家所選物品的資料庫位址

    public Player_Vital player_vital;
    public GameObject flashlight;
    bool flash_on;

    void Start ()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        inventory = GlobalManager.Instance.inventory;   //讀取玩家背包資料
        itemStack = GlobalManager.Instance.itemStack;
        countLeft = GlobalManager.Instance.countLeft;

        flash_on = false;
    }

    void OnDisable()
    {
        GlobalManager.Instance.inventory = inventory;   //儲存玩家背包資料
    }

    void OnGUI ()
    {        
        GUI.skin = skin;
        DrawInventory();
        if (showDetail)
        {
            GUI.Label(new Rect(Screen.width * 0.555f, Screen.height * 0.56f, Screen.width * 0.2f, Screen.height * 0.17f), "<" + selected.itemName + ">\n" + selected.itemDesc);
            GUI.DrawTexture(new Rect(Screen.width * 0.56f, Screen.height * 0.135f, Screen.width * 0.18f, Screen.height * 0.34f), Resources.Load<Texture2D>("Icons/" + selected.itemName), ScaleMode.ScaleToFit);
            GUI.Box(pos, "", skin.GetStyle("box"));
            if (selected.itemName == "水瓶")
            {
                GUI.Label(new Rect(Screen.width * 0.555f, Screen.height * 0.655f, Screen.width * 0.18f, Screen.height * 0.17f), "剩餘量：" + countLeft[index] * 20 + "%");
            }
        }
    }

    public void ClearDetail ()
    {
        showDetail = false;
        selected = new Item();
    }

    public void UseSelected()
    {
        ////////測試//////////
        if (selected is ConsumableItem)
        {
            ConsumableItem c = (ConsumableItem)selected;
            switch (c.valueType)
            {
                case 1:
                    player_vital.Heal(c.value);
                    break;
                case 2:
                    player_vital.Eat(c.value);
                    break;
                case 3:
                    player_vital.Drink(c.value);
                    break;
            }
        }
        if (selected.itemName == "手電筒")
        {
            if (flash_on == false)
            {
                flashlight.SetActive(true);
                flash_on = true;
            }
            else
            {
                flashlight.SetActive(false);
                flash_on = false;
            }
        }
        countLeft[index]--;
        if (countLeft[index] == 0)
        {
            DropSelected();
        }
        ////////測試//////////
    }

    public void DropSelected ()   //所選物品用完或是被丟掉
    {
        if (showDetail)
        {
            inventory[index] = new Item();
            if (selected.itemName == "水瓶")   //水瓶喝完或倒掉之後會變成裝水器具
            {
                inventory[index] = itemDatabase.items.Find(x => x.itemName == "空水瓶");
                countLeft[index] = ((ToolItem)inventory[index]).count;
                return;
            }
            itemStack[index] = 0;
            countLeft[index] = -1;
        }
    }

    public void AddItem (int id)   //某物品被裝進背包
    {
        Item a = GlobalManager.Instance.itemDatabase.items.Find(x => x.itemID == id);
        List<Item> inv = GlobalManager.Instance.inventory;
        List<int> stk = GlobalManager.Instance.itemStack;
        List<int> clf = GlobalManager.Instance.countLeft;

        if (a is MaterialItem)   //相同的材料可以堆疊
        {
            for (int i = 0; i < inv.Count; i++)
            {
                if (inv[i].itemName == a.itemName && stk[i] < ((MaterialItem)a).maxStack)
                {
                    stk[i]++;
                    return;
                }
            }
        }
        for (int i = 0; i < inv.Count; i++)
        {
            if (inv[i].itemName == null)
            {
                if (a is ConsumableItem)
                {
                    clf[i] = ((ConsumableItem)a).count;
                }
                if (a is ToolItem)
                {
                    clf[i] = ((ToolItem)a).count;
                }
                inv[i] = a;
                stk[i]++;
                break;
            }
        }
    }

    public void RemoveItem (string name, int qty)   //消耗固定數量的某物品
    {
        List<Item> inv = GlobalManager.Instance.inventory;
        List<int> stk = GlobalManager.Instance.itemStack;
        List<int> clf = GlobalManager.Instance.countLeft;

        for (int i = 0; i < inv.Count && qty > 0; i++)
        {
            while (inv[i].itemName == name && qty > 0)
            {
                if (inv[i] is MaterialItem)
                {
                    stk[i]--;
                    if (stk[i] == 0)
                    {
                        inv[i] = new Item();
                    }
                }
                else
                {
                    clf[i]--;
                    if (clf[i] == 0)
                    {
                        inv[i] = new Item();
                    }
                }
                qty--;
            }
        }
    }

    void DrawInventory ()   //印出背包內的所有格子
    {
        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {            
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(Screen.width * 0.27f + x * Screen.width * 0.08f, Screen.height * 0.14f + y * Screen.height * 0.15f, Screen.height * 0.12f, Screen.height * 0.12f);
                GUI.Box(slotRect, "", skin.GetStyle("slot"));
                if(inventory[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, Resources.Load<Texture2D>("Icons/" + inventory[i].itemName), ScaleMode.ScaleToFit);
                    if (itemStack[i] > 1)
                    {
                        Rect stkRect = new Rect(slotRect.x + Screen.height * 0.05f, slotRect.y + Screen.height * 0.07f, slotRect.width * 0.5f, slotRect.height * 0.5f);
                        GUI.Label(stkRect, itemStack[i].ToString(), skin.GetStyle("stk"));
                    }
                    if (slotRect.Contains(Event.current.mousePosition) && Input.GetMouseButton(0))   //當玩家點選其中一個格子時
                    {
                        selected = inventory[i];
                        pos = slotRect;
                        index = i;
                        showDetail = true;
                    }                    
                }
                i++;
            }
        }
	}
}
