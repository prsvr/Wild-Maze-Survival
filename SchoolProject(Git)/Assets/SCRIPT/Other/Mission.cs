using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public List<Quest> missions;
    public List<bool> completed;
    public GUISkin skin;
    public QuestDatabase questDatabase;
    Vector2 scroll;
    bool showDetail;
    Quest selected;

    void Start ()
    {
        questDatabase = FindObjectOfType<QuestDatabase>();
        AddMission(1);
        AddMission(2);
        AddMission(3);
        AddMission(4);
        AddMission(5);
        CompleteMission(3);
        CompleteMission(4);
    }

    void OnGUI ()
    {
        GUI.skin = skin;
        scroll = GUI.BeginScrollView(new Rect(Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.45f, Screen.height * 0.5f), scroll, new Rect(Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.9f));
        for (int i = missions.Count - 1; i >= 0; i--)
        {
            Rect rect = new Rect(Screen.width * 0.3f, Screen.height * (0.3f + (missions.Count - 1 - i) * 0.11f), Screen.width * 0.4f, Screen.height * 0.1f);
            if (GUI.Button(rect, "  #" + missions[i].questID + "  " + missions[i].questName + (completed[i] ? "   (已完成)" : "")))
            {
                showDetail = true;
                selected = missions[i];
            }
        }
        if (showDetail)
        {
            GUI.Window(0, new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.52f, Screen.height * 0.57f), DoWindow, "");
        }
        GUI.EndScrollView();
    }

    void DoWindow (int id)
    {
        GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.39f, 100), "Name :  " + selected.questName);
        GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.14f, Screen.width * 0.39f, 100), "Description :");
        GUI.Label(new Rect(Screen.width * 0.07f, Screen.height * 0.22f, Screen.width * 0.39f, 300), selected.questDesc);
        if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.48f, 70, 30), "BACK"))
        {
            showDetail = false;
        }
    }

    public void ClearDetail ()
    {
        showDetail = false;
        selected = new Quest();
    }

    public void AddMission (int id)
    {
        missions.Add(questDatabase.quests.Find(x => x.questID == id));
        completed.Add(false);
    }

    public void CompleteMission (int id)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i].questID == id)
            {
                completed[i] = true;
            }
        }
    }
}
