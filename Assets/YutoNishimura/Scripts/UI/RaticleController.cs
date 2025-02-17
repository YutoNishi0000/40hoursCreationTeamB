using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RaticleController : UniTaskController
{
    [SerializeField] private Image raticle;           //回転させたいUI
    [Header("何度回転させたいか")]
    [SerializeField] private float rotation;  //何度回転させるか
    private Vector3 endRotation;             //回転後の回転
    private CancellationToken token;
    private float blend;

    private enum ChangeColorMode
    {
        NOMAL,
        YELLOW,
    }

    // Start is called before the first frame update
    void Start()
    {
        endRotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(ScreenShot.GetJudgeSubTargetFlag())
        {
            UniTaskUpdate(null, () => UpdateUniTask(rotation, raticle, 3, ChangeColorMode.YELLOW), null, () => { return (ScreenShot.GetJudgeSubTargetFlag() == false); }, token, UniTaskCancellMode.Auto).Forget();
        }
        else
        {
            UniTaskUpdate(null, () => UpdateUniTask(0, raticle, 3, ChangeColorMode.NOMAL), null, () => { return (ScreenShot.GetJudgeSubTargetFlag() == true); }, token, UniTaskCancellMode.Auto).Forget();
        }
    }

    /// <summary>
    /// UnitaskController内で呼ばれる
    /// </summary>
    /// <param name="rotation">どれだけ回転させるか</param>
    /// <param name="image">回転させたいUI</param>
    /// <param name="rotateTime">回転時間</param>
    private void UpdateUniTask(float rotation, Image image, float rotateTime, ChangeColorMode mode)
    {
        endRotation.z = rotation;
        image.rectTransform.DORotate(endRotation, rotateTime);
        blend = Mathf.Lerp(blend, (float)mode, rotateTime);
        image.material.SetFloat("_Trigger", blend);
    }
}
