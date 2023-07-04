using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//ポスト　エフェクトを管理するクラス
//カメラオブジェクトにアタッチ
public class PostEffectController : UniTaskController
{
    [SerializeField] private Material nearTargetetEffect;    //対象が近づいたときに電流を流すためのマテリアル
    [SerializeField] private Material nearTargetetImpact;    //対象が近くにいる時にぼやかすためのマテリアル
    [SerializeField] private Material gaussianBlurMaterial;  //ガウシアンブラーマテリアル
    [SerializeField] private Material mixMaterial;           //テクスチャ合成用マテリアル
    private static bool postEffectFlag;                      //ポストエフェクトをかけるかどうか
    [SerializeField, Range(0, 1)] private float blendTex = 0.5f;  //テクスチャ合成割合（シェーダーの変数にセット）


    private int _Direction;

    private void Start()
    {
        _Direction = Shader.PropertyToID("_Direction"); //プロパティIDを取得
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var initTexture = RenderTexture.GetTemporary(src.width, src.height);
        var finalTexture = RenderTexture.GetTemporary(src.width, src.height);
        /////////横幅を半分にしたレンダーテスクチャを作成（まだ、なにも描かれていない）
        var rth = RenderTexture.GetTemporary(src.width / 2, src.height);

        var h = new Vector2(1, 0); //ブラー方向のベクトル(U方向)
        gaussianBlurMaterial.SetVector(_Direction, h); //シェーダー内の変数にブラー方向を設定

        Graphics.Blit(src, rth, gaussianBlurMaterial);
        /////////

        /////////先のテクスチャサイズから、さらに縦半分にしたレンダーテスクチャを作成（まだ、なにも描かれていない）
        var rtv = RenderTexture.GetTemporary(rth.width, rth.height / 2);

        var v = new Vector2(0, 1);　//ブラー方向のベクトル(V方向)        
        gaussianBlurMaterial.SetVector(_Direction, v); //シェーダー内の変数にブラー方向を設定

        Graphics.Blit(rth, rtv, gaussianBlurMaterial); // ブラー処理を行う
        /////////

        Graphics.Blit(rtv, finalTexture, gaussianBlurMaterial); //元サイズから1/4になったレンダーテクスチャを、元のサイズに戻す


        Graphics.Blit(finalTexture, dest, mixMaterial);

        mixMaterial.SetTexture("_Texture1", src);
        mixMaterial.SetTexture("_Texture2", finalTexture);
        mixMaterial.SetFloat("_Blend", blendTex);
        //まてりある.setTextureして、ソースと合成するようなshaderが必要（上のdestは何かしらのバッファに変更）

        RenderTexture.ReleaseTemporary(rtv); //テンポラリレンダーテスクチャの開放
        RenderTexture.ReleaseTemporary(rth); //開放しないとメモリリークするので注意
        RenderTexture.ReleaseTemporary(initTexture); //開放しないとメモリリークするので注意
        RenderTexture.ReleaseTemporary(finalTexture); //開放しないとメモリリークするので注意
    }

    /// <summary>
    /// ポストエフェクトを実行するかのフラグをセットするための関数
    /// </summary>
    /// <param name="flag">フラグ</param>
    public static void SetPostEffectFlag(bool flag)
    {
        postEffectFlag = flag;
    }



    //private float blend;                                     //シェーダーでα値を徐々に大きくしていくときに使う変数
    //private CancellationToken token;                         //キャンセルトークン

    //private void Start()
    //{
    //    postEffectFlag = true;
    //    UniTaskUpdate(()=> { }, UpdateUniTask, () => { return false; }, token, UniTaskCancellMode.Auto).Forget();
    //}

    //private void OnRenderImage(RenderTexture src, RenderTexture dest)
    //{
    //    //// ポストエフェクトをかけない場合
    //    //if(!postEffectFlag || nearTargetetImpact == null || nearTargetetEffect == null)
    //    //{
    //    //    Graphics.Blit(src, dest);
    //    //    return;
    //    //}
    //    //else if(postEffectFlag)
    //    //{
    //    //    //まず最初に画面をぼかす
    //    //    Graphics.Blit(src, dest, nearTargetetImpact);
    //    //    //ぼかした後ポストエフェクトをかける
    //    //    Graphics.Blit(src, dest, nearTargetetEffect);

    //    //    //ポストエフェクトかける順番を順番をこのようにすることで、後から帯電のポストエフェクトがかかるようになる
    //    //}

    //    //ここで、ガウシアンブラーを適用したテクスチャと元のテクスチャの合成を行う->空気感を出すため
    //    //ぼかした後ポストエフェクトをかける
    //    Graphics.Blit(src, dest, gaussianBlurMaterial);
    //}

    ///// <summary>
    ///// 毎フレーム飛び出される
    ///// </summary>
    //public override void UpdateUniTask()
    //{
    //    //対象のオブジェクトを取得
    //    GameObject targetObject = RespawTarget.GetCurrentTargetObj();

    //    //対象のオブジェクトがnullじゃなくてスキル２が発動していたら
    //    if (targetObject != null && SkillManager.GetSpiritSenceFlag())
    //    {
    //        //プレイヤーと対象の距離を取得
    //        float dis = Vector3.Distance(transform.position, targetObject.transform.position);

    //        //ポストエフェクト再生
    //        SunderManager(dis, Config.detectionTargetDistance);
    //    }
    //    //対象のオブジェクトがnullだったら
    //    else
    //    {
    //        SetPostEffectFlag(false);
    //    }
    //}

    ///// <summary>
    ///// 画面に電流を流すための関数
    ///// </summary>
    ///// <param name="distance">プレイヤーと対象の距離</param>
    ///// <param name="limit">どれぐらい近づいたら電流を流すか</param>
    //private void SunderManager(float distance, float limit)
    //{
    //    if (distance <= limit)
    //    {
    //        //α値を増やす
    //        blend += Time.deltaTime;
    //        //blendが1より大きかったら1を返す
    //        nearTargetetEffect.SetFloat("_Blend", ((blend >= 1) ? 1 : blend));
    //        //ブラーをかける
    //        nearTargetetImpact.SetFloat("_BlurDegree", 0.05f);
    //        SetPostEffectFlag(true);
    //    }
    //    else
    //    {
    //        //全てのパラメーター、マテリアルの値をリセット
    //        blend = 0;
    //        nearTargetetEffect.SetFloat("_Blend", 0);
    //        nearTargetetImpact.SetFloat("_BlurDegree", 0);
    //        SetPostEffectFlag(false);
    //    }
    //}
}
