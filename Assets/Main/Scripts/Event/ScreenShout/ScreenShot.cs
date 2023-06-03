using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(TimerUI))]
public class ScreenShot : MonoBehaviour
{
    //���͂��K�v�Ȃ���
    [SerializeField] private RawImage targetImage;     //�e�N�X�`����\�����邽�߂�RawImage
    [SerializeField] private Image point1;             //�X�N�V�������摜�̂P�Ԗڂ̈ړ���
    [SerializeField] private Image point2;             //�X�N�V�������摜�̂Q�Ԗڂ̈ړ���
    [SerializeField] private Image plusCount;
    [SerializeField] private Image minusCount1;
    [SerializeField] private Image minusCount2;
    [SerializeField] private float duration;           //�J�����V�F�C�N�̌p������
    [SerializeField] private float magnitude;          //�J�����V�F�C�N�̗h��̋���
    [SerializeField]private GameObject mimic = null;    //�Ώۂ̃��f��
    [SerializeField] private GameObject player;
    [SerializeField] private Image lostTimeImg;
    [SerializeField] private GameObject[] gameUI;     //�ʐ^���B��Ƃ��ɏ�������UI

    //���������Ŏg������
    private Camera cam;                                //�v���C���[�̃J����
    private string screenShotPath;                     //�X�N���[���V���b�g���Đ������ꂽ�e�N�X�`���̃t�@�C���p�X
    private string timeStamp;                          //���ݎ�����\�����߂̂���
    private const float firstScale = 0.8f;             //���ڈړ�����Ƃ��ɂǂꂾ��RawImnage���k������邩�i���{�̑傫���ɂȂ邩�j
    private const float secondScale = 0.2f;            //���ڏk������Ƃ��ɂǂꂾ��RawImage���k������邩�i���{�̑傫���ɂȂ邩�j
    private Vector3 InitialPrevPos;                    //RawImage�̏����ʒu
    private Vector3 InitialPrevscale;                  //RawImage�̏����X�P�[��
    internal List<GameObject> setterObj;          //���t���[�������Ă���َ��Ȃ��̂̏����擾���邽�߂̂���
    private bool noneStrangeFlag;                      //�َ��Ȃ��̂��B�e����Ă�����false �B�e����Ă��Ȃ�������true
    public static bool noneTargetFlag;                 //�^�[�Q�b�g���B�e����Ă�����false �B�e����Ă��Ȃ�������true
    private readonly float fadeInSpeed = 0.2f;         //�t�F�[�h�X�s�[�h�A�B�e����Ăяo������
    private Vector3 center = Vector3.zero;             //��ʂ̒��S
    private const float raiseScore = 1.5f;             //�X�R�A�㏸�{��
    private SkillManager skillManager;
    private TimerUI fadeManager;
    private readonly float prevSlide = 1.0f;
    private readonly float prevScale = 0.5f;
    private bool judgeSubTargetFlag;
    private bool judgeTargetFlag;
    private JudgeScreenShot judge;
    

    //�V���b�^�[�A�j���[�V�����̎��
    private enum ShutterAnimationType
    {
        None,
        Target,
        Other
    }

    //���ꂼ��̃X�R�A�̒l
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    //���ꂼ��̔���̕�
    private const float areaWidth = 192;      //(960 / 5) ���5����
    private const float areaHeight = 216;     //(1080 / 5) ���5����

    private void Start()
    {
        judge = new JudgeScreenShot();
        judgeSubTargetFlag = false;
        judgeTargetFlag = false;
        setterObj = new List<GameObject>();
        InitialPrevPos = targetImage.rectTransform.position;
        InitialPrevscale = targetImage.rectTransform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage.enabled = false;
        noneStrangeFlag = true;
        noneTargetFlag = true;
        skillManager = GetComponent<SkillManager>();
        fadeManager = GetComponent<TimerUI>();
        //��ʂ̒��S�����߂�
        center = new Vector3(areaWidth * 2 + areaWidth * 0.5f, areaHeight * 2 + areaHeight * 0.5f, 0.0f);
    }

