using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GlobalDialogueTrigger : MonoBehaviour { //根據state來播放已經設定好的對話

    public Dialogue[] dialogues;
    public DialogueState.State dialogueState;

    Dialogue d; //暫存用

    void Update()
    {
        switch(dialogueState)
        {
            case DialogueState.State.None:
                break;
            case DialogueState.State.Opening:
                d = Array.Find(dialogues, dialogue => dialogue.name == "Opening");
                TriggerDialogue(d);
                dialogueState = DialogueState.State.None;
                break;
            case DialogueState.State.Middle:
                d = Array.Find(dialogues, dialogue => dialogue.name == "Middle");
                TriggerDialogue(d);
                dialogueState = DialogueState.State.None;
                break;
            case DialogueState.State.End:
                d = Array.Find(dialogues, dialogue => dialogue.name == "End");
                TriggerDialogue(d);
                dialogueState = DialogueState.State.None;
                break;
        }
    }

    void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
