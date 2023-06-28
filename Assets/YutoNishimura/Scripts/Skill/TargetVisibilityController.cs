using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisibilityController : MonoBehaviour
{
    [SerializeField] private Image ImageUI;    // クロスフェードに使用するImageオブジェクト
    [SerializeField] private Texture Texture1;    // テクスチャ1枚目
    [SerializeField] private Texture Texture2;    // テクスチャ2枚目
    [SerializeField] private Texture Texture3;    //テクスチャ3枚目
    private bool lockVisibilityLevel1;
    private bool lockVisibilityLevel2;
    private CancellationToken token;

    private void Start()
    {
        InitializeShader(Texture1, Texture2);
        lockVisibilityLevel1 = false;
        lockVisibilityLevel2 = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LimitVisibilityManager(GameManager.Instance.numSubShutter);
        }
    }

    /// <summary>
    /// ターゲットの視界を撮影した枚数に応じて変化させる
    /// </summary>
    /// <param name="subCount">異質なものを撮影した枚数</param>
    public void LimitVisibilityManager(int subCount)
    {
        if(subCount == Config.targetVisibilityFirstPhase && !lockVisibilityLevel1)
        {
            //クロスフェード実行
            BlendManager(Texture1, Texture2, token).Forget();
            lockVisibilityLevel1 = true;
        }
        else if(subCount == Config.targetVisibilitySecondPhase && !lockVisibilityLevel2)
        {
            //クロスフェード実行
            BlendManager(Texture2, Texture3, token).Forget();
            lockVisibilityLevel2 = true;
        }
    }

    /// <summary>
    /// テクスチャのブレンドを行う
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    private async UniTask BlendManager(Texture before, Texture after, CancellationToken token)
    {
        //自身が破棄されるときにUnitaskを中止するためのキャンセルトークンを取得
        SetCancelToken(ref token);

        float alpha = 0.0f;
        // Imageのマテリアルを取得
        Material material = ImageUI.material;
        material.SetTexture("_Texture1", before);
        material.SetTexture("_Texture2", after);

        while (true)
        {
            alpha += Time.deltaTime;

            material.SetFloat("_Blend", alpha);

            if(alpha >= 1.0f)
            {
                //ループから抜け出す
                break;
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
    /// シェーダーを初期化する関数
    /// </summary>
    /// <param name="texture1"></param>
    /// <param name="texture2"></param>
    private void InitializeShader(Texture texture1, Texture texture2)
    {
        Material material = ImageUI.material;
        material.SetFloat("_Blend", 0);
        material.SetTexture("_Texture1", texture1);
        material.SetTexture("_Texture2", texture2);
    }
}
