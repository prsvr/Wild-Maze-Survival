using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_AR : MonoBehaviour
{
    public GameObject Player, Canvas, Objects;

    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("AR"));
    }

    public void Unload_AR()
    {
        Player.SetActive(true);
        Canvas.SetActive(true);
        Objects.SetActive(true);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("AR"));
    }
}