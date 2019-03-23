using UnityEngine;
using System.Collections;
public class GyroRotate : MonoBehaviour
{
    Quaternion rot = new Quaternion(0, 0, 1, 0);

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            transform.parent.rotation = Quaternion.Euler(90, 0, 0);            
            transform.localRotation = Input.gyro.attitude * rot;
        }
    }
}