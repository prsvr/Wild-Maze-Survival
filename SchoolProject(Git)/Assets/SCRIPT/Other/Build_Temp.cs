using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Build_Temp : MonoBehaviour {

    public int num = 0;
    public AudioClip[] Clips;
    public AudioSource audio;
    public Text info_text;
    public Button button1;
    public Button button2;
    public GameObject obj1;
    public GameObject obj2;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		button1.onClick.AddListener(() => 
            {
                num = 1;
                info_text.text = "<color=#a52a2aff><b>[Shelter]</b></color>\nA simple shelter for you to take a rest.";
            }
        );
        button2.onClick.AddListener(() =>
            {
                num = 2;
                info_text.text = "<color=#a52a2aff><b>[Campfire]</b></color>\nYou can cook foods with it. The fire can warm your body as well.";
            }
        );
    }


    public void Build()
    {
        if (num == 1)
        {
            obj1.SetActive(true);
            audio.clip = Clips[0];
            audio.Play();
        }
        else if (num == 2)
        {
            obj2.SetActive(true);
            audio.clip = Clips[1];
            audio.Play();

        }
    }
}
