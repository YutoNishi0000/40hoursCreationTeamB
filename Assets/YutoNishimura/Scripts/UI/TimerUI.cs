using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�ǉ��T�b�A�}�C�i�X�T�b�̎��ɃC���[�W��\������
//�C���[�W�̓t�F�[�h�C������
public class TimerUI : MonoBehaviour
{
    private static Image fadeImage;
    private static Image anotherImage;       //���[�h���n�[�h���������ɒǉ��ŕ\���������C���[�W
    [SerializeField] private static Image plusImg;
    [SerializeField] private static Image minusImg;

    //�t�F�[�h�pImage�̓����x
    private static float alpha = 0.0f;

    //�t�F�[�h�C���A�E�g�̃t���O
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //�t�F�[�h���������ԁi�P�ʂ͕b�j
    private static float fadeTime = 0.5f;

    private static float enabledTime = 0;

    //�t�F�[�h�p��Canvas��Image����
    public static void Init()
    {

    }

    //�t�F�[�h�A�E�g�J�n
    public static void FadeOut(bool getPoints)
    {
        if (getPoints)
        {
            fadeImage = GameObject.Find("GetPoint").GetComponent<Image>();
        }
        else
        {
            fadeImage = GameObject.Find("LostPoint_1").GetComponent<Image>();
            anotherImage = GameObject.Find("LostPoint_2").GetComponent<Image>();
        }
        fadeImage.enabled = true;
        alpha = 0;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        anotherImage.color = new Color(anotherImage.color.r, anotherImage.color.g, anotherImage.color.b, alpha);
        isFadeOut = true;
    }

    void Update()
    {
        if (isFadeOut)
        {
            //�o�ߎ��Ԃ��瓧���x�v�Z
            alpha += Time.deltaTime / fadeTime;

            //�t�F�[�h�A�E�g�I������
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;

                enabledTime += Time.deltaTime;

                if(enabledTime > 1.0f)
                {
                    isFadeOut = false;
                    enabledTime = 0;
                    fadeImage.enabled = false;
                }
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

            if(GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
            {
                anotherImage.color = new Color(anotherImage.color.r, anotherImage.color.g, anotherImage.color.b, alpha);
            }
        }
    }
}
