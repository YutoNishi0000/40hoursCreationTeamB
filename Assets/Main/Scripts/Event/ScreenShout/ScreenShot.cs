using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

[RequireComponent(typeof(TimerUI))]
public class ScreenShot : MonoBehaviour
{
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
    private bool noneTargetFlag;
    private Player player;
    

    public static int[] abc = new int[10];

    void Awake()
    {
        setterObj = new List<GameObject>();
        destroyStrangeList = new List<int>();
        InitialPrevPos = targetImage.rectTransform.position;
        InitialPrevscale = targetImage.rectTransform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetImage.enabled = false;
        noneStrangeFlag = true;
        noneTargetFlag = true;
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(Shutter.isFilming)
        {
            //�t���O��������
            noneTargetFlag = true;
            noneStrangeFlag = true;

            Instantiate(mimic,
                new Vector3(
                RespawTarget.GetCurrentTargetObj().transform.position.x,
                RespawTarget.GetCurrentTargetObj().transform.position.y,
                RespawTarget.GetCurrentTargetObj().transform.position.z),
                Quaternion.Euler(
                RespawTarget.GetCurrentTargetObj().transform.rotation.x,
                RespawTarget.GetCurrentTargetObj().transform.rotation.y,
                RespawTarget.GetCurrentTargetObj().transform.rotation.z));
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
                    Invoke("startOA", 0.2f);
                }
            }

            //��B��i�َ��Ȃ��́A�^�[�Q�b�g���B�e����Ă��Ȃ��j���Ă�����
            if (noneTargetFlag && noneStrangeFlag && Shutter.isFilming)
            {
                //Debug.Log("���Ԃ������܂���");
                Debug.Log("�����B�e�ł��ĂȂ�");
                CountDownTimer.DecreaceTime();
                player.Shake(duration, magnitude);
                ShutterAnimation.NoneAnimationStart();
            }
        }
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

    public void SetList(List<GameObject> list) { setterObj = list; }

    public void SetDestroyList(List<int> list) { destroyStrangeList = list; }

    public List<int> GetDestroyList() { return destroyStrangeList; }

    public void SetPhotographTargetFlag(bool flag) { noneTargetFlag = flag; }
}
