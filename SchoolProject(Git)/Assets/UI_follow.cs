using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_follow : MonoBehaviour {

    public GameObject arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 arrow_pos = Camera.main.WorldToScreenPoint(this.transform.position);
        arrow.transform.position = arrow_pos;
	}
}
