using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ExitCamp : MonoBehaviour {

    public MessageManager messageManager;
    public GameObject message;
    public Text msg_text;

    public void exitCamp()
    {
        msg_text.text = "要休息並進入下一天嗎？";
        message.SetActive(true);
        messageManager.myStatus = MessageManager.Status.Stage;
    }

}
