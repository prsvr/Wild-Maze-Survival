using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Clicker : MonoBehaviour {

	void Update () {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
           
            }
        }
        else
        {

        }
	}

    
}
