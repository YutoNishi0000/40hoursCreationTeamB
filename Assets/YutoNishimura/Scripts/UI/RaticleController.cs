using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RaticleController : UniTaskController
{
    [SerializeField] private Image raticle;           //‰ñ“]‚³‚¹‚½‚¢UI
    [Header("‰½“x‰ñ“]‚³‚¹‚½‚¢‚©")]
    [SerializeField] private float rotation;  //‰½“x‰ñ“]‚³‚¹‚é‚©
    private Vector3 endRotation;             //‰ñ“]Œã‚Ì‰ñ“]
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
    /// UnitaskController“à‚ÅŒÄ‚Î‚ê‚é
    /// </summary>
    /// <param name="rotation">‚Ç‚ê‚¾‚¯‰ñ“]‚³‚¹‚é‚©</param>
    /// <param name="image">‰ñ“]‚³‚¹‚½‚¢UI</param>
    /// <param name="rotateTime">‰ñ“]ŽžŠÔ</param>
    private void UpdateUniTask(float rotation, Image image, float rotateTime, ChangeColorMode mode)
    {
        endRotation.z = rotation;
        image.rectTransform.DORotate(endRotation, rotateTime);
        blend = Mathf.Lerp(blend, (float)mode, rotateTime);
        image.material.SetFloat("_Trigger", blend);
    }
}
