using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public int slotsX, slotsY;
    public GUISkin skin;
    public GameObject backpack;
    public Inventory inventory;
    public List<Recipe> merge;
    public RecipeDatabase recipeDatabase;
    bool showDetail;
    Recipe selected;
    Rect pos;
    bool isSufficient;

    void Start ()
    {
        recipeDatabase = FindObjectOfType<RecipeDatabase>();
        inventory = backpack.GetComponent<Inventory>();
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            merge.Add(new Recipe());
        }
        AddRecipe(4);
    }

    void OnGUI ()
    {
        GUI.skin = skin;
        DrawMerge();
        if (showDetail)
        {
            GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.52f, Screen.width * 0.18f, Screen.height * 0.17f), "<" + selected.recipeName + ">\n" + selected.recipeDesc);
            GUI.DrawTexture(new Rect(Screen.width * 0.56f, Screen.height * 0.24f, Screen.width * 0.18f, Screen.height * 0.34f), Resources.Load<Texture2D>("Icons/" + selected.recipeName), ScaleMode.ScaleToFit);
            GUI.Box(pos, "", skin.GetStyle("box"));
            DrawIngredients();
        }
    }

    public void ClearDetail ()
    {
        showDetail = false;
        selected = new Recipe();
    }

    public void MergeItem ()
    {
        if (showDetail && isSufficient)
        {
            for (int i = 0; i < selected.ingredients.Length; i++)
            {
                inventory.RemoveItem(selected.ingredients[i], selected.stack[i]);
            }
            inventory.AddItem(selected.recipeID + 100);
            inventory.ClearDetail();
        }
    }

    public void AddRecipe (int id)
    {
        for (int i = 0; i < merge.Count; i++)
        {
            if (merge[i].recipeName == null)
            {
                merge[i] = recipeDatabase.recipes.Find(x => x.recipeID == id);
                break;
            }
        }
        
    }

    void DrawMerge ()
    {
        int i = 0;

        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(Screen.width * 0.29f + x * Screen.width * 0.1f, Screen.height * 0.13f + y * Screen.height * 0.16f, Screen.height * 0.15f, Screen.height * 0.15f);
                GUI.Box(slotRect, "", skin.GetStyle("slot"));
                if (merge[i].recipeName != null)
                {
                    GUI.DrawTexture(slotRect, Resources.Load<Texture2D>("Icons/" + merge[i].recipeName), ScaleMode.ScaleToFit);
                    if (slotRect.Contains(Event.current.mousePosition) && Input.GetMouseButton(0))
                    {
                        selected = merge[i];
                        pos = slotRect;
                        showDetail = true;
                    }
                }
                i++;
            }
        }
    }

    void DrawIngredients ()
    {
        bool[] check = new bool[inventory.inventory.Count];
        int found;
        isSufficient = true;

        for (int i = 0; i < selected.ingredients.Length; i++)
        {
            found = 0;
            Rect slotRect = new Rect(Screen.width * 0.4f + i * Screen.width * 0.06f, Screen.height * 0.74f, Screen.height * 0.1f, Screen.height * 0.1f);
            GUI.Box(slotRect, "", skin.GetStyle("slot"));
            for (int j = 0; j < inventory.inventory.Count; j++)
            {
                if (inventory.inventory[j].itemName == selected.ingredients[i] && !check[j])
                {
                    found += inventory.itemStack[j];
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
                Rect stkRect = new Rect(slotRect.x + Screen.height * 0.04f, slotRect.y + Screen.height * 0.06f, slotRect.width * 0.5f, slotRect.height * 0.5f);
                GUI.Label(stkRect, selected.stack[i].ToString(), skin.GetStyle("stk"));
            }
            GUI.color = new Color(255, 255, 255);
        }
    }
}
