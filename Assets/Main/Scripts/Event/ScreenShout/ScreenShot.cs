using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class ScreenShot : Human
{
    Camera cam;
    GameObject canvas;
    GameObject targetImage;
    string screenShotPath;
    string timeStamp;
    public RawImage preview;

    [SerializeField]
    private Image _image = null;

    private PlayerStateController playerState;
    private ChangeCameraAngle _changeCamera;
    private TodayTask todayTask;

    public Image prevPos;
    public Image prevPos2;
    public Vector3 InitialPrevPos;
    public Vector3 InitialPrevscale;
    public Text SucceededShutter;
    public Text FailedShutter;


    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas");
        targetImage = GameObject.Find("RawImage");
        todayTask = GameObject.Find("TodayTask").GetComponent<TodayTask>();
        preview.enabled = false;
        _image.enabled = false;
        SucceededShutter.enabled = false;
        FailedShutter.enabled = false;
        playerState = FindObjectOfType<PlayerStateController>();
        _changeCamera= FindObjectOfType<ChangeCameraAngle>();
        InitialPrevPos = preview.rectTransform.position;
        InitialPrevscale = preview.rectTransform.localScale;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift) && playerState.GetPlayerState() == PlayerStateController.PlayerState.Voyeurism)
        //{
        //    if (ShutterChanceController._shutterChance)
        //    {
        //        StartCoroutine(nameof(HiddonText), SucceededShutter);
        //        //todayTask.TaskCompletion(1);

        //        for(int i = 0; i < todayTask.todayTask.Count; i++)
        //        {
        //            //�^�X�N������ڂ̃^�X�N���e�ł����
        //            if (GameManager.Instance.tasks[i].date == 1)
        //            {
        //                GameManager.Instance.tasks[i].isCompletion = true;
        //            }
        //        }

        //        int j = 0;

        //        //�^�X�N���������
        //        if(todayTask.todayTask.Count == 2)
        //        {
        //            //��Ƃ��^�X�N���N���A���Ă���Ȃ��
        //            if (GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
        //            {
        //                //�����ɉ����Ď��̓��Ɉڍs
        //                switch(GameManager.Instance.GetDate())
        //                {
        //                    case 1:
        //                        Day2.day2 = true;
        //                        break;

        //                    case 2:
        //                        Day3.day3 = true;
        //                        break;
        //                }
        //            }
        //        }
        //        //�^�X�N��������ł����Ă��̓���Day2��������
        //        else if(todayTask.todayTask.Count == 1 && GameManager.Instance.GetDate() == 1 && GameManager.Instance.tasks[j].isCompletion)
        //        {
        //            //�O���ڂɈڍs
        //            Day2.day2 = true;
        //        }

        //        //���̓��̃^�X�N���S�ăN���A����Ă����玟��Day�Ɉڍs
        //        if (GameManager.Instance.tasks[j].isCompletion && GameManager.Instance.tasks[j + 1].isCompletion)
        //        {
        //            //�����A����Day������ڂł����
        //            if (GameManager.Instance.Date == 1)
        //            {
        //                //�O���ڂɈڍs
        //                Day2.day2 = true;
        //            }
        //            //�����A����Day���O���ڂł����
        //            else if(GameManager.Instance.Date == 2)
        //            {
        //                //�l���ڂɈڍs
        //                Day3.day3 = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        StartCoroutine(nameof(HiddonText), FailedShutter);
        //    }

        //    ClickShootButton();
        //    FadeIn(0.5f, _image);
        //    preview.enabled = true;
        //    Invoke(nameof(MovePreview), 1f);
        //}
    }

    void MovePreview()
    {
        _changeCamera.ExitVoyeurism();
        preview.rectTransform.DOScale(transform.localScale / 3, 0.5f);
        preview.rectTransform.DOMove(prevPos.rectTransform.position, 0.5f);
        Invoke(nameof(SlideMovePreview), 1f);
    }

    void SlideMovePreview()
    {
        preview.transform.DOMoveX(prevPos2.rectTransform.position.x, 0.3f);
        Invoke(nameof(OffPreview), 0.3f);
    }

    /// <summary>
    /// �e�L�X�g���\���ɂ��܂�
    /// </summary>
    /// <param name="text">��\���ɂ������e�L�X�g</param>
    /// <param name="timne">���b��ɔ�\���ɂ��邩</param>
    IEnumerator HiddonText(Text text)
    {
        text.enabled = true;

        yield return new WaitForSeconds(2);

        text.enabled = false;
    }

    void OffPreview()
    {
        //FadeIn(0.5f, preview);
        preview.enabled = false;
        preview.rectTransform.position = InitialPrevPos;
        preview.rectTransform.localScale = InitialPrevscale;
    }

    private string GetScreenShotPath()
    {
        string path = "";
        // �v���W�F�N�g�t�@�C�������ɍ쐬
        path = timeStamp + ".png";
        //		path = Application.persistentDataPath+timeStamp + ".png";
        return path;
    }

    // UI�����������ꍇ��canvas���A�N�e�B�u�ɂ���
    private void UIStateChange()
    {
        canvas.SetActive(!canvas.activeSelf);
    }

    private IEnumerator CreateScreenShot()
    {
        UIStateChange();
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

        Debug.Log("Done!");
        UIStateChange();

        ShowSSImage();
    }

    Texture2D ResizeTexture(Texture2D src, int dst_w, int dst_h)
    {
        Texture2D dst = new Texture2D(dst_w, dst_h, src.format, false);

        float inv_w = 1f / dst_w;
        float inv_h = 1f / dst_h;

        for (int y = 0; y < dst_h; ++y)
        {
            for (int x = 0; x < dst_w; ++x)
            {
                dst.SetPixel(x, y, src.GetPixelBilinear((float)x * inv_w, (float)y * inv_h));
            }
        }
        return dst;
    }

    public void ClickShootButton()
    {
        SoundManager.Instance.PlayCameraSE();
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

    /// <summary>
    /// �K��l�ɖ߂�
    /// </summary>
    private void Reset()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// �t�F�[�h�C������(�I�[�o�[���[�h�֐�)
    /// </summary>
    public void FadeIn(float duration, RawImage image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed, true));
    }

    /// <summary>
    /// �t�F�[�h�C������
    /// </summary>
    public void FadeIn(float duration, Image image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed, true));
    }

    /// <summary>
    /// �t�F�[�h�A�E�g����
    /// </summary>
    public void FadeOut(float duration, Image image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed));
    }

    /// <summary>
    /// �t�F�[�h�A�E�g����(�I�[�o�[���[�h�֐�)
    /// </summary>
    public void FadeOut(float duration, RawImage image, Action on_completed = null)
    {
        image.enabled = true;
        StartCoroutine(ChangeAlphaValueFrom0To1OverTime(duration, image, on_completed));
    }

    /// <summary>
    /// ���Ԍo�߂ŃA���t�@�l���u0�v����u1�v�ɕύX
    /// </summary>
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
        float duration,
        Image image,
        Action on_completed,
        bool is_reversing = false
    )
    {
        if (!is_reversing) image.enabled = true;

        var elapsed_time = 0.0f;
        var color = image.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            image.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        if (is_reversing) image.enabled = false;
        if (on_completed != null) on_completed();
    }

    /// <summary>
    /// ���Ԍo�߂ŃA���t�@�l���u0�v����u1�v�ɕύX(�I�[�o�[���[�h�֐�)
    /// </summary>
    private IEnumerator ChangeAlphaValueFrom0To1OverTime(
        float duration,
        RawImage image,
        Action on_completed,
        bool is_reversing = false
    )
    {
        if (!is_reversing) image.enabled = true;

        var elapsed_time = 0.0f;
        var color = image.color;

        while (elapsed_time < duration)
        {
            var elapsed_rate = Mathf.Min(elapsed_time / duration, 1.0f);
            color.a = is_reversing ? 1.0f - elapsed_rate : elapsed_rate;
            image.color = color;

            yield return null;
            elapsed_time += Time.deltaTime;
        }

        if (is_reversing) image.enabled = false;
        if (on_completed != null) on_completed();
    }
}
