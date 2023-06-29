using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class JudgeScreenShot : MonoBehaviour
{
    public JudgeTarget judgeTarget;
    public JudgeSubTarget judgeSubTarget;

    //�R���X�g���N�^
    public JudgeScreenShot(ParticleSystem targetEffect)
    {
        judgeTarget = new JudgeTarget(targetEffect);
        judgeSubTarget = new JudgeSubTarget();
    }
}

//�^�[�Q�b�g���肾�����s���N���X
public class JudgeTarget : MonoBehaviour
{
    private EffectController effectController;

    //�R���X�g���N�^
    public JudgeTarget(ParticleSystem particle)
    {
        effectController = new EffectController(particle);
    }

    //���ꂼ��̃X�R�A�̒l
    public enum ScoreType
    {
        high = 50,
        midle = 30,
        low = 10,
        outOfScreen = 0,
    };

    /// <summary>
    /// �^�[�Q�b�g���B�e�ł��Ă��邩
    /// </summary>
    /// <param name="player">�v���C���[�I�u�W�F�N�g</param>
    /// <param name="targetModel">�B�e���ɕ\���������I�u�W�F�N�g</param>
    /// <param name="cam">�v���C���[�J�����I�u�W�F�N�g</param>
    /// <param name="worldPosition">�^�[�Q�b�g�̃��[���h���W</param>
    /// <param name="center">�v���C���[��ʃJ�����̒��S</param>
    /// <param name="areaWidth">�v���C���[��ʂ̕�</param>
    /// <param name="areaHeight">�v���C���[��ʂ̍���</param>
    /// <param name="raise">�X�R�A�{��</param>
    /// <returns>�B�e�����Ftrue�A�B�e���s�Ffalse</returns>
    public bool ShutterTarget(
        GameObject player,
        GameObject targetModel,
        Camera cam,
        Vector3 worldPosition,
        Vector3 center,
        float areaWidth,
        float areaHeight,
        float raise)
    {
        //SE�Đ�
        SEManager.Instance.PlayShot();
        //�v���C���[�ƃ^�[�Q�b�g�̊ԂɃI�u�W�F�N�g�����邩�ǂ���
        //===== �ԂɃI�u�W�F�N�g������Ƃ� =====
        if (createRay(player))
        {
            return false;
        }
        //===== �ԂɃI�u�W�F�N�g���Ȃ��Ƃ� =====
        //�^�[�Q�b�g�����S�ɂǂꂾ���߂����ɂ���ăX�R�A�����߂�
        ScoreType finalScore 
            = checkScore(WorldToScreenPoint(cam, worldPosition), center, areaWidth, areaHeight);

        switch (finalScore)
        {
            case ScoreType.low:
                TargetProcess(targetModel, ScoreType.low, raise);
                return true;
            case ScoreType.midle:
                TargetProcess(targetModel, ScoreType.midle, raise);
                return true;
            case ScoreType.high:
                TargetProcess(targetModel, ScoreType.high, raise);
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// �Ώۂ��B�e�����Ƃ��̍Œ���̏���
    /// </summary>
    /// <param name="targetModel">�B�e���Ɏʐ^�Ɏʂ������I�u�W�F�N�g</param>
    /// <param name="type">enum�^�̂��ꂼ��̃X�R�A���Z��</param>
    /// <param name="raise">�X�R�A���Z�{��</param>
    private void TargetProcess(GameObject targetModel, ScoreType type, float raise)
    {
        GameObject targetObj = RespawTarget.GetCurrentTargetObj();
        //�蓮�Ń^�[�Q�b�g���\�������ʒu�𒲐�
        Vector3 targetPos = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y - 1, targetObj.transform.position.z);
        Instantiate(targetModel, targetPos, targetObj.transform.rotation);
        //�G�t�F�N�g���Đ�
        effectController.PlayEffect(targetPos);
        //�X�R�A���Z
        ScoreManger.Score += GetFinalScore(type);
        //���Ԃ��l��
        CountDownTimer.IncreaceTime();
        //�Ώۂ��B�e�����񐔂��C���N�������g
        GameManager.Instance.numTargetShutter++;
        //SE
        SEManager.Instance.PlayTargetShot();
        SEManager.Instance.PlayPlusTimeCountSE();
        //�^�[�Q�b�g���X�|�[��
        RespawTarget.RespawnTarget();
    }

    /// <summary>
    /// �X�R�A�{�����܂߂��ŏI�I�ȃX�R�A
    /// </summary>
    /// <param name="type">enum�^�̂��ꂼ��̃X�R�A���Z��</param>
    /// <param name="flag">�X�R�A�A�b�v�t���O</param>
    /// <param name="raise">�X�R�A�A�b�v�{��</param>
    /// <returns></returns>
    public float GetFinalScore(ScoreType type)
    {
        switch (type)
        {
            case ScoreType.low:
                GameManager.Instance.numLowScore++;
                return (float)ScoreType.low;
            case ScoreType.midle:
                GameManager.Instance.numMiddleScore++;
                return (float)ScoreType.midle;
            case ScoreType.high:
                GameManager.Instance.numHighScore++;
                return (float)ScoreType.high;
            default:
                return 0;
        }
    }

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
    /// �����ɂǂꂾ���߂�������
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <returns>�X�R�A�̒l</returns>
    private ScoreType checkScore(Vector3 scrPoint, Vector3 center, float areaWidth, float areaHeight)
    {
        //return lower(checkScoreHori(scrPoint), checkScoreVart(scrPoint));
        ScoreType defaultScore = lower(checkScoreHori(scrPoint, center, areaWidth), checkScoreVart(scrPoint, center, areaHeight));

        return defaultScore;
    }
    /// <summary>
    /// �X�R�A�̔���(��)
    /// </summary>
    /// <param name="scrPoint">�X�N���[�����W</param>
    /// <param name="score">�^�e�����Ō����Ƃ��̃X�R�A</param>
    /// <returns>�X�R�A</returns>
    private ScoreType checkScoreHori(Vector3 scrPoint, Vector3 center, float areaWidth)
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
    private ScoreType checkScoreVart(Vector3 scrPoint, Vector3 center, float areaHeight)
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
    public bool createRay(GameObject player)
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
}

//�َ��Ȃ��̂̔��肾�����s���N���X
public class JudgeSubTarget : MonoBehaviour
{
    //����g��
    public bool ShutterSubTargets(Camera camera, GameObject playerPos, List<GameObject> list, float judgeDis)
    {
        float fov = camera.fieldOfView;
        //fov��p���ē��ς��擾
        float judgeRange = Mathf.Cos(Mathf.PI - (((2 * Mathf.PI) - ((fov / 360) * Mathf.PI * 2)) / 2));
        //�O�̂��߃J�����������Ă�������̃x�N�g�����擾
        Vector3 playerForwardVec = camera.transform.forward;   
        int tempNumSubTargets = GameManager.Instance.numSubShutter;

        //�َ��Ȃ��̂̌���������s��
        for (int i = 0; i < list.Count; i++)
        {
            //�َ��Ȃ��̂����݂��Ă��邱�Ƃ�����
            if (list[i] != null)
            {
                //�v���C���[�ƈَ��Ȃ��̂Ƃ̃x�N�g�����擾
                Vector3 playerToSubVec = list[i].transform.position - playerPos.transform.position;
                //�J�����������Ă�������ƍ����������߂��v���C���[�ƈَ��Ȃ��̊Ԃ̃x�N�g���̓��ς��擾
                float dot = Vector3.Dot(playerToSubVec.normalized, playerForwardVec.normalized);

                //fov��p���Ď擾�������ςƍ����������߂����ς��r�i�v���C���[�ƈَ��Ȃ��̊Ԃ̃x�N�g���̓��ς�fov��p���Ď擾�������ς��傫��������B�e�����j<=�O�p�֐��̊T�O
                if (playerToSubVec.magnitude < judgeDis && dot >= judgeRange)
                {
                    //�T�u�J�����J�E���g���C���N�������g
                    GameManager.Instance.numSubShutter++;
                    //�X�R�A�����Z
                    ScoreManger.Score += 10;
                    //tempList[i]�̃I�u�W�F�N�g�̏��Ńt���O���I���ɂ� 
                    list[i].GetComponent<HeterogeneousController>().SetTakenPicFlag(true);
                }
            }
        }

        //��̏�����ʂ��āA�B�e�����َ��Ȃ��̂̌��������Ă�����
        if ((GameManager.Instance.numSubShutter - tempNumSubTargets) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}