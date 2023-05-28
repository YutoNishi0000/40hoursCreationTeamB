using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : ButtonController
{
    [Header("横から出てくるバー")]
    [SerializeField] private Image bar;
    [Header("バーの初期位置")]
    [SerializeField] private Image initialPos;
    [Header("バーの移動後の位置")]
    [SerializeField] private Image afterPos;

    private void Start()
    {
        //bar.rectTransform.position = bar.rectTransform.position;
        initialPos.rectTransform.position = bar.rectTransform.position;
        InitializeButton();
    }
    private void Update()
    {
        Debug.Log(GameManager.Instance.blockSwithScene);
        if (GameManager.Instance.blockSwithScene)
        {
            return;
        }
        Debug.Log("シーン切り替え");
        MoveScene(sceneName);
    }
    public void PopUpForHome()
    {
        bar.rectTransform.DOMoveX(afterPos.rectTransform.position.x, changeScaleTime);
    }

    public void PopDownForHome()
    {
        bar.rectTransform.DOMoveX(initialPos.rectTransform.position.x, changeScaleTime);
    }
    
}
