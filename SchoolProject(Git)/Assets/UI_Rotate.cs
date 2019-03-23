using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Rotate : MonoBehaviour {

    private Quaternion rotation;
    public Transform player;

    void Awake()
    {
        rotation = transform.rotation;
    }
    void Update()
    {
        rotation = player.rotation;
    }
}
