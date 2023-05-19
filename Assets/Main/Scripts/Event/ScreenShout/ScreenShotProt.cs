using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class ScreenShotProt : Human
{
    Camera cam;
    GameObject targetImage;
    string screenShotPath;
    List<string> filePathes;
    string timeStamp;
    public RawImage preview;

    [SerializeField]
    private Image _image = null;

    public Image prevPos;
    public Image prevPos2;
    public Vector3 InitialPrevPos;
    public Vector3 InitialPrevscale;
    public Text SucceededShutter;
    public Text FailedShutter;

    private int numShutter;           //�T�u�J�����ŃV���b�^�[��؂�����
    private List<GameObject> setterObj;


    void Awake()
    {
        filePathes = new List<string>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage = GameObject.Find("RawImage");
        preview.enabled = false;
        _image.enabled = false;
        SucceededShutter.enabled = false;
        FailedShutter.enabled = false;
        InitialPrevPos = preview.rectTransform.position;
        InitialPrevscale = preview.rectTransform.localScale;
        numShutter = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            preview.enabled = true;
            //preview.transform.position = Vector3.zero;
            GameManager.Instance.IsPhoto = true;
            Shutter();
            //OffPreview();
            //todayTask.TaskCompletion(1);
            ClickShootButton();
            ShowSSImage();
            //Invoke(nameof(MovePreview), 1f);
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            preview.enabled = true;
            //OffPreview();
            //StartCoroutine(nameof(HiddonText), SucceededShutter);
            //todayTask.TaskCompletion(1);
            ClickShootButton();
            ShowSSImage();
            //Invoke(nameof(MovePreview), 1f);

            Debug.Log(setterObj.Count);

            for (int i = 0; i < setterObj.Count; i++)
            {
                if (setterObj[i] != null && setterObj[i].GetComponent<HeterogeneousController>().GetEnableTakePicFlag())
                {
                    //�T�u�J�����J�E���g���C���N�������g
                    GameManager.Instance.numSubShutter++;
                    Debug.Log("1");
                    //�X�R�A�����Z
                    ScoreManger.Score += 10;
                    Debug.Log("2");
                    //tempList[i]�̃I�u�W�F�N�g�̏��Ńt���O���I���ɂ���
                    setterObj[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
                    Debug.Log("��������");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //�t�H���_���̎ʐ^��S�č폜
            ClearCash(filePathes);
        }
    }

    public void SetList(List<GameObject> list) { setterObj = list; }

    //�B�e�֐�
    private void Shutter()
    {
        preview.enabled = true;
        OffPreview();
        StartCoroutine(nameof(HiddonText), SucceededShutter);
        //todayTask.TaskCompletion(1);
        ClickShootButton();
        ShowSSImage();
        FadeIn(0.5f, _image);
        Invoke(nameof(MovePreview), 1f);
    }

    void MovePreview()
    {
        preview.rectTransform.DOScale(transform.localScale / 5, 0.5f);
        preview.rectTransform.DOMove(prevPos.rectTransform.position, 0.5f);
        Invoke(nameof(SlideMovePreview), 1f);
    }

    void SlideMovePreview()
    {
        preview.transform.DOMoveX(prevPos2.rectTransform.position.x, 0.3f);
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
        //Pictures�t�H���_�̒��ɎB�����ʐ^��u���悤�ɂ���
        string path = "Assets/Pictures/" + timeStamp + ".png";

        return path;
    }

    private IEnumerator CreateScreenShot()
    {
        //�e�N�X�`���̖��O�����ݎ����ɐݒ�
        DateTime date = DateTime.Now;
        timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");

        // �����_�����O�����܂őҋ@
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = renderTexture;

        Texture2D texture = new Texture2D(cam.targetTexture.width / 2, cam.targetTexture.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width / 2, cam.targetTexture.height), 0, 0);
        texture.Apply();

        // �ۑ�����摜�̃T�C�Y��ς���Ȃ�ResizeTexture()�����s
        //		texture = ResizeTexture(texture,320,240);

        byte[] pngData = texture.EncodeToPNG();
        //screenShotPath = ;

        // �t�@�C���Ƃ��ĕۑ�����Ȃ�File.WriteAllBytes()�����s
        File.WriteAllBytes(GetScreenShotPath(), pngData);

        filePathes.Add(GetScreenShotPath());

        cam.targetTexture = null;
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
        //SoundManager.Instance.PlayCameraSE();
        StartCoroutine(CreateScreenShot());
    }

    public void ShowSSImage()
    {
        if (!String.IsNullOrEmpty(screenShotPath))
        {
            byte[] image = File.ReadAllBytes(GetScreenShotPath());

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(image);

            // NGUI �� UITexture �ɕ\��
            RawImage target = targetImage.GetComponent<RawImage>();
            target.texture = tex;
        }
    }

    private void ClearCash(List<string> pathes)
    {
        //�p�X�����������X�g�𓮓I�ɃR�s�[
        var numPathes = pathes;

        //�B�����ʐ^�͑S�č폜
        for(int i = 0; i  < numPathes.Count; i++)
        {
            File.Delete(pathes[i]);
        }

        //�p�X�����������X�g�͒��g����ɂ���
        pathes.Clear();
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
