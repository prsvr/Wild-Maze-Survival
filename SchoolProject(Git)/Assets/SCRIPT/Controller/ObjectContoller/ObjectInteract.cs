using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ObjectInteract : MonoBehaviour {

    float init_outline = 0;        
    float hover_outline = 0.06f;

    public ObjectManager objm;
    public int type;            //此參數由ObjectManager初始化時提供

    void Start()
    {
        objm = GameObject.FindObjectOfType<ObjectManager>();
    }

    void OnMouseOver()
    {
        //GetComponent<Renderer>().material.SetFloat("_OutlineWidth", hover_outline);
    }

    void OnMouseExit()
    {
       // GetComponent<Renderer>().material.SetFloat("_OutlineWidth", init_outline);
    }

    void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject()) //檢測若在操作UI，防止UI後的物件被點擊。
        {
            objm.PickUp(gameObject.name, type);             //撿物件進背包
            objm.SetActive(gameObject.name, type, false);   //關閉物件
        }
    }
}
