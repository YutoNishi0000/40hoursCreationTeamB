using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pose : MonoBehaviour
{
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
    [Header("セレクトシーンのインデックス番号"), SerializeField]
    private int SelectSceneIndex;

    /// <summary>ゲームシーンのインデックス番号</summary>
    [Header("ゲームシーンのインデックス番号"), SerializeField]
    private int ReStartSceneIndex;

    /// <summary>オプションシーンのインデックス番号</summary>
    [Header("オプションシーンのインデックス番号"), SerializeField]
    private int OptionSceneIndex;

    //子オブジェクトの数
    private const int childNum = 4;   
    
    //UIのオブジェクト情報
    private GameObject[] poseUI = new GameObject[childNum];

    //ポーズ中かどうか
    private bool IsPosing = false;

    private void Start()
    {
        if (_selectButton == null || _reStartButton == null || _optionButton == null)
        {
            Debug.LogError("selectUIのインスペクターにButtonを格納してください。");
            return;
        }

        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }

        hideUI();

        // ボタンクリック時にイベント追加 ---------------------- //
        _selectButton.Button.onClick.AddListener(SelectMove);
        _reStartButton.Button.onClick.AddListener(ReStartMove);
        _optionButton.Button.onClick.AddListener(OptionMove);
        // ----------------------------------------------------- //
    }

    private void Update()
    {
        // ===== ポーズ中の処理 =====
        if (IsPosing)
        {
            showUI();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPosing = false;
            }
            return;
        }
        // ===== ポーズじゃないときの処理 =====
        hideUI();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPosing = true;
        }
    }
    /// <summary> ポーズ画面非表示 </summary>
    private void hideUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(false);
        }
    }
    /// <summary> ポーズ画面表示 </summary>
    private void showUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(true);
        }
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
