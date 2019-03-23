using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEventTemp : MonoBehaviour {

    public AnimalController animalController;

    bool moved = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && moved == false)
        {
            animalController.run = true;
            moved = true;
        }
    }
}
