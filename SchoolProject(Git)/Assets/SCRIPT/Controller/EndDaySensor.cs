using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDaySensor : MonoBehaviour {

    public DayAndNightControl dayAndNight;
    public GameObject message;
    public Text messageText;

    bool finished;

    // Use this for initialization
    void Start () {
        finished = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(dayAndNight.currentTime > 0.85f && dayAndNight.currentTime < 1f && finished == false)
        {
            finished = true;
            ToCamp();
        }
	}

    void ToCamp()
    {
        message.SetActive(true);
        MessageManager msg_status = message.GetComponent<MessageManager>();
        msg_status.myStatus = MessageManager.Status.Hint;

        messageText.text = "時間太晚，返回營地休息。";
    }
}
