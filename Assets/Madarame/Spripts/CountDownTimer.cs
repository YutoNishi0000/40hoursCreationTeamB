using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : UIController
{
	private enum GameTime
	{
		Easy = 5,
		Nomal = 7,
		Hard = 10
	}

	private static float _totalTime;    // �������Ԃ̍��v
	private float _oldSeconds;   // �O��Update���̕b��
	private Text _timerText;
	private int _minute;  // �������ԁi���j
	private float _seconds; // �������ԁi�b�j
	private const int sceneIndex = 4;	//���U���g�V�[���̃C���f�b�N�X 

	private GameTime gameTime;
	//���炷����
	private static float _decreaceTime = 5;
	//���₷����
	private static float _increaceTime = 5;

	void Start()
	{
		_seconds = 10;
        //switch (GameManager.Instance.GetGameMode())
        //{
        //    case GameManager.GameMode.Easy:
        //        _minute = (int)GameTime.Easy;
        //        break;
        //    case GameManager.GameMode.Nomal:
        //        _minute = (int)GameTime.Nomal;
        //        break;
        //    case GameManager.GameMode.Hard:
        //        _minute = (int)GameTime.Hard;
        //        break;
        //}
        _totalTime = _minute * 60 + _seconds;
		_oldSeconds = 0f;
		_timerText = GetComponentInChildren<Text>();
		ResultScene();
	}

	void Update()
	{
		Debug.Log(GameManager.Instance.sceneIndex);
		//�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
		//�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�������ԏI���x�Ƃ����������\������
		if (_totalTime <= 0f)
		{
			SEManager.Instance.PlayTimeLimit(2f);
			Debug.Log("�������ԏI��");
			InstantAnimation();
<<<<<<< HEAD
			MoveScene(GameManager.Instance.sceneIndex);
=======
			
			if (!GameManager.Instance.blockSwithScene)
			{
				MoveScene(GameManager.Instance.sceneIndex);
			}
>>>>>>> origin/main
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
