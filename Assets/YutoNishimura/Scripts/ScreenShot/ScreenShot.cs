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
using System.Threading;

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
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private GameObject animationManager;

    //���������Ŏg������
    private Camera cam;                                //�v���C���[�̃J����
    private string screenShotPath;                     //�X�N���[���V���b�g���Đ������ꂽ�e�N�X�`���̃t�@�C���p�X
    private string timeStamp;                          //���ݎ�����\�����߂̂���
    private Vector3 InitialPrevPos;                    //RawImage�̏����ʒu
    private Vector3 InitialPrevscale;                  //RawImage�̏����X�P�[��
    internal List<GameObject> setterObj;          //���t���[�������Ă���َ��Ȃ��̂̏����擾���邽�߂̂���
    private bool noneStrangeFlag;                      //�َ��Ȃ��̂��B�e����Ă�����false �B�e����Ă��Ȃ�������true
    public static bool noneTargetFlag;                 //�^�[�Q�b�g���B�e����Ă�����false �B�e����Ă��Ȃ�������true
    private Vector3 center = Vector3.zero;             //��ʂ̒��S
    private SkillManager skillManager;
    private TimerUI fadeManager;
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

    //���ꂼ��̔���̕�
    private const float areaWidth = 192;      //(960 / 5) ���5����
    private const float areaHeight = 216;     //(1080 / 5) ���5����

    private void Start()
    {
        judge = new JudgeScreenShot(particle);
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
            InitializeRawImage();
            ClickShootButton();
            Invoke(nameof(FirstMovePreview), Config.movePrevTimeFirst);

            judgeTargetFlag = judge.judgeTarget.ShutterTarget(player, mimic, cam, RespawTarget.GetCurrentTargetObj().transform.position, center, areaWidth, areaHeight, Config.raiseScore);
            judgeSubTargetFlag = judge.judgeSubTarget.ShutterSubTargets(cam, player, setterObj, Config.subTargetJudgeLength);

            //��B��i�َ��Ȃ��́A�^�[�Q�b�g���B�e����Ă��Ȃ��j���Ă�����
            if (!judgeTargetFlag && !judgeSubTargetFlag)
            {
                ShutterNone();
            }

            ShutterAnimationController(Config.delayTimeShutterAnimation, judgeTargetFlag, judgeSubTargetFlag);

            //�t���O��������
            judgeTargetFlag = false;
            judgeSubTargetFlag = false;

            Shutter.isFilming = false;
        }
    }

    #region �B�e�n

    //��B�肵���Ƃ��̏���
    private void ShutterNone()
    {
        CountDownTimer.DecreaceTime();
        //player.GetComponent<Player>().Shake(duration, magnitude);
        SEManager.Instance.PlayMinusTimeCountSE();
        fadeManager.FadeOut(Config.fadeOutSpeed, minusCount1);

        //������Փx���n�[�h��������
        if (GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
        {
            //�����T�b�������Ԃ����炷
            CountDownTimer.DecreaceTime();
            fadeManager.FadeOut(Config.fadeOutSpeed, minusCount2);
        }
    }

    //�V���b�^�[�A�j���[�V�������Ǘ�����֐�
    private void ShutterAnimationController(float invokeTime, bool targetFlag, bool subTargetFlag)
    {
        //�َ��ȕ������B�e�����ꍇ
        if (subTargetFlag && !targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Other);
        }
        //�^�[�Q�b�g�����B�e�����ꍇ
        else if (!subTargetFlag && targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target);
        }
        //�ǂ�����B�e�����ꍇ
        else if (subTargetFlag && targetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target);
        }
        //��B�肾�����ꍇ
        else
        {
            ShutterAnimationmanager(ShutterAnimationType.None);
        }
    }

    //�B�����ʐ^�̃t�@�C���p�X���擾
    private string GetScreenShotPath()
    {
        string directoryPath = GameManager.Instance.GetDirectryPath();

        string path = GameManager.Instance.GetPicturesFilePath(directoryPath) + timeStamp + ".png";

        return path;
    }

    private async UniTask CreateScreenShot(CancellationToken cancelToken)
    {
        DateTime date = DateTime.Now;
        timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");

        PostEffectController.SetPostEffectFlag(false);

        ////�C�ӂ̃t���[���̕`�揈�����I���܂ő҂�
        await UniTask.DelayFrame(1, PlayerLoopTiming.Update, cancelToken);

        // �����_�����O�����܂őҋ@
        //yield return new WaitForEndOfFrame();

        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);
        RenderTexture prev = cam.targetTexture;
        cam.targetTexture = rt;
        cam.Render();
        cam.targetTexture = prev;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();

        byte[] pngData = screenShot.EncodeToPNG();
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
        //���g���j�������Ƃ���Unitask�𒆎~���邽�߂̃L�����Z���g�[�N�����擾
        var token = this.GetCancellationTokenOnDestroy();
        CreateScreenShot(token).Forget();
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
        //�v���r���[������������
        targetImage.texture = null;
        targetImage.enabled = false;
        targetImage.rectTransform.position = InitialPrevPos;
        targetImage.rectTransform.localScale = InitialPrevscale;
    }

    #endregion

    #region �v���r���[�̓���
    private void FirstMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * Config.reduceScaleFirst, Config.changePrevTransformFirst);
        Invoke(nameof(SecondMovePreview), Config.movePrevTimeSecond);
    }

    private void SecondMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * Config.reduceScaleSecond, Config.changePrevTransformSecond);
        targetImage.rectTransform.DOMove(point1.rectTransform.position, Config.changePrevTransformSecond);
        Invoke(nameof(SlideMovePreview), Config.movePrevTimeThird);
    }

    private void SlideMovePreview()
    {
        targetImage.transform.DOMoveX(point2.rectTransform.position.x, Config.changePrevTransformThird);
    }

    #endregion

    #region �V���b�^�[�A�j���[�V����

    private void ShutterAnimationmanager(ShutterAnimationType type)
    {
        switch(type)
        {
            case ShutterAnimationType.None:
                GetComponent<ShutterAnimation>().StartAnimation(GameManager.ShutterAnimationState.None);
                break;
            case ShutterAnimationType.Target:
                GetComponent<ShutterAnimation>().StartAnimation(GameManager.ShutterAnimationState.Target);
                break;
            case ShutterAnimationType.Other:
                GetComponent<ShutterAnimation>().StartAnimation(GameManager.ShutterAnimationState.Other);
                break;
        }
    }

    #endregion

    #region �Q�b�^�[�A�Z�b�^�[

    public void SetList(List<GameObject> list) { setterObj = list; }

    #endregion
}
