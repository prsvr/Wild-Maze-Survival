using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DialogsScript : MonoBehaviour
{

    // UI.Text物件
    public Text text;
    // 每個字出現的間格
    public float letterPause = 0.3f;
    // 每句話的間格
    public int sentencePause = 1;
    // 例句
    private string sentence = "20XX年8月15日，你與三五好友相約前往貝里斯叢林探險。";
    private string sentence2 = "不料，在一個月黑風高的夜晚突然下起了狂風暴雨，導致山洪爆發......";
    private string sentence3 = "在還來不及叫醒睡夢中的朋友之前，你們就被沖散並失去了意識......";
    private string sentence4 = "醒來之後發現一切都變了調，你能否在找到同伴之前生存下去？";
    int i = 0;

    // 初始化
    void Start()
    {
        StartCoroutine(Delay(sentence));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            SceneManager.LoadScene("Stage");
    }

    // 每句話停頓點
    IEnumerator Delay(string str)
    {
        yield return new WaitForSeconds(sentencePause);
        StartCoroutine(TypeText(str));
    }

    // 打字機效果
    IEnumerator TypeText(string str)
    {
        foreach (var word in str)
        {
            text.text += word;
            yield return new WaitForSeconds(letterPause);
        }
        yield return new WaitForSeconds(2);
        if (i == 0)
        {
            text.text = "";
            StartCoroutine(TypeText(sentence2));
        }
        if (i == 1)
        {
            text.text = "";
            StartCoroutine(TypeText(sentence3));
        }
        if (i == 2)
        {
            text.text = "";
            StartCoroutine(TypeText(sentence4));
        }
        i++;

        //yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadScene("Camp");
    }
}
