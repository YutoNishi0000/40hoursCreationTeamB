using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//エフェクト基底クラス
public class EffectController : MonoBehaviour
{
    public ParticleSystem effect;

    //コンストラクタ
    public EffectController(ParticleSystem particle)
    {
        effect = Instantiate(particle);
        effect.Stop();
        effect.gameObject.SetActive(false);
    }

    /// <summary>
    /// 指定した場所でエフェクトを再生する
    /// </summary>
    /// <param name="particlePos"></param>
    public void PlayEffect(Vector3 particlePos)
    {
        effect.gameObject.SetActive(true);
        effect.transform.position = particlePos;
        effect.Play();
    }
}