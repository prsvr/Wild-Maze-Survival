using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public Queue<string> SentencesLog;  //存放所有對話的queue

    public Text DialogueText;
    public Animator animator;
    public bool dialogueEnd = false;

	void Start () {
        SentencesLog = new Queue<string>();
	}

    //開始對話
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);   //對話框顯示動畫
        dialogueEnd = false;
        SentencesLog.Clear();

        //將DialgueTrigger中的對話全部載入Queue中
        foreach(string sentences_load in dialogue.sentences)
        {
            SentencesLog.Enqueue(sentences_load);
        }

        NextDialogue();
    }
	
    //顯示下一個對話
    public void NextDialogue()
    {
        if(SentencesLog.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentnce_now = SentencesLog.Dequeue();  //將Queue的對話取一句出來放入sentence_now
        StopAllCoroutines();                          //避免重複使用到Coroutine  
        StartCoroutine(TypeEffect(sentnce_now));
    }

    //打字機效果
    IEnumerator TypeEffect (string sentence_now)
    {
        DialogueText.text = "";
        foreach(char letter in sentence_now.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;              //每個字間等待一個frame
        }
    }

    //結束對話
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);  //對話框關閉動畫
        dialogueEnd = true;
    }
}
