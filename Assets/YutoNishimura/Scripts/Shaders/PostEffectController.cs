using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//ポスト　エフェクトを管理するクラス
//カメラオブジェクトにアタッチ
public class PostEffectController : MonoBehaviour
{
    [SerializeField] private Material nearTargetetEffect;

    private static bool postEffectFlag;

    private float blend;                      //シェーダーでα値を徐々に大きくしていくときに使う変数

    private CancellationToken token;

    [SerializeField] private Material nearTargetetImpact;
    [SerializeField, Range(4, 16)] private int _sampleCount = 8;
    [SerializeField, Range(0.0f, 1.0f)] private float _strength = 0.5f;

    delegate void PostEffect();

    private void Start()
    {
        postEffectFlag = true;
        SunderUpdate(token).Forget();
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
        }
    }

    /// <summary>
    /// ポストエフェクトを実行するかのフラグをセットするための関数
    /// </summary>
    /// <param name="flag"></param>
    public static void SetPostEffectFlag(bool flag)
    {
        postEffectFlag = flag;
    }


    //比較的重い処理なので非同期処理を用いて少しでも軽くする
    private async UniTask SunderUpdate(CancellationToken token)
    {
        //自身が破棄されるときにUnitaskを中止するためのキャンセルトークンを取得
        SetCancelToken(ref token);

        while (true)
        {
            GameObject targetObject = RespawTarget.GetCurrentTargetObj();

            if (targetObject != null)
            {
                float dis = Vector3.Distance(transform.position, targetObject.transform.position);

                SunderManager(dis, 20);
            }
            else
            {
                SetPostEffectFlag(false);
            }

            //1フレーム待つ
            await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);
        }
    }

    private void SetCancelToken(ref CancellationToken token)
    {
        //毎回新しいインスタンスを生成しないようにするため参照型を引数として受け取っている
        //メリット：キャンセルトークンをGetSetする必要がなくなる

        //自身が破棄されるときにUnitaskを中止するためのキャンセルトークンを取得
        token = this.GetCancellationTokenOnDestroy();
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
            blend += Time.deltaTime;
            nearTargetetEffect.SetFloat("_Blend", ((blend >= 1) ? 1 : blend));
            nearTargetetImpact.SetFloat("_BlurDegree", 0.05f);
            SetPostEffectFlag(true);
        }
        else
        {
            blend = 0;
            nearTargetetEffect.SetFloat("_Blend", 0);
            nearTargetetImpact.SetFloat("_BlurDegree", 0);
            SetPostEffectFlag(false);
        }
    }
}
