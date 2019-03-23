using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GUISkin skin;
    public GameObject backpack;
    public List<Item> inventory;
    public List<int> itemStack;
    public RecipeDatabase recipeDatabase;
    public GameObject objects;
    bool showDetail;
    Recipe selected;
    bool isSufficient;

    void Awake ()
    {
        recipeDatabase = FindObjectOfType<RecipeDatabase>();
        inventory = GlobalManager.Instance.inventory;
        itemStack = GlobalManager.Instance.itemStack;
    }

    void OnGUI ()
    {
        GUI.skin = skin;
        if (showDetail)
        {
            GUI.Label(new Rect(Screen.width * 0.29f, Screen.height * 0.42f, Screen.width * 0.33f, Screen.height * 0.12f), "<" + selected.recipeName + ">\n" + selected.recipeDesc);
            GUI.DrawTexture(new Rect(Screen.width * 0.64f, Screen.height * 0.34f, Screen.width * 0.1f, Screen.height * 0.18f), Resources.Load<Texture2D>("Icons/" + selected.recipeName), ScaleMode.ScaleToFit);
            DrawIngredients();
        }
    }

    public void BuildSelected ()
    {
        if (showDetail && isSufficient)
        {
            for (int i = 0; i < selected.ingredients.Length; i++)
            {
                backpack.GetComponent<Inventory>().RemoveItem(selected.ingredients[i], selected.stack[i]);
            }

            if(selected.recipeName == "火堆" && GlobalManager.Instance.Campfire == false)
            {
                objects.transform.Find(selected.recipeName).gameObject.SetActive(true);
                GlobalManager.Instance.Campfire = true;
            }
            else if (selected.recipeName == "遮蔽所" && GlobalManager.Instance.Shelter == false)
            {
                objects.transform.Find(selected.recipeName).gameObject.SetActive(true);
                GlobalManager.Instance.Shelter = true;
            }

        }
    }

    public void Display (int id)
    {
        selected = recipeDatabase.recipes.Find(x => x.recipeID == id);
        showDetail = true;
    }

    public void ClearDetail ()
    {
        showDetail = false;
        selected = new Recipe();
    }

    void DrawIngredients ()
    {
        bool[] check = new bool[inventory.Count];
        int found;
        isSufficient = true;

        for (int i = 0; i < selected.ingredients.Length; i++)
        {
            found = 0;
            Rect slotRect = new Rect(Screen.width * 0.29f + i * Screen.width * 0.07f, Screen.height * 0.63f, Screen.height * 0.09f, Screen.height * 0.09f);
            GUI.Box(slotRect, "", skin.GetStyle("slot"));
            for (int j = 0; j < inventory.Count; j++)
            {
                if (inventory[j].itemName == selected.ingredients[i] && !check[j])
                {
                    found += itemStack[j];
                    check[j] = true;
                }
            }
            if (found < selected.stack[i])
            {
                GUI.color = new Color(255, 255, 255, 0.3f);
                isSufficient = false;
            }
            GUI.DrawTexture(slotRect, Resources.Load<Texture2D>("Icons/" + selected.ingredients[i]), ScaleMode.ScaleToFit);
            if (selected.stack[i] > 1)
            {
                Rect stkRect = new Rect(slotRect.x + Screen.height * 0.04f, slotRect.y + Screen.height * 0.05f, slotRect.width * 0.5f, slotRect.height * 0.5f);
                GUI.Label(stkRect, selected.stack[i].ToString(), skin.GetStyle("stk"));
            }
            GUI.color = new Color(255, 255, 255);
        }
    }
}
