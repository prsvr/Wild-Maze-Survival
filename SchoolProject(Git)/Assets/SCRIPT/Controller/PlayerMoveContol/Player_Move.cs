using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public Transform pos_now, pos_now_arrows;       //當前的位置
    public Vector3 Length;          //移動的固定距離
    public float transitionTime;    //總移動時間(影響lerp的速度)
    public bool trigger;            //是否開始移動的開關
    public GameObject arrows;       //道路的箭頭
    public GameObject x1,x2,z1,z2;  //道路的箭頭

    private Vector3 targetPosition, targetPosition_arrows;
    private Vector3 startPosition, startPosition_arrows;
    private float timeMove;         //當下移動的計時器
    private AudioSource se_step;

    private Player_Vital myVital;
    public DayAndNightControl day_cycle;

    private int toCamp;

    public GameObject message;
    public Text messageText;

    void Start()
    {
        se_step = GetComponent<AudioSource>();
        myVital = GetComponent<Player_Vital>();

        startPosition = pos_now.position;
        startPosition_arrows = pos_now_arrows.position;

        targetPosition = startPosition + Length;
        targetPosition_arrows = startPosition_arrows + Length;

        timeMove = 0.0f;

        DisableArrows();
    }

    void Update()
    {
        if (trigger == true)
        {
            if (Vector3.Distance(targetPosition, pos_now.position) > 0) // 確認距離目的地多遠才要停下來
            {
                timeMove += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, targetPosition, timeMove / transitionTime);
                arrows.transform.position = Vector3.Lerp(startPosition_arrows, targetPosition_arrows, timeMove / transitionTime);
                //Debug.Log(startPosition + " " + targetPosition);
            }
            else
            {
                trigger = false;
                timeMove = 0.0f;
                startPosition = transform.position;
                startPosition_arrows = arrows.transform.position;

                //Debug.Log(startPosition + " " + targetPosition);
            }
        }
    }

    //碰撞到方向偵測器時決定哪裡可以走
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DirectionSensor")
        {
            Debug.Log(startPosition);

            GameObject sensor = other.gameObject;
            DirectionSensor sensor_direction = sensor.GetComponent<DirectionSensor>();
            toCamp = sensor_direction.ToCamp;

            if (sensor_direction.z_plus == true)
                z2.SetActive(true);
            if (sensor_direction.z_minus == true)
                z1.SetActive(true);            
            if (sensor_direction.x_plus == true)
                x2.SetActive(true);
            if (sensor_direction.x_minus == true)
                x1.SetActive(true);
        }
    }

    void DisableArrows()
    { 
        z2.SetActive(false);
        z1.SetActive(false);
        x2.SetActive(false);
        x1.SetActive(false);
    }

    //供按鈕呼叫的函式，主要是準備移動和調整移動方向。
    public void Move_X_plus()
    {
        Length = new Vector3(5,0,0);

        if (toCamp == 3) ToCamp();
        else PrepareToMove();
    }
    public void Move_X_minus()
    {
        Length = new Vector3(-5, 0, 0);

        if (toCamp == 4) ToCamp();
        else PrepareToMove();
    }
    public void Move_Z_plus()
    {
        Length = new Vector3(0, 0, 5);

        if (toCamp == 1) ToCamp();
        else PrepareToMove();
    }
    public void Move_Z_minus()
    {
        Length = new Vector3(0, 0, -5);

        if (toCamp == 2) ToCamp();
        else PrepareToMove();
    }

    //移動
    void PrepareToMove()
    {
        se_step.Play(0);

        day_cycle.DayCycle();
        DisableArrows();

        myVital.Hunger_Drop(); //飢餓值下降
        myVital.Thirst_Drop(); //口渴值下降 
        if (myVital.Emergency_Flag == true) myVital.Health_Drop();

        targetPosition = startPosition + Length;
        targetPosition_arrows = startPosition_arrows + Length;

        //Debug.Log(startPosition + " " + targetPosition);
        trigger = true; //開關打開便移動
    }

    void ToCamp()
    {
        message.SetActive(true);
        MessageManager msg_status = message.GetComponent<MessageManager>();
        msg_status.myStatus = MessageManager.Status.Camp;

        messageText.text = "你要前往營地嗎？";
    }
}
