using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Object_Highlight : MonoBehaviour {

    public float init_outline;
    public float hover_outline;
    public Text text;
    public Button Y_btn;
    public GameObject MSGMOX;
    public GameObject target;
    public int targetID;

    void OnMouseOver()
    {
        GetComponent<Renderer>().material.SetFloat("_OutlineWidth", hover_outline);
        GameObject.Find("Object_Controller").GetComponent<Object_Controller>().OBJ = target;
        GameObject.Find("Object_Controller").GetComponent<Object_Controller>().targetID = targetID;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetFloat("_OutlineWidth", init_outline);
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //檢測若在操作UI，防止UI後的物件被點擊。
        {
            //temp tutorials may delete later
            /*Y_btn.onClick.AddListener(() =>
                {
                    GameObject.Find("Object_Controller").GetComponent<Object_Controller>().tutor_count += 1;
                }
            )
            ;
            //temp tutorials may delete later*/

            MSGMOX.SetActive(true);
            text.text = "Do you want to pick this up?";

            //GameObject.Find("Player").GetComponent<Player_Move>().enabled = false;
            //GameObject.Find("Player").GetComponent<Player_Camera>().enabled = false;
        }
    }

}
