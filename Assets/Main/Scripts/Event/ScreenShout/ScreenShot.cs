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
    private List<GameObject> setterObj;                //���t���[�������Ă���َ��Ȃ��̂̏����擾���邽�߂̂���
    private bool noneStrangeFlag;                      //�َ��Ȃ��̂��B�e����Ă�����false �B�e����Ă��Ȃ�������true
    public static bool noneTargetFlag;                 //�^�[�Q�b�g���B�e����Ă�����false �B�e����Ă��Ȃ�������true
    private readonly float fadeInSpeed = 0.2f;         //�t�F�[�h�X�s�[�h�A�B�e����Ăяo������
    private Vector3 center = Vector3.zero;             //��ʂ̒��S
    private const float raiseScore = 1.5f;             //�X�R�A�㏸�{��
    private SkillManager skillManager;
    private TimerUI fadeManager;
    private readonly float prevSlide = 1.0f;
    private readonly float prevScale = 0.5f;

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
            ShutterManager();
            ShutterOther();

            //��B��i�َ��Ȃ��́A�^�[�Q�b�g���B�e����Ă��Ȃ��j���Ă�����
            if (noneTargetFlag && noneStrangeFlag)
            {
                ShutterNone();
            }

            ShutterAnimationController(fadeInSpeed);

            //�t���O��������
            noneTargetFlag = true;
            noneStrangeFlag = true;

            //�V���b�^�[�A�j���[�V������x��ĕ\��������
            //�����Ă���UI���I����
            Invoke(nameof(OnUIShutter), fadeInSpeed);
            Shutter.isFilming = false;
        }
    }

    #region �B�e�n

    //�B�e����֐�
    private void ShutterManager()
    {
        //��Q�����Ȃ���
        if (!createRay())
        { 
            switch (checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position)))
            {
                case ScoreType.low:
                    ShutterTarget();
                    GameManager.Instance.numLowScore++;
                    break;
                case ScoreType.midle:
                    ShutterTarget();
                    GameManager.Instance.numMiddleScore++;
                    break;
                case ScoreType.high:
                    ShutterTarget();
                    GameManager.Instance.numHighScore++;
                    break;
                case ScoreType.outOfScreen:
                    SEManager.Instance.PlayShot();
                    noneTargetFlag = true;
                    Debug.Log("��ʊO");
                    break;
            }
        }
        else
        {
            SEManager.Instance.PlayShot();
        }
    }

    //�B�e���̏���
    public void ShutterTarget()
    {
        GameObject targetObj = RespawTarget.GetCurrentTargetObj();
        //�蓮�Ń^�[�Q�b�g���\�������ʒu�𒲐�
        Vector3 targetPos = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y - 1, targetObj.transform.position.z);
        Instantiate(mimic, targetPos, targetObj.transform.rotation);
        noneTargetFlag = false;
        //���Ԃ��l��
        CountDownTimer.IncreaceTime();
        //�^�[�Q�b�g���B�e���ꂽ
        SetPhotographTargetFlag(false);
        //�X�R�A���Z
        ScoreManger.Score += FinalScorePoint(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position));
        //�Ώۂ��B�e�����񐔂��C���N�������g
        GameManager.Instance.numTargetShutter++;
        //SE
        SEManager.Instance.PlayTargetShot();
        //�^�[�Q�b�g���X�|�[��
        RespawTarget.RespawnTarget();
        //UI���t�F�[�h�A�E�g
        fadeManager.FadeOut(fadeInSpeed, plusCount);
        SEManager.Instance.PlayPlusTimeCountSE();
    }

    private void ShutterOther()
    {
        //�T�u�J�����B�e���肪�I���������Ƃ��̔���
        for (int i = 0; i < setterObj.Count; i++)
        {
            if (setterObj[i] != null && JudgeSubTarget(cam, setterObj[i], player))
            {
                noneStrangeFlag = false;
                //�T�u�J�����J�E���g���C���N�������g
                GameManager.Instance.numSubShutter++;
                //�X�R�A�����Z
                ScoreManger.Score += 10;
                //tempList[i]�̃I�u�W�F�N�g�̏��Ńt���O���I���ɂ���
                setterObj[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
            }
        }
    }

    private void ShutterNone()
    {
        //Debug.Log("���Ԃ������܂���");
        Debug.Log("�����B�e�ł��ĂȂ�");
        CountDownTimer.DecreaceTime();
        player.GetComponent<Player>().Shake(duration, magnitude);
        //ShutterAnimation.NoneAnimationStart();
        fadeManager.FadeOut(fadeInSpeed, minusCount1);
        SEManager.Instance.PlayMinusTimeCountSE();

        //������Փx���n�[�h��������
        if (GameManager.Instance.GetGameMode() == GameManager.GameMode.Hard)
        {
            //�����T�b�������Ԃ����炷
            CountDownTimer.DecreaceTime();
            fadeManager.FadeOut(fadeInSpeed, minusCount2);
        }
    }

    //�V���b�^�[�A�j���[�V�������Ǘ�����֐�
    private void ShutterAnimationController(float invokeTime)
    {
        //�َ��ȕ������B�e�����ꍇ
        if (!noneStrangeFlag && noneTargetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Other, invokeTime).Forget();
        }
        //�^�[�Q�b�g�����B�e�����ꍇ
        else if (noneStrangeFlag && !noneTargetFlag)
        {
            ShutterAnimationmanager(ShutterAnimationType.Target, invokeTime).Forget();
        }
        //�ǂ�����B�e�����ꍇ
        else if (!noneStrangeFlag && !noneTargetFlag)
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

    #region �^�[�Q�b�g�B�e����

    /// <summary>
    /// ���[���h���W���X�N���[�����W��
    /// </summary>
    /// <param name="cam">�J�����I�u�W�F�N�g</param>
    /// <param name="worldPosition">���[���h���W</param>
    /// <returns>�X�N���[�����W</returns>
    private Vector3 WorldToScreenPoint(Camera cam, Vector3 worldPosition)
    {
        // �J������Ԃɕϊ�(�J�������猩�����W�ɕϊ�)
        Vector3 cameraSpace = cam.worldToCameraMatrix.MultiplyPoint(worldPosition);

        // �N���b�s���O��Ԃɕϊ�(cameraSpace�����͈̔͂ɍi���Ă�)
        Vector4 clipSpace = cam.projectionMatrix * new Vector4(cameraSpace.x, cameraSpace.y, cameraSpace.z, 1f);

        //�f�o�C�X���W�F�����[1�@�E��{1
        //�����Ă�̂͐��K��
        // �f�o�C�X���W�n�ɕϊ�
        Vector3 deviceSpace = new Vector3(clipSpace.x / clipSpace.w, clipSpace.y / clipSpace.w, clipSpace.z / clipSpace.w);

        // �X�N���[�����W�n�ɕϊ�
        Vector3 screenSpace = new Vector3((deviceSpace.x + 1f) * 0.25f * Screen.width, (deviceSpace.y + 1f) * 0.5f * Screen.height, deviceSpace.z);

        return screenSpace;
    }

    /// <summary>
    /// �ŏI�I�ȃX�R�A�|�C���g
    /// </summary>
    public float FinalScorePoint(Vector3 scrPoint)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        float finalScore = (float)checkScore(scrPoint);

        //�X�L���ɂ��X�R�A���Z�t���O���I����������if���̏��������s
        if (skillManager.GetAddScoreFlag())
        {
            //�t���O���I����������1.5�{�̃X�R�A��Ԃ�
            return finalScore * raiseScore;
        }
        else
        {
            return finalScore;
        }
    }

    /// <summary>
    /// �����ɂǂꂾ���߂�������
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <returns>�X�R�A�̒l</returns>
    private ScoreType checkScore(Vector3 scrPoint)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        ScoreType defaultScore = lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));

        return defaultScore;
    }
    /// <summary>
    /// �X�R�A�̔���(��)
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <param name="score">�^�e�����Ō����Ƃ��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private ScoreType checkScoreHori(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2)
        {
            return ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth)
        {
            return ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.x - center.x) < areaWidth / 2 + areaWidth * 2)
        {
            return ScoreType.low;
        }
        return ScoreType.outOfScreen;
    }
    /// <summary>
    /// �X�R�A�̔���(�c)
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <param name="score">�^�e�����Ō����Ƃ��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private ScoreType checkScoreVart(Vector3 scrPoint)
    {
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2)
        {
            return ScoreType.high;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight)
        {
            return ScoreType.midle;
        }
        if (Mathf.Abs(scrPoint.y - center.y) < areaHeight / 2 + areaHeight * 2)
        {
            return ScoreType.low;
        }
        return ScoreType.outOfScreen;
    }
    /// <summary>
    /// �Ⴂ�ق��̒l�������߂�
    /// </summary>
    /// <param name="v">�Е��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private ScoreType lower(ScoreType v1, ScoreType v2)
    {
        if (v1 < v2)
        {
            return v1;
        }
        else
        {
            return v2;
        }
    }
    RaycastHit hit;
    private bool createRay()
    {
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.gameObject.transform.position;

        Vector3 direction = diff.normalized;
        //Debug.Log(direction);
        float distance = Vector3.Distance(RespawTarget.GetCurrentTargetObj().transform.position, player.transform.position);
        Debug.DrawRay(player.transform.position, distance * direction, Color.green, 1f);
        if (Physics.Raycast(player.transform.position, direction, out hit, distance))
        {
            //Physics.Raycast(player.transform.position, direction, out hit, distance);
            // spere.transform.position = hit.point;
            Debug.Log("�X�e�[�W�ɓ������Ă�");
            return true;
        }
        Debug.Log("�X�e�[�W�ɓ������ĂȂ�");
        return false;

    }

    #endregion

    #region �َ��Ȃ��̎B�e����

    public bool JudgeSubTarget(Camera camera, GameObject subTargetPos, GameObject playerPos)
    {
        float fov = camera.fieldOfView;
        float judgeRange = Mathf.Cos(Mathf.PI - (((2 * Mathf.PI) - ((fov / 360) * Mathf.PI * 2)) / 2));
        Vector3 playerToSubVec = subTargetPos.transform.position - playerPos.transform.position;
        Vector3 playerForwardVec = camera.transform.forward;   //�O�̂��߃J�����������Ă�������̃x�N�g�����擾
        float dot = Vector3.Dot(playerToSubVec.normalized, playerForwardVec.normalized);

        if (playerToSubVec.magnitude < 7.0f && dot >= judgeRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region �Q�b�^�[�A�Z�b�^�[

    public void SetList(List<GameObject> list) { setterObj = list; }

    public void SetPhotographTargetFlag(bool flag) { noneTargetFlag = flag; }

    #endregion
}