    private void Update()
    {
        if (Shutter.isFilming)
        {
            OffUIShutter();
            InitializeRawImage();
            ClickShootButton();
            Invoke(nameof(FirstMovePreview), prevSlide);

            judgeTargetFlag = judge.judgeTarget.ShutterTarget(player, mimic, cam, RespawTarget.GetCurrentTargetObj().transform.position, center, areaWidth, areaHeight, raiseScore);
            judgeSubTargetFlag = judge.judgeSubTarget.ShutterSubTargets(cam, player, setterObj, 7.0f);

            //��B��i�َ��Ȃ��́A�^�[�Q�b�g���B�e����Ă��Ȃ��j���Ă�����
            if (!judgeTargetFlag && !judgeSubTargetFlag)
            {
                ShutterNone();
            }

            ShutterAnimationController(fadeInSpeed, judgeTargetFlag, judgeSubTargetFlag);

            //�t���O��������
            judgeTargetFlag = false;
            judgeSubTargetFlag = false;

            //�V���b�^�[�A�j���[�V������x��ĕ\��������
            //�����Ă���UI���I����
            Invoke(nameof(OnUIShutter), fadeInSpeed);
            Shutter.isFilming = false;
        }
    }

    #region �B�e�n

    //��B�肵���Ƃ��̏���
    private void ShutterNone()
    {
        CountDownTimer.DecreaceTime();
        player.GetComponent<Player>().Shake(duration, magnitude);
        SEManager.Instance.PlayMinusTimeCountSE();
        fadeManager.FadeOut(fadeInSpeed, minusCount1);

        //������Փx���n�[�h��������
        if (GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
        {
            //�����T�b�������Ԃ����炷
            CountDownTimer.DecreaceTime();
            fadeManager.FadeOut(fadeInSpeed, minusCount2);
        }
    }

    //�V���b�^�[�A�j���[�V�������Ǘ�����֐�
    private void ShutterAnimationController(float invokeTime, bool targetFlag, bool subTargetFlag)
    {
        //�َ��ȕ������B�e�����ꍇ
        if (subTargetFlag && !targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Other, invokeTime).Forget();
        }
        //�^�[�Q�b�g�����B�e�����ꍇ
        else if (!subTargetFlag && targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target, invokeTime).Forget();
        }
        //�ǂ�����B�e�����ꍇ
        else if (subTargetFlag && targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target, invokeTime).Forget();
        }
        //��B�肾�����ꍇ
        else
        {
            ShutterAnimationmanager(ShutterAnimationType.None, invokeTime).Forget();
        }
    }

    //�B�����ʐ^�̃t�@�C���p�X���擾
    private string GetScreenShotPath()
    {
        string directoryPath = GameManager.Instance.GetDirectryPath();

        string path = GameManager.Instance.GetPicturesFilePath(directoryPath) + timeStamp + ".png";

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

        Texture2D texture = new Texture2D(cam.targetTexture.width / 2, cam.targetTexture.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width / 2, cam.targetTexture.height), 0, 0);
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

    //�B�e�֐�
    public void ClickShootButton()
    {
        StartCoroutine(CreateScreenShot());
    }

    //�B�e�����ʐ^��RawImage�ɕ\��
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

            //�e�N�X�`������ǂݍ��񂾌��RawImage��\������
            targetImage.enabled = true;
        }
    }

    #endregion

    #region UI�n

    //�B�e����u�Ԕ�\���ɂ��ꂽUI��\������֐��iInvoke�ŌĂԁj
    public void OnUIShutter()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(true);
        }
    }
    
    //�B�e����u�Ԕ�\���ɂ��ꂽUI��\������֐��iInvoke�ŌĂԁj
    public void OffUIShutter()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(false);
        }
    }

    private void InitializeRawImage()
    {
        targetImage.texture = null;
        targetImage.enabled = false;
        targetImage.rectTransform.position = InitialPrevPos;
        targetImage.rectTransform.localScale = InitialPrevscale;
    }

    #endregion

    #region �v���r���[�̓���
    private void FirstMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * firstScale, prevScale);
        Invoke(nameof(SecondMovePreview), 0.5f);
    }

    private void SecondMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * secondScale, prevScale);
        targetImage.rectTransform.DOMove(point1.rectTransform.position, prevScale);
        Invoke(nameof(SlideMovePreview), prevSlide);
    }

    private void SlideMovePreview()
    {
        targetImage.transform.DOMoveX(point2.rectTransform.position.x, prevScale);
    }

    #endregion

    #region �V���b�^�[�A�j���[�V����

    private async UniTask ShutterAnimationmanager(ShutterAnimationType type, float delayTime)
    {
        int time = (int)(delayTime * 1000);
        await UniTask.Delay(time);

        switch(type)
        {
            case ShutterAnimationType.None:
                ShutterAnimation.NoneAnimationStart();
                break;
            case ShutterAnimationType.Target:
                ShutterAnimation.TargetAnimationStart();
                break;
            case ShutterAnimationType.Other:
                ShutterAnimation.OtherAnimationStart();
                break;
        }
    }

    #endregion

    #region �Q�b�^�[�A�Z�b�^�[

    public void SetList(List<GameObject> list) { setterObj = list; }

    #endregion
}
