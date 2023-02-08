using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public UIText uitext;
    [SerializeField] GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    // クリック待ちのコルーチン
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsClicked()) yield return 0;
    }

    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        uitext.DrawText("自己紹介してください。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("A君", "僕の名前はA!");
        yield return StartCoroutine("Skip");

        uitext.DrawText("B君", "僕の名前はB!");
        yield return StartCoroutine("Skip");

        uitext.DrawText("C君", "僕の名前はC!");
        yield return StartCoroutine("Skip");
        text.SetActive(false);

        uitext.DrawText("S君", "僕の名前はS!");
        yield return StartCoroutine("Skip");
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            text.SetActive(true);
        }
    }
}
