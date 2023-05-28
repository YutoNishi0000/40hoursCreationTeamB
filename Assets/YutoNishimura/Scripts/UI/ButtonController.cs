using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;


public class ButtonController : UIController
{
    [Header("何秒でポップアップ、ポップダウン、バーが移動するか")]
    [SerializeField] protected float changeScaleTime = 0.1f;
    [Header("ポップアップ後の大きさ（倍率）")]
    [SerializeField] protected float afterScale = 1.5f;
    [Header("ポップアップ前のスプライト")]
    [SerializeField] protected Sprite initialImage;
    [Header("ポップアップ後のボタンのイメージ")]
    [SerializeField] protected Sprite afterImage;

    //内部処理で使う変数
    protected Image tempButton;
    private readonly Vector3 normalVec = new Vector3(1, 1, 1);
    

    private void Start()
    {
        InitializeButton();
    }
    
    //継承先のStart関数、またはAwake関数内で必ず呼び出さなければならない関数
    protected void InitializeButton()
    {
        tempButton = this.GetComponent<Image>();
    }
    public void PopUp()
    {
        tempButton.sprite = afterImage;
        tempButton.rectTransform.DOScale(normalVec * afterScale, changeScaleTime);
    }

    public void PopDown()
    {
        tempButton.sprite = initialImage;
        tempButton.rectTransform.DOScale(normalVec, changeScaleTime);
    }
}
