using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip skill;
    [SerializeField] private AudioClip targetShot;
    [SerializeField] private GameObject timeLimit;
    [SerializeField] private AudioClip plusCountSE;
    [SerializeField] private AudioClip minusCountSE;

    private bool isTimeLimit = false;
    private void oneShot(AudioClip ac)
    {
        audioSource.PlayOneShot(ac);
    }
    /// <summary>
    /// �^�[�Q�b�g���X�|�[������SE
    /// </summary>
    public void PlayRespawn()
    {
        oneShot(respawn);
    }
    /// <summary>
    /// �B�e����SE
    /// </summary>
    public void PlayShot()
    {
        oneShot(shot);
    }
    /// <summary>
    /// �X�L����������SE
    /// </summary>
    public void PlaySkill()
    {
        oneShot(skill);
    }
    /// <summary>
    /// �^�[�Q�b�g�B�e��������SE
    /// </summary>
    public void PlayTargetShot()
    {
        oneShot(targetShot);
    }
    /// <summary>
    /// �^�C���J�E���g���v���X���ꂽ�Ƃ���SE
    /// </summary>
    public void PlayPlusTimeCountSE()
    {
        oneShot(plusCountSE);
    }
    /// <summary>
    /// �^�C���J�E���g���}�C�i�X���ꂽ����SE
    /// </summary>
    public void PlayMinusTimeCountSE()
    {
        oneShot(minusCountSE);
    }
    /// <summary>
    /// �^�C�����~�b�g�ɓ��B����SE
    /// </summary>
    /// <param name="playTime">�Đ�����</param>
    public void PlayTimeLimit(float playTime)
    {        
        if (!isTimeLimit)
        {
            Instantiate(timeLimit);
        }
        isTimeLimit = true;
    }
}
