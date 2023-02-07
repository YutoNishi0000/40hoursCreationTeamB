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

    // �N���b�N�҂��̃R���[�`��
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsClicked()) yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawText("���ȏЉ�Ă��������B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("A�N", "�l�̖��O��A!");
        yield return StartCoroutine("Skip");

        uitext.DrawText("B�N", "�l�̖��O��B!");
        yield return StartCoroutine("Skip");

        uitext.DrawText("C�N", "�l�̖��O��C!");
        yield return StartCoroutine("Skip");
        text.SetActive(false);

        uitext.DrawText("S�N", "�l�̖��O��S!");
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
