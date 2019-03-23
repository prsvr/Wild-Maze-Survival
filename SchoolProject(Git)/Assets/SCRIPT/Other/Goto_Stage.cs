using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Goto_Stage : MonoBehaviour {

	// Use this for initialization
	public void Load_Scene()
    {
        SceneManager.LoadScene("Stage");
    }
}
