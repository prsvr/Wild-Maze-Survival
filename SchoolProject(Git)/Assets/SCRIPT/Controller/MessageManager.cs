using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public GameObject btn_Y, btn_N, btn_OK;
    public Button Yes, NO, OK;

    public LoadingScreen loadingScreen;
    public GameObject loadobj;

    public enum Status
    {
        None,
        Hint,
        Camp,
        Stage
    };
    public Status myStatus = new Status();

    void Start()
    {
        btn_Y.SetActive(false);
        btn_N.SetActive(false);
        btn_OK.SetActive(false);

        Yes.GetComponent<Button>();
        NO.GetComponent<Button>();
        OK.GetComponent<Button>();
    }

    void Update ()
    {
		if(myStatus == Status.Hint)
        {
            myStatus = Status.None;
            SetBunntonConfirm();
            OK.onClick.AddListener(ToCamp);
        }
        else if (myStatus == Status.Camp)
        {
            myStatus = Status.None;
            SetButtonChoice();
            Yes.onClick.AddListener(ToCamp);
        }
        else if (myStatus == Status.Stage)
        {
            myStatus = Status.None;
            SetButtonChoice();
            Yes.onClick.AddListener(ToStage);
        }
    }

    public void ToCamp()
    {
        //Yes.onClick.RemoveListener(ToCamp);
        loadingScreen.StartClick(loadobj);
    }

    public void ToStage()
    {
        //Yes.onClick.RemoveListener(ToCamp);
        loadingScreen.StartClick(loadobj);
    }

    void SetBunntonConfirm()
    {
        btn_OK.SetActive(true);
        btn_Y.SetActive(false);
        btn_N.SetActive(false);
    }

    void SetButtonChoice()
    {
        btn_Y.SetActive(true);
        btn_N.SetActive(true);
        btn_OK.SetActive(false);
    }
}
