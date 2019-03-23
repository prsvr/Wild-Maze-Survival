using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepExist : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}