using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase : MonoBehaviour
{
    public List<Recipe> recipes;

	void Awake ()
    {
        foreach (TextAsset f in Resources.LoadAll("Recipes"))
        {
            recipes.AddRange(JsonUtility.FromJson<jsonData>(f.text).r);
        }
    }

    private class jsonData
    {
        public Recipe[] r;
    }
}
