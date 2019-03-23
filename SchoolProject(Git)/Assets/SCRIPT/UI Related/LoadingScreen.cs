using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    public Text loadText;
    public Image loadImage;
    public string SceneName;

    public void StartClick(GameObject loadScreen)
    {
        loadScreen.SetActive(true);
        StartCoroutine(DisplayLoadingScreen(SceneName));
    }

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            loadText.text = ((int)(async.progress * 100)).ToString() + "%";
            loadImage.transform.localScale = new Vector2(async.progress* 0.3585272f, loadImage.transform.localScale.y);

            if (async.progress == 0.9f)
            {
                loadText.text = "100%";
                loadImage.transform.localScale = new Vector2(1 * 0.3585272f, loadImage.transform.localScale.y);
            }

            yield return null;
        }
    }
}
