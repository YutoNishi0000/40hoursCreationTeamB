using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

[RequireComponent(typeof(TimerUI))]
public class ScreenShot : MonoBehaviour
{
    //��ʂ̒��S
    static Vector3 center = Vector3.zero;
    private const float raiseScore = 1.5f;     //�X�R�A�㏸�{��

    //���͂��K�v�Ȃ���
    [SerializeField] private RawImage targetImage;     //�e�N�X�`����\�����邽�߂�RawImage
    [SerializeField] private Image point1;             //�X�N�V�������摜�̂P�Ԗڂ̈ړ���
    [SerializeField] private Image point2;             //�X�N�V�������摜�̂Q�Ԗڂ̈ړ���
    [SerializeField] private float duration;           //�J�����V�F�C�N�̌p������
    [SerializeField] private float magnitude;          //�J�����V�F�C�N�̗h��̋���

    //���������Ŏg������
    private Camera cam;                                //�v���C���[�̃J����
    private string screenShotPath;                     //�X�N���[���V���b�g���Đ������ꂽ�e�N�X�`���̃t�@�C���p�X
    private string timeStamp;                          //���ݎ�����\�����߂̂���
    private const float firstScale = 0.8f;             //���ڈړ�����Ƃ��ɂǂꂾ��RawImnage���k������邩�i���{�̑傫���ɂȂ邩�j
    private const float secondScale = 0.2f;            //���ڏk������Ƃ��ɂǂꂾ��RawImage���k������邩�i���{�̑傫���ɂȂ邩�j
    private Vector3 InitialPrevPos;                    //RawImage�̏����ʒu
    private Vector3 InitialPrevscale;                  //RawImage�̏����X�P�[��
    private List<GameObject> setterObj;                //���t���[�������Ă���َ��Ȃ��̂̏����擾���邽�߂̂���
    private List<int> destroyStrangeList;
    [SerializeField]private GameObject mimic = null;    //�Ώۂ̃��f��
    private bool noneStrangeFlag;
    public static bool noneTargetFlag;
    private Player player;
    [SerializeField] private Image lostTimeImg;
    float imgTime;
    float a_img;
    private readonly float fadeInSpeed = 10.0f;
    private SkillManager skillManager;

    [SerializeField] private GameObject[] gameUI;     //�ʐ^���B��Ƃ��ɏ�������UI

    //�r���[���W�ɕϊ��������I�u�W�F�N�g�|�W�V����
    [SerializeField] private GameObject obj = null;

    //���ꂼ��̃X�R�A�̒l
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    //�X�R�A�{��(���x���ɉ����Ēl���قȂ�)
    private readonly float Odds_Level1 = 1.2f;
    private readonly float Odds_Level2 = 1.5f;
    private readonly float Odds_Level3 = 2.0f;

    //���ꂼ��̔���̕�
    private const float areaWidth = 192;      //(960 / 5) ���5����
    private const float areaHeight = 216;     //(1080 / 5) ���5����


    public static int[] abc = new int[10];

    void Awake()
    {
        //lostTimeImg.enabled = false;
        imgTime = 0;
        setterObj = new List<GameObject>();
        destroyStrangeList = new List<int>();
        InitialPrevPos = targetImage.rectTransform.position;
        InitialPrevscale = targetImage.rectTransform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage.enabled = false;
        noneStrangeFlag = true;
        noneTargetFlag = true;
        player = GameObject.FindObjectOfType<Player>();
        skillManager = GetComponent<SkillManager>();
    }

    private void Start()
    {
        //getTimeImg.enabled = false;
        //��ʂ̒��S�����߂�
        center = new Vector3(
            areaWidth * 2 + areaWidth * 0.5f,
            areaHeight * 2 + areaHeight * 0.5f,
            0.0f);
    }

    private void Update()
    {
        if (Shutter.isFilming)
        {
            for (int i = 0; i < gameUI.Length; i++)
            {
                gameUI[i].SetActive(false);
            }

            InitializeRawImage();
            ClickShootButton();
            Invoke(nameof(FirstMovePreview), 1f);
            //GameManager.Instance.IsPhoto = true;

            //�T�u�J�����B�e���肪�I���������Ƃ��̔���
            for (int i = 0; i < setterObj.Count; i++)
            {
                if (setterObj[i] != null && setterObj[i].GetComponent<HeterogeneousController>().GetEnableTakePicFlag())
                {
                    Debug.Log("�َ��Ȃ��̎B�e");
        
                    noneStrangeFlag = false;
                    //�T�u�J�����J�E���g���C���N�������g
                    GameManager.Instance.numSubShutter++;
                    //Debug.Log("1");
                    //�X�R�A�����Z
                    ScoreManger.Score += 10;
                    //Debug.Log("2");
                    //tempList[i]�̃I�u�W�F�N�g�̏��Ńt���O���I���ɂ���
                    setterObj[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
                    //Debug.Log("��������");
                    //���X�g�ɂ��̔z��̃C���f�b�N�X��ǉ�
                    //destroyStrangeList.Add(i);
                    //�A�j���[�V����
                    Invoke("startOA", 0.2f);
                }
            }

            //Debug.Log("�ʂ��Ă�(1)");
            //��Q��������Ƃ�
            if (createRay())
            {
                Debug.Log("��Q�a");
                SEManager.Instance.PlayShot();
            }
            else
            {
                switch(checkScore(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position)))
                {
                    case ScoreType.low:
                        ShutterManager();
                        GameManager.Instance.numLowScore++;
                        break;
                    case ScoreType.midle:
                        ShutterManager();
                        GameManager.Instance.numMiddleScore++;
                        break;
                    case ScoreType.high:
                        ShutterManager();
                        GameManager.Instance.numHighScore++;
                        break;
                    case ScoreType.outOfScreen:
                        SEManager.Instance.PlayShot();
                        ScreenShot.noneTargetFlag = true;
                        Debug.Log("��ʊO");
                        break;
                }
                ////�^�[�Q�b�g����ʊO��
                //if ( == ScoreType.outOfScreen)
                //{
                //    SEManager.Instance.PlayShot();
                //    ScreenShot.noneTargetFlag = true;
                //    Debug.Log("��ʊO");
                //}
                //else
                //{

                //}
            }

            //��B��i�َ��Ȃ��́A�^�[�Q�b�g���B�e����Ă��Ȃ��j���Ă�����
            if (noneTargetFlag && noneStrangeFlag && Shutter.isFilming)
            {
                //Debug.Log("���Ԃ������܂���");
                Debug.Log("�����B�e�ł��ĂȂ�");
                CountDownTimer.DecreaceTime();
                player.Shake(duration, magnitude);
                ShutterAnimation.NoneAnimationStart();
                TimerUI.FadeOut(false);
                SEManager.Instance.PlayPlusTimeCountSE();
                //�A�j���[�V����
                Invoke("startNA", 0.2f);
            }
            //�t���O��������
            noneTargetFlag = true;
            noneStrangeFlag = true;

            Invoke(nameof(OnUIShutter), 0.2f);
            Shutter.isFilming = false;
        }
    }

