using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

//対象に近づいたとき、画面に電流が流れるようにしたい
//比較的重い処理なのでUnitaskを用いる
public class SunderController : Actor
{
    [SerializeField] private GameObject playerObject;

    private CancellationToken token;

    private void Start()
    {
        SunderUpdate(token).Forget();
    }

    private async UniTask SunderUpdate(CancellationToken token)
    {
        //自身が破棄されるときにUnitaskを中止するためのキャンセルトークンを取得
        SetCancelToken(ref token);

        while (true)
        {
            GameObject targetObject = RespawTarget.GetCurrentTargetObj();

            if(targetObject == null)
            {
                return;
            }

            float dis = Vector3.Distance(transform.position, targetObject.transform.position);

            SunderManager(dis, 100);

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
        if(distance <= limit)
        {
            PostEffectController.SetPostEffectFlag(true);
        }
        else
        {
            PostEffectController.SetPostEffectFlag(false);
        }
    }

}
