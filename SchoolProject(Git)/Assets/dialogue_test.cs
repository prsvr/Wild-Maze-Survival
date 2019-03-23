using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_test : MonoBehaviour {

    public DialogueTrigger dialogueTrigger;

	void Start () {
        dialogueTrigger.TriggerDialogue();
    }

}
