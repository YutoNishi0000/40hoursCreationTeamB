using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDownTimer : UIController
{
	private static float _totalTime;    // 制限時間の合計
	private float _oldSeconds;   // 前回Update時の秒数
	private Text _timerText;
	private int _minute;  // 制限時間（分）
	private float _seconds; // 制限時間（秒）
	private const int sceneIndex = 4;	//リザルトシーンのインデックス 
	//減らす時間
	private static float _decreaceTime = Config.airShutterMinusCount;
	//増やす時間
	private static float _increaceTime = Config.targetShutterPlusCount;
	//アラーム鳴らす時間
	private const float alertTime = 60;
	//制限時間の残り時間のカウントダウンを開始するかどうか
	public static bool startFinishCountDown = false;
	//制限時間の残り時間のカウントダウンを開始する時間(秒)
	private const float startFinishCountTime = 5;

	void Start()
	{
		switch (GameManager.Instance.GetGameMode())
		{
			case GameManager.GameMode.Easy:
				_minute = Config.easyModeTime;
				break;
			case GameManager.GameMode.Nomal:
				_minute = Config.nomalModeTime;
				break;
			case GameManager.GameMode.Hard:
				_minute = Config.hardModeTime;
				break;
		}
		_totalTime = _minute * 60 + _seconds;
		_oldSeconds = 0f;
		_timerText = GetComponentInChildren<Text>();
		ResultScene();
	}

	void Update()
	{
		if(!CountDown.GetFinishCountDown())
		{
			return;
		}
		if(Pose.IsPosing)
        {
			return;
        }
		if(_totalTime <= startFinishCountTime)
        {
			startFinishCountDown = true;
		}
		if(_totalTime <= alertTime)
        {
			SEManager.Instance.PlayTimeLimit(2f);
		}
		//　制限時間が0秒以下なら何もしない
		//　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
		if (_totalTime <= 0f && CountDown.GetFinishCountDown())
		{			
			Debug.Log("制限時間終了");
			InstantAnimation();
			MoveScene(GameManager.Instance.sceneIndex);
			return;
		}
		//　一旦トータルの制限時間を計測；
		_totalTime -= Time.deltaTime;

		//　再設定
		_minute = (int)_totalTime / 60;
		_seconds = _totalTime - _minute * 60;

		//　タイマー表示用UIテキストに時間を表示する
		if ((int)_seconds != (int)_oldSeconds)
		{
			_timerText.text = _minute.ToString("00") + ":" + ((int)_seconds).ToString("00");
		}
		_oldSeconds = _seconds;		
	}

	//タイマーを初期化する関数
	private void InitializeTimer()
	{

	}

	public static void DecreaceTime()
	{
		_totalTime -= _decreaceTime;
	}
	public static void IncreaceTime()
    {
		_totalTime += _increaceTime;
	}

}
