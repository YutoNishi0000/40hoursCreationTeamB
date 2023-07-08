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

    /// <summary>オプションボタン </summary>    
    [Header("オプションボタン"), SerializeField]
    private PoseUIManager _optionButton;

    /// <summary>オプション画面 </summary>    
    [Header("オプション画面"), SerializeField]
    private GameObject _option;

    /// <summary>オプションのEXITボタン </summary>  
    [Header("オプションのEXITボタン"), SerializeField]
    private HoverableButton _optionExitButton;

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
        if (_selectButton == null || _restartButton == null || _option == null) // || _optionButton == null)
        {
            Debug.LogError("selectUIのインスペクターにButtonを格納してください。");
            return;
        }

        for (int i = 0; i < poseUI.Length; i++)
        {
            poseUI[i] = transform.GetChild(i).gameObject;
        }
        _option.SetActive(false);
        _optionExitButton.AddOnClick(CloseOption);
        HideUI();

        // ボタンクリック時にイベント追加 ---------------------- //
        _selectButton. Button.onClick.AddListener(SelectMove);
        _restartButton.Button.onClick.AddListener(ReStartMove);
        _optionButton. Button.onClick.AddListener(OpenOption);
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
        BGMManager.Instance.BGMAdministrator(SelectSceneIndex);
        SceneManager.LoadScene(SelectSceneIndex);
        IsPosing = false;
    }
    /// <summary> リスタートする </summary>
    private void ReStartMove()
    {
        GameManager.Instance.IsPlayGame = false;
        IsPosing = false;
        SceneManager.LoadScene(ReStartSceneIndex);
        BGMManager.Instance.BGMAdministrator(ReStartSceneIndex);
    }
    /// <summary> オプション画面を開く </summary>
    private void OpenOption()
    {
        _option.SetActive(true);
    }
    /// <summary> オプション画面を閉じる </summary>
    private void CloseOption()
    {
        _option.SetActive(false);
        _optionExitButton.CancelHover();
    }
}
