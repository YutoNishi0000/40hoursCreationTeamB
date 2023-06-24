using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�t�F�N�g���N���X
public class EffectController : MonoBehaviour
{
    public ParticleSystem effect;

    //�R���X�g���N�^
    public EffectController(ParticleSystem particle)
    {
        effect = Instantiate(particle);
        effect.Stop();
        effect.gameObject.SetActive(false);
    }

    /// <summary>
    /// �w�肵���ꏊ�ŃG�t�F�N�g���Đ�����
    /// </summary>
    /// <param name="particlePos"></param>
    public void PlayEffect(Vector3 particlePos)
    {
        effect.gameObject.SetActive(true);
        effect.transform.position = particlePos;
        effect.Play();
    }
}