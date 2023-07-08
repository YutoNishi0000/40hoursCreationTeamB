using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    private AudioSource[] audioSource;
    [SerializeField] private AudioClip respawn;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip skill;
    [SerializeField] private GameObject targetShot;
    [SerializeField] private GameObject timeLimit;
    [SerializeField] private AudioClip plusCountSE;
    [SerializeField] private AudioClip minusCountSE;
    [SerializeField] private AudioClip targetEffectSE;
    //===== �A�E�g�Q�[�� =====
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip decision;
    [SerializeField] private AudioClip back;

    [SerializeField] private readonly int maxAudioSources = 15;     //��������I�[�f�B�I�\�[�X�̍ő吔
    private CancellationToken token;

    private bool isTimeLimit = false;

    private void Start()
    {
        audioSource = new AudioSource[maxAudioSources];
        for(int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    private void oneShot(AudioClip ac)
    {
        AudioSource ad = GetSEAudioSources();
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
        GameObject se = Instantiate(targetShot);
        AudioSource ad = se.GetComponent<AudioSource>();
        UniTaskUpdate(() => { ad.Play(); }, null, () => { ad.Stop(); Destroy(se); }, () => { return (ad.time >= 2); }, token, UniTaskCancellMode.Auto).Forget();
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
    /// <summary>
    /// �^�[�Q�b�g���B�e���ꂽ�Ƃ��ɗ���SE
    /// </summary>
    public void PlayTargetEffectSE()
    {
        oneShot(targetEffectSE);
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

    public AudioSource GetSEAudioSources()
    {
        for(int i = 0; i < audioSource.Length; i++)
        {
            if (!audioSource[i].isPlaying)
            {
                return audioSource[i];
            }
        }

        return null;
    }

}
