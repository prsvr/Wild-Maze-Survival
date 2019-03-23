﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown_y : MonoBehaviour {

    public RectTransform container;
    public bool open;

	void Start () {
        container = transform.Find("Container").GetComponent<RectTransform>();
        open = true;
	}

	public void Update() {
        if (open == false)
        {
            Vector3 scale = container.localScale;
            scale.y = Mathf.Lerp(scale.y, 1, Time.deltaTime * 12);
            container.localScale = scale;
        }
        else
        {
            Vector3 scale = container.localScale;
            scale.y = Mathf.Lerp(scale.y, 0, Time.deltaTime * 12);
            container.localScale = scale;
        }
    }

    public void Click()
    {
        if (open == false)
        {
            open = true;
        }
        else
        {
            open = false;
        }
    }

}
