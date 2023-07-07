using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pose : MonoBehaviour
{
    /// <summary>セレクトボタン </summary>    
    [Header("セレクトボタン"), SerializeField]
    private PoseUIManager _selectButton;

    /// <summary>リスタートボタン </summary>    
    [Header("リスタートボタン"), SerializeField]
    private PoseUIManager _restartButton;

    ///// <summary>オプションボタン </summary>    
    //[Header("オプションボタン"), SerializeField]
    //private PoseUIManager _optionButton;

    /// <summary>セレクトシーンのインデックス番号</summary>
    [Header("セレクトシーンのインデックス番号"), SerializeField]
    private int SelectSceneIndex;

    /// <summary>ゲームシーンのインデックス番号</summary>
    [Header("ゲームシーンのインデックス番号"), SerializeField]
    private int ReStartSceneIndex;

    ///// <summary>オプションシーンのインデックス番号</summary>
    //[Header("オプションシーンのインデックス番号"), SerializeField]
    //private int OptionSceneIndex;

    /// <summary>ボタン説明 </summary>  
    [Header("ボタン説明"), SerializeField]
    private ButtonGuide _buttonGuide;

    //子オブジェクトの数
    private const int childNum = 8;   
    
    //UIのオブジェクト情報
    private GameObject[] poseUI = new GameObject[childNum];

    //ポーズ中かどうか
    public static bool IsPosing = false;

    private void Start()
    {
        if (_selectButton == null || _restartButton == null) // || _optionButton == null)
        {
            Debug.LogError("selectUIのインスペクターにButtonを格納してください。");
            return;
        }

        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }

        HideUI();

        // ボタンクリック時にイベント追加 ---------------------- //
        _selectButton. Button.onClick.AddListener(SelectMove);
        _restartButton.Button.onClick.AddListener(ReStartMove);
        //_optionButton. Button.onClick.AddListener(OptionMove);
        // ----------------------------------------------------- //
    }

    private void Update()
    {
        // ===== ポーズ中の処理 =====
        if (IsPosing)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HideUI();
                // カーソル非表示
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.Instance.IsPlayGame = true;
                IsPosing = false;
                _buttonGuide.ResetSelect();
            }
            return;
        }
        // ===== ポーズじゃないときの処理 =====
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ShowUI();
            // カーソル非表示
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.IsPlayGame = false;
            IsPosing = true;
        }
    }
    /// <summary> ポーズ画面非表示 </summary>
    private void HideUI()
    {
        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i].SetActive(false);
        }
    }
    /// <summary> ポーズ画面表示 </summary>
    private void ShowUI()
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
        GameManager.Instance.IsPlayGame = false;
        SceneManager.LoadScene(ReStartSceneIndex);
    }
    /// <summary> オプション画面に移動 </summary>
    private void OptionMove()
    {
        SceneManager.LoadScene(OptionSceneIndex);
    }
}
