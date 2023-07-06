using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip skill;
    [SerializeField] private AudioClip targetShot;
    [SerializeField] private GameObject timeLimit;
    [SerializeField] private AudioClip plusCountSE;
    [SerializeField] private AudioClip minusCountSE;
    //===== �A�E�g�Q�[�� =====
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip decision;
    [SerializeField] private AudioClip back;

    [SerializeField] private readonly int maxAudioSources = 10;     //��������I�[�f�B�I�\�[�X�̍ő吔

    private bool isTimeLimit = false;

    private void Start()
    {
        audioSources = new AudioSource[maxAudioSources];
        for (int i = 0; i < audioSources.Length; ++i)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// ���p�\�ȃI�[�f�B�I�\�[�X���擾����
    /// </summary>
    /// <returns></returns>
    private AudioSource GetAvailableAudioSource()
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null)
            {
                Debug.Log("�����������[����null�ł�");
            }

            if (!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }

        return null;
    }

    private void oneShot(AudioClip ac)
    {
        AudioSource ad = GetAvailableAudioSource();
        ad.PlayOneShot(ac);
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
    //===== �A�E�g�Q�[�� =====
    public void PlaySelect()
    {
        oneShot(select);
    }
    public void PlayDecision()
    {
        oneShot(decision);
    }
    public void PlayBack()
    {
        oneShot(back);
    }

}
