using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    public Animator animator;
    public bool run;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private float timeMove;

    public Transform pos_now;       //當前的位置
    public Vector3 Length;          //移動的固定距離
    public float transitionTime;    //總移動時間(影響lerp的速度)
    public bool trigger;            //是否開始移動的開關

    AudioManager audioManager;


    void Start () {
        animator.SetBool("isIdle", true);

        startPosition = pos_now.position;
        targetPosition = startPosition + Length;
        timeMove = 0.0f;

        audioManager = FindObjectOfType<AudioManager>();
    }
	
	void Update () {
        if (run == true)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
            trigger = true;
            Run();
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRun", false);
        }
    }

    void Run()
    {
        if (trigger == true)
        {
            if (Vector3.Distance(targetPosition, pos_now.position) > 0) // 確認距離目的地多遠才要停下來
            {
                timeMove += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, targetPosition, timeMove / transitionTime);

                //Debug.Log(startPosition + " " + targetPosition);
            }
            else
            {
                run = false;
                trigger = false;
                timeMove = 0.0f;
                startPosition = transform.position;

                //Debug.Log(startPosition + " " + targetPosition);
            }
        }
    }
}
