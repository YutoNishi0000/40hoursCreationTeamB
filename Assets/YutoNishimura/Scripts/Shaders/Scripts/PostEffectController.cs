using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//ポスト　エフェクトを管理するクラス
//カメラオブジェクトにアタッチ
public class PostEffectController : UniTaskController
{
    [SerializeField] private Material nearTargetetEffect;

    private static bool postEffectFlag;

    private float blend;                      //シェーダーでα値を徐々に大きくしていくときに使う変数

    private CancellationToken token;

    [SerializeField] private Material nearTargetetImpact;

    delegate void PostEffect();

    private void Start()
    {
        postEffectFlag = true;
        //SunderUpdate(token).Forget();
        UniTaskUpdate(()=> { }, UpdateUniTask, () => { return false; }, token, UniTaskCancellMode.Auto).Forget();
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // ポストエフェクトをかけない場合
        if(!postEffectFlag || nearTargetetImpact == null || nearTargetetEffect == null)
        {
            Graphics.Blit(src, dest);
            return;
        }
        else if(postEffectFlag)
        {
            //まず最初に画面をぼかす
            Graphics.Blit(src, dest, nearTargetetImpact);
            //ぼかした後ポストエフェクトをかける
            Graphics.Blit(src, dest, nearTargetetEffect);

            //ポストエフェクトかける順番を順番をこのようにすることで、後から帯電のポストエフェクトがかかるようになる
        }

        //ここで、ガウシアンブラーを適用したテクスチャと元のテクスチャの合成を行う->空気感を出すため
    }

    /// <summary>
    /// 毎フレーム飛び出される
    /// </summary>
    public override void UpdateUniTask()
    {
        Debug.Log("電流update");
        //対象のオブジェクトを取得
        GameObject targetObject = RespawTarget.GetCurrentTargetObj();

        //対象のオブジェクトがnullじゃなくてスキル２が発動していたら
        if (targetObject != null && SkillManager.GetSpiritSenceFlag())
        {
            //プレイヤーと対象の距離を取得
            float dis = Vector3.Distance(transform.position, targetObject.transform.position);

            //ポストエフェクト再生
            SunderManager(dis, Config.detectionTargetDistance);
        }
        //対象のオブジェクトがnullだったら
        else
        {
            SetPostEffectFlag(false);
        }
    }

    /// <summary>
    /// ポストエフェクトを実行するかのフラグをセットするための関数
    /// </summary>
    /// <param name="flag">フラグ</param>
    public static void SetPostEffectFlag(bool flag)
    {
        Debug.Log("電流start");
        postEffectFlag = flag;
    }

    /// <summary>
    /// 画面に電流を流すための関数
    /// </summary>
    /// <param name="distance">プレイヤーと対象の距離</param>
    /// <param name="limit">どれぐらい近づいたら電流を流すか</param>
    private void SunderManager(float distance, float limit)
    {
        if (distance <= limit)
        {
            //α値を増やす
            blend += Time.deltaTime;
            //blendが1より大きかったら1を返す
            nearTargetetEffect.SetFloat("_Blend", ((blend >= 1) ? 1 : blend));
            //ブラーをかける
            nearTargetetImpact.SetFloat("_BlurDegree", 0.05f);
            SetPostEffectFlag(true);
        }
        else
        {
            //全ての値をリセット
            blend = 0;
            nearTargetetEffect.SetFloat("_Blend", 0);
            nearTargetetImpact.SetFloat("_BlurDegree", 0);
            SetPostEffectFlag(false);
        }
    }
}
