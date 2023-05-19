using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class ScreenShot : MonoBehaviour
{
    private Camera cam;                                //�v���C���[�̃J����
    [SerializeField] private RawImage targetImage;     //�e�N�X�`����\�����邽�߂̕�
    [SerializeField] private Image point1;             //�X�N�V�������摜�̂P�Ԗڂ̈ړ���
    [SerializeField] private Image point2;             //�X�N�V�������摜�̂Q�Ԗڂ̈ړ���
    [SerializeField] private Image point3;             //�X�N�V�������摜�̂R�Ԗڂ̈ړ���
    private string screenShotPath;                     //�X�N���[���V���b�g���Đ������ꂽ�e�N�X�`���̃t�@�C���p�X
    private string timeStamp;                          //���ݎ�����\�����߂̂���
    private const float firstScale = 0.8f;             //���ڈړ�����Ƃ��ɂǂꂾ��RawImnage���k������邩�i���{�̑傫���ɂȂ邩�j
    private const float secondScale = 0.2f;            //���ڏk������Ƃ��ɂǂꂾ��RawImage���k������邩�i���{�̑傫���ɂȂ邩�j
    private RawImage initialTargetImage;               //RawImage�̏����l

    void Awake()
    {
        initialTargetImage = targetImage;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            InitializeRawImage();
            ClickShootButton();
        }
    }

    private void InitializeRawImage()
    {
        targetImage = initialTargetImage;
    }

    private string GetScreenShotPath()
    {
        string path = "Assets/Pictures/" + timeStamp + ".png";

        return path;
    }

    private IEnumerator CreateScreenShot()
    {
        DateTime date = DateTime.Now;
        timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");
        // �����_�����O�����܂őҋ@
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = renderTexture;

        Texture2D texture = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        texture.Apply();

        // �ۑ�����摜�̃T�C�Y��ς���Ȃ�ResizeTexture()�����s
        //		texture = ResizeTexture(texture,320,240);

        byte[] pngData = texture.EncodeToPNG();
        screenShotPath = GetScreenShotPath();

        // �t�@�C���Ƃ��ĕۑ�����Ȃ�File.WriteAllBytes()�����s
        File.WriteAllBytes(screenShotPath, pngData);

        cam.targetTexture = null;

        //���������e�N�X�`���t�@�C���������ǂݍ����RawImage�ɏo��
        ShowSSImage();
    }

    public void ClickShootButton()
    {
        StartCoroutine(CreateScreenShot());
    }

    public void ShowSSImage()
    {
        if (!String.IsNullOrEmpty(screenShotPath))
        {
            byte[] image = File.ReadAllBytes(screenShotPath);

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(image);

            // NGUI �� UITexture �ɕ\��
            RawImage target = targetImage.GetComponent<RawImage>();
            target.texture = tex;
        }
    }
}
