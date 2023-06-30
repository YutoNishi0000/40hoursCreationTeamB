using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : UIController
{
	private static float _totalTime;    // �������Ԃ̍��v
	private float _oldSeconds;   // �O��Update���̕b��
	private Text _timerText;
	private int _minute;  // �������ԁi���j
	private float _seconds; // �������ԁi�b�j
	private const int sceneIndex = 4;	//���U���g�V�[���̃C���f�b�N�X 
	//���炷����
	private static float _decreaceTime = Config.airShutterMinusCount;
	//���₷����
	private static float _increaceTime = Config.targetShutterPlusCount;

	void Start()
	{
		_seconds = 0;
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

		//�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
		//�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�������ԏI���x�Ƃ����������\������
		if (_totalTime <= 0f)
		{
			SEManager.Instance.PlayTimeLimit(2f);
			Debug.Log("�������ԏI��");
			InstantAnimation();
			MoveScene(GameManager.Instance.sceneIndex);
			return;
		}
		//�@��U�g�[�^���̐������Ԃ��v���G
		_totalTime -= Time.deltaTime;

		//�@�Đݒ�
		_minute = (int)_totalTime / 60;
		_seconds = _totalTime - _minute * 60;

		//�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
		if ((int)_seconds != (int)_oldSeconds)
		{
			_timerText.text = _minute.ToString("00") + ":" + ((int)_seconds).ToString("00");
		}
		_oldSeconds = _seconds;		
	}

	//�^�C�}�[������������֐�
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
