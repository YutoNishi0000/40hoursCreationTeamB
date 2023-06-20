using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetVisibilityController : MonoBehaviour
{
    [SerializeField] private Image ImageUI;    // クロスフェードに使用するImageオブジェクト
    [SerializeField] private Texture Texture1;    // テクスチャ1枚目
    [SerializeField] private Texture Texture2;    // テクスチャ2枚目
    [SerializeField] private Texture Texture3;    //テクスチャ3枚目

    // Start is called before the first frame update
    void Start()
    {
        InitializeShader(Texture1, Texture2);
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
        switch (subCount)
        {
            case 1:
                BlendManager(Texture1, Texture2).Forget();
                break;
            case 2:
                BlendManager(Texture2, Texture3).Forget();
                break;
        }
    }

    /// <summary>
    /// テクスチャのブレンドを行う
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    private async UniTask BlendManager(Texture before, Texture after)
    {
        float alpha = 0.0f;
        // Imageのマテリアルを取得
        Material material = ImageUI.GetComponent<Image>().material;
        material.SetTexture("_Texture1", before);
        material.SetTexture("_Texture2", after);

        while (true)
        {
            alpha += Time.deltaTime;

            material.SetFloat("_Blend", alpha);

            if(alpha >= 1.0f)
            {
                break;
            }

            await UniTask.DelayFrame(1);
        }
    }

    /// <summary>
    /// シェーダーを初期化する関数
    /// </summary>
    /// <param name="texture1"></param>
    /// <param name="texture2"></param>
    private void InitializeShader(Texture texture1, Texture texture2)
    {
        Material material = ImageUI.GetComponent<Image>().material;
        material.SetFloat("_Blend", 0);
        material.SetTexture("_Texture1", texture1);
        material.SetTexture("_Texture2", texture2);
    }
}
