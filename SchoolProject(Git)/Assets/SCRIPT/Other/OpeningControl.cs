using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OpeningControl : MonoBehaviour {

    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;
    public LoadingScreen loadingScreen;
    public GameObject loadobj;
    bool start = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update () {

        if (start == false)
        {
            dialogueTrigger.TriggerDialogue();
            start = true;
        }

        if (dialogueManager.dialogueEnd == true) ToStage();
    }

    public void ToStage()
    {
        dialogueManager.dialogueEnd = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        loadingScreen.StartClick(loadobj);
    }

}
