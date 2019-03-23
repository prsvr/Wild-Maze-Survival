using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public List<Quest> quests;

	void Awake()
    {
        foreach (TextAsset f in Resources.LoadAll("Quests"))
        {
            quests.AddRange(JsonUtility.FromJson<jsonData>(f.text).q);
        }
    }

    private class jsonData
    {
        public Quest[] q;
    }
}
