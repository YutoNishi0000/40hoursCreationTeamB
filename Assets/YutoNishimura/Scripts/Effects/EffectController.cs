using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//エフェクト基底クラス
public class EffectController : MonoBehaviour
{
    public static ParticleSystem effect;

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
    public static async UniTask PlayEffect(Vector3 particlePos)
    {
        await UniTask.Delay(1500);
        effect.gameObject.SetActive(true);
        effect.transform.position = particlePos;
        effect.Play();
        SEManager.Instance.PlayTargetEffectSE();
    }
}