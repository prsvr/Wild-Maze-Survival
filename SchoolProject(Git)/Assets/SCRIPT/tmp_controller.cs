using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp_controller : MonoBehaviour {

    public GameObject obj;

    void Start()
    {
        if (GlobalManager.Instance.Campfire == true)
        {
            obj.transform.Find("Campfire").gameObject.SetActive(true);
        }
        if (GlobalManager.Instance.Shelter == true)
        {
            obj.transform.Find("Shelter").gameObject.SetActive(true);
        }
    }
}