    //�B�e���̏���
    public void ShutterManager()
    {
        Transform transform = RespawTarget.GetCurrentTargetObj().transform;
        Instantiate(mimic, transform.position, transform.rotation);
        ScreenShot.noneTargetFlag = false;
        //���Ԃ��l��
        CountDownTimer.IncreaceTime();
        Debug.Log("���Ԃ��l�����܂���");
        //�^�[�Q�b�g���B�e���ꂽ
        SetPhotographTargetFlag(false);
        //��Q�����Ȃ��Ƃ��̏���
        Debug.Log("�B�e����");
        //�X�R�A���Z
        ScoreManger.Score += FinalScorePoint(WorldToScreenPoint(cam, RespawTarget.GetCurrentTargetObj().transform.position));
        ScoreManger.ShotMainTarget = true;
        TargetManager.IsSpawn = true;
        //�Ώۂ��B�e�����񐔂��C���N�������g
        GameManager.Instance.numTargetShutter++;
        //SE
        SEManager.Instance.PlayTargetShot();
        //�^�[�Q�b�g���X�|�[��
        RespawTarget.RespawnTarget();
        //�A�j���[�V�����J�n
        Invoke("startTA", 0.2f);
        TimerUI.FadeOut(true);
        SEManager.Instance.PlayMinusTimeCountSE();
    }

    //�B�e����u�Ԕ�\���ɂ��ꂽUI��\������֐��iInvoke�ŌĂԁj
    public void OnUIShutter()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(true);
        }
    }


    //�����Image��\���A��\������֐�
    public void UIManager_Image(Image img, bool isActive)
    {
        img.enabled = isActive;
    }

    //�����Text��\���A��\������֐�
    public void UIManager_Text(Text text, bool isActive)
    {
        text.enabled = isActive;
    }

    private void InitializeRawImage()
    {
        targetImage.texture = null;
        targetImage.enabled = false;
        targetImage.rectTransform.position = InitialPrevPos;
        targetImage.rectTransform.localScale = InitialPrevscale;
    }

    private string GetScreenShotPath()
    {
        //string path = "Assets/Pictures/" + timeStamp + ".png";
        string path = GameManager.Instance.GetPicturesFilePath() + timeStamp + ".png";

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




        //================================================================================================================================================
        //
        //   �����B�e�N�X�`�����k�߂��Ă����Ԃ������牺�̃R�����g�����Ă��镔���i�u / 2�v�j�̂Ƃ�����͂����Ă݂āj
        //
        //================================================================================================================================================




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

            //�e�N�X�`������ǂݍ��񂾌��RawImage��\������
            targetImage.enabled = true;
        }
    }

    private void FirstMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * firstScale, 0.5f);
        Invoke(nameof(SecondMovePreview), 0.5f);
    }

    private void SecondMovePreview()
    {
        targetImage.rectTransform.DOScale(transform.localScale * secondScale, 0.5f);
        targetImage.rectTransform.DOMove(point1.rectTransform.position, 0.5f);
        Invoke(nameof(SlideMovePreview), 1f);
    }

    private void SlideMovePreview()
    {
        targetImage.transform.DOMoveX(point2.rectTransform.position.x, 0.3f);
    }
    void startOA()
    {
        ShutterAnimation.OtherAnimationStart();
    }
    void startNA()
    {
        ShutterAnimation.NoneAnimationStart();
    }

    public void SetList(List<GameObject> list) { setterObj = list; }

    public void SetDestroyList(List<int> list) { destroyStrangeList = list; }

    public List<int> GetDestroyList() { return destroyStrangeList; }

    public void SetPhotographTargetFlag(bool flag) { noneTargetFlag = flag; }





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
        Vector3 diff = RespawTarget.GetCurrentTargetObj().transform.position - player.transform.position;

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
    void startTA()
    {
        ShutterAnimation.TargetAnimationStart();
    }
}
