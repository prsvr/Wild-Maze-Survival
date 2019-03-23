using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAtOpening : MonoBehaviour {

    private float speedMod = 1.0f;
    private Vector3 point;

    void Start()
    {
        transform.LookAt(point);
    }

    void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
    }
}
