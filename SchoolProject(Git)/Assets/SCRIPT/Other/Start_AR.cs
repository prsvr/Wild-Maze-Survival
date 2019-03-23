using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_AR : MonoBehaviour
{
    GameObject Player, Canvas, Objects;
    bool done = true;

    void OnMouseDown()
    {
        done = false;
        Player = GameObject.Find("Player");
        Canvas = GameObject.Find("Canvas");
        Objects = GameObject.Find("Objects");
        Player.SetActive(false);
        Canvas.SetActive(false);
        SceneManager.LoadScene("AR", LoadSceneMode.Additive);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "AR" && !done)
        {
            Transform plane = GameObject.Find("Mid Air Stage").transform;
            GameObject clone = Instantiate(gameObject, plane.position, plane.rotation, plane);
            clone.transform.localScale *= 0.3f;
            Destroy(clone.GetComponent<Start_AR>());
            Objects.SetActive(false);
            GameObject.Find("Back_btn").GetComponent<End_AR>().Player = Player;
            GameObject.Find("Back_btn").GetComponent<End_AR>().Canvas = Canvas;
            GameObject.Find("Back_btn").GetComponent<End_AR>().Objects = Objects;
            done = true;
        }
    }
}