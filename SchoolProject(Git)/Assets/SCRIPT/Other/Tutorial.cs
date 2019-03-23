using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public Button move_btn, pack_btn, return_btn, ok_btn;

    public GameObject mask1, mask2;
    public GameObject point1, point2;
    public GameObject MSGBOX, btn1, btn2, btn3, btn4;
    public Text hint_text;

    public int check = 0;

    void Start()
    {
        //Step1
        mask1.SetActive(true);
        point1.SetActive(true);
    }

    void Update()
    {
        //Step2 確保指觸發一次
        if (check == 0)
        {
            move_btn.onClick.AddListener(() =>
                {
                    StartCoroutine(Wait());
                    check = 1;
                }
            );
        }
        else if (check == 1)
        {
                pack_btn.onClick.AddListener(() =>
                {
                    Step4();
                    check = 2;
                }
            );
        }
        else if(check == 2)
        {
                return_btn.onClick.AddListener(() =>
                {
                    check = 3;
                    Step5();
                }
            );
        }

        
        //this.GetComponent<Tutorial>().enabled = false;

        //Step3 檢測是否撿了兩個材料才觸發
        if (GameObject.Find("Object_Controller").GetComponent<Object_Controller>().tutor_count == 2)
        {
            GameObject.Find("Object_Controller").GetComponent<Object_Controller>().tutor_count = 0;
            Step3();
        }
    }

    void Step2()
    {
        MSGBOX.SetActive(true);
        btn1.SetActive(false); //YES
        btn2.SetActive(false); //NO
        btn3.SetActive(true);  //OK
        hint_text.text = "Serach for leafs and sticks to build a camp.";
    }

    void Step3()
    {
        mask2.SetActive(true);
        point2.SetActive(true);
    }

    void Step4()
    {
        MSGBOX.SetActive(true);
        btn1.SetActive(false); //YES
        btn2.SetActive(false); //NO
        btn3.SetActive(true);  //OK
        hint_text.text = "This is your backpack. \nIt contains your own tools, and the items you've gathered. ";
    }

    void Step5()
    {
        MSGBOX.SetActive(true);
        btn1.SetActive(false); //YES
        btn2.SetActive(false); //NO
        btn4.SetActive(true);  //Start
        hint_text.text = "Now, let's build a camp as soon as possible. ";
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Step2();
    }
}
