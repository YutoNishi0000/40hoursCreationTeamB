using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    private float _totalTime;    // 制限時間の合計
    private float _oldSeconds;   // 前回Update時の秒数
    private Text _timerText;
    [SerializeField] private int _minute;  // 制限時間（分）
    [SerializeField] private float _seconds; // 制限時間（秒）

    void Start()
    {
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
		_totalTime = _minute * 60 + _seconds;
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
		}
	}
}
