using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUI : MonoBehaviour
{
    /// <summary>ポーズ中かどうか </summary>
    public bool _isPosing = false;

    /// <summary>背景 </summary>    
    [Header("背景"), SerializeField]
    private PoseUIManager _background;

    /// <summary>セレクトボタン </summary>    
    [Header("セレクトボタン"), SerializeField]
    private PoseUIManager _selectButton;
    
    /// <summary>リスタートボタン </summary>    
    [Header("リスタートボタン"), SerializeField]
    private PoseUIManager _reStartButton;
    
    /// <summary>オプションボタン </summary>    
    [Header("オプションボタン"), SerializeField]
    private PoseUIManager _optionButton;

    /// <summary>セレクトシーンのインデックス番号</summary>
    private const int SelectSceneIndex = 2;

    /// <summary>ゲームシーンのインデックス番号</summary>
    private const int ReStartSceneIndex = 3;
    
    /// <summary>オプションシーンのインデックス番号</summary>
    private const int OptionSceneIndex = 6;

    void Start()
    {
        if (_selectButton == null || _reStartButton == null || _optionButton == null)
        {
            Debug.LogError("selectUIのインスペクターにButtonを格納してください。");
            return;
        }

        _background.HideUI();
        _selectButton.HideUI();
        _reStartButton.HideUI();
        _optionButton.HideUI();
        //HidePoseUI();

        // ボタンクリック時にイベント追加 ---------------------- //
        _selectButton. Button.onClick.AddListener(SelectMove);
        _reStartButton.Button.onClick.AddListener(ReStartMove);
        _optionButton. Button.onClick.AddListener(OptionMove);
        // ----------------------------------------------------- //
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isPosing = true;
            GameManager.Instance.IsPlayGame = false;
            ShowPoseUI();
        }
    }

    /// <summary> ポーズ画面非表示 </summary>
    private void HidePoseUI()
    {
        
    }
    /// <summary> ポーズ画面表示 </summary>
    private void ShowPoseUI()
    {
        _background.ShowUI();
        _selectButton.ShowUI();
        _reStartButton.ShowUI();
        _optionButton.ShowUI();
    }
    /// <summary> セレクト画面に移動 </summary>
    private void SelectMove()
    {
        SceneManager.LoadScene(SelectSceneIndex);
    }
    /// <summary> リスタートする </summary>
    private void ReStartMove()
    {
        SceneManager.LoadScene(ReStartSceneIndex);
    }
    /// <summary> オプション画面に移動 </summary>
    private void OptionMove()
    {
        SceneManager.LoadScene(OptionSceneIndex);
    }
}
