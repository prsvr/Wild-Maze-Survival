using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSay : MonoBehaviour {

    GlobalDialogueTrigger globalDialogueTrigger;

    bool TalkFinished = false;

	void Start ()
    {
        TalkFinished = false;
        globalDialogueTrigger = FindObjectOfType<GlobalDialogueTrigger>();
    }
	
	void Update ()
    {
        PlayDialogueByDay(); //根據天數來觸發對話
    }

    void PlayDialogueByDay()
    {
        if (TalkFinished == false)
        {
            switch (GlobalManager.Instance.Day)
            {
                case 1:
                    globalDialogueTrigger.dialogueState = DialogueState.State.Opening;
                    TalkFinished = true;
                    break;
            }
        }
    }
}

