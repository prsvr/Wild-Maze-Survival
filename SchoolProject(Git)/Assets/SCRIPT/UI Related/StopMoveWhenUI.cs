using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoveWhenUI : MonoBehaviour {

    public bool stop;

	
	void OnEnable () {
        stop = true;
        GameObject.Find("Player").GetComponent<Player_Move>().enabled = false;
        GameObject.Find("Player").GetComponent<Player_Camera>().enabled = false;
    }

    void OnDisable()
    {
        stop = false;
        if (stop == false)
        {
            GameObject.Find("Player").GetComponent<Player_Move>().enabled = true;
            GameObject.Find("Player").GetComponent<Player_Camera>().enabled = true;
        }
    }
}
