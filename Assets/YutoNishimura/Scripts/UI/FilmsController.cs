using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//�Q�[�����ɎB�e�����ʐ^��\������N���X
public class FilmsController : MonoBehaviour
{
    [Header("RawImage(NoImage�݂����ȉ摜���\���Ă���RawImage)�����Ă�������")]
    [SerializeField] private RawImage pictures;           //RawImage
    [Header("���b�ԕ\�����邩")]
    [SerializeField] private float intervalChangeTime = 0.5f;
    [Header("�t�F�[�h����")]
    private float fadeTime = 0.5f;    //�t�F�[�h���������ԁi�P�ʂ͕b�j
    private string[] picturesFilepathes;

    //�t�F�[�h�pImage�̓����x
    private static float alpha = 0.0f;

    //�t�F�[�h�C���A�E�g�̃t���O
    private bool isFadeIn = false;
    private bool isFadeOut = false;


    private float enabledTime = 0;

    private List<Texture> images;

    private int pictureIndex;    //�����Ԗڂ̎ʐ^��\�����Ă��邩

    // Start is called before the first frame update
    void Start()
    {
        images = new List<Texture>();
        //�C���f�b�N�X�͂O�Ԗڂ���
        pictureIndex = 0;
        //�A���t�@�l�͂O
        alpha = 0;
        LoadPictures();
    }

    // Update is called once per frame
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

                if (enabledTime > intervalChangeTime)
                {
                    isFadeOut = false;
                    isFadeIn = true;
                    enabledTime = 0;
                }
            }
        }
        else if(isFadeIn)
        {
            //�o�ߎ��Ԃ��瓧���x�v�Z
            alpha -= Time.deltaTime / fadeTime;

            //�t�F�[�h�A�E�g�I������
            if (alpha <= 0)
            {
                alpha = 0f;
                isFadeOut = true;
                isFadeIn = false;

                //�����Ŏʐ^�̃f�[�^��؂�ւ���
                pictureIndex = (pictureIndex + 1) % picturesFilepathes.Length;
                pictures.texture = images[pictureIndex];
            }
        }

        //�t�F�[�h�pImage�̐F�E�����x�ݒ�
        pictures.color = new Color(pictures.color.r, pictures.color.g, pictures.color.b, alpha);
    }

    //�B�����ʐ^�����[�h
    public void LoadPictures()
    {
        string directoryPath = GameManager.Instance.GetDirectryPath();
        //�����f�B���N�g�������݂��Ă�����
        if (Directory.Exists(directoryPath))
        {
            //�ʐ^�t�H���_���擾
            picturesFilepathes = Directory.GetFiles(directoryPath);

            if (picturesFilepathes.Length != 0)
            {
                for (int i = 0; i < picturesFilepathes.Length; i++)
                {
                    if (!String.IsNullOrEmpty(picturesFilepathes[i]))
                    {
                        //�e�N�X�`������ǂݍ���
                        byte[] image = File.ReadAllBytes(picturesFilepathes[i]);

                        Texture2D tex = new Texture2D(0, 0);
                        tex.LoadImage(image);

                        // �z��Ƀf�[�^��}��
                        images.Add(tex);
                    }
                }

                //1�Ԗڂ̎ʐ^��\��������
                pictures.texture = images[pictureIndex];
                isFadeOut = true;
            }
        }
        else
        {
            alpha = 1;
        }
    }
}
