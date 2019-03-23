using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class AnimalEventTemp2 : MonoBehaviour {

    public AnimalController animalController;
    public GameObject Red;
    AudioManager audioManager;
    Player_Vital player_Vital;

    bool moved = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player_Vital = FindObjectOfType<Player_Vital>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && moved == false)
        {
            audioManager.Play("hit");

            player_Vital.Hurt();

            CameraShaker.Instance.ShakeOnce(8,8,.2f,1);
            animalController.run = true;
            moved = true;

            StartCoroutine(RedEffect(1));
        }
    }

    private IEnumerator RedEffect(float waitTime)
    {
        Red.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Red.SetActive(false);
    }
}
