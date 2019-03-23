using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controller : MonoBehaviour {

    public GameObject OBJ;
    public GameObject Player;
    public int tutor_count = 0;
    public int targetID;

    void Update()
    {
        //if (tutor_count > 2) tutor_count = 2;
    }

    public void Kill()
    {
        OBJ.SetActive(false);
        //Player.GetComponent<Player_Move>().enabled = true;
        //Player.GetComponent<Player_Camera>().enabled = true;
    }

    public void Not_Kill()
    {
        //Player.GetComponent<Player_Move>().enabled = true;
        //Player.GetComponent<Player_Camera>().enabled = true;
    }
}
