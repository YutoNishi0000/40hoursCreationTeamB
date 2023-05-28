using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
	private enum GameTime
	{
		Easy = 5,
		Nomal = 7,
		Hard = 10
	}

	private static float _totalTime;    // 制限時間の合計
	private float _oldSeconds;   // 前回Update時の秒数
	private Text _timerText;
	private int _minute;  // 制限時間（分）
	private float _seconds; // 制限時間（秒）
	[SerializeField] private string _resultSceneName;  //リザルトシーンの名前

	private GameTime gameTime;
	//減らす時間
	private static float _decreaceTime = 5;
	//増やす時間
	private static float _increaceTime = 5;

	void Start()
	{
		_seconds = 0;
		switch(GameManager.Instance.GetGameMode())
		{
			case GameManager.GameMode.Easy:
				_minute = (int)GameTime.Easy;
				break;
			case GameManager.GameMode.Nomal:
				_minute = (int)GameTime.Nomal;
				break;
			case GameManager.GameMode.Hard:
				_minute = (int)GameTime.Hard;
				break;
		}

		_totalTime = _minute * 60 + _seconds;
		_oldSeconds = 0f;
		_timerText = GetComponentInChildren<Text>();
	}

	void Update()
	{
		//　制限時間が0秒以下なら何もしない
		if (_totalTime <= 0f)
		{
			SEManager.Instance.PlayTimeLimit(2f);
			GameManager.Instance.SetGameOver(true);
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
		//　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
		if (_totalTime <= 0f)
		{
			Debug.Log("制限時間終了");
			SceneManager.LoadScene(_resultSceneName);
		}
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
