using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Goto_Camp : MonoBehaviour {

	// Use this for initialization
	public void Load_Scene()
    {
        SceneManager.LoadScene("CampLocation");
    }
}
