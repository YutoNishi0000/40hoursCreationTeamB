using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	public AudioSource OutGameBGM;
	public AudioSource InGameBGM;
	public AudioSource GameClearBGM;
	public AudioSource GameOverBGM;

	//�P�O�̃V�[����
	private string beforeScene = "Title";

	public static SoundManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start()
	{
		//�����ƊeBGM�I�u�W�F�N�g���V�[���؂�ւ������j�����Ȃ��悤�ɂ���
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(OutGameBGM.gameObject);
		DontDestroyOnLoad(InGameBGM.gameObject);
		DontDestroyOnLoad(GameClearBGM.gameObject);
		DontDestroyOnLoad(GameOverBGM.gameObject);

		//�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h��o�^
		UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
	}

	//�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h
	void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
	{
		//�V�[�����ǂ��ς�������Ŕ���

		//Title����Day1��
		if (beforeScene == "Title" && nextScene.name == "Day1")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day1����Day2��
		if (beforeScene == "Day1" && nextScene.name == "Day2")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day2����Day3��
		if (beforeScene == "Day1" && nextScene.name == "Day2")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day3����Day4��
		if (beforeScene == "Day1" && nextScene.name == "Day2")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day4����Day5��
		if (beforeScene == "Day1" && nextScene.name == "Day2")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day5���烊�U���g��
		if (beforeScene == "MainGame" && nextScene.name == "Result_Hiro")
		{
			//Title_BGM.Stop();
			//Game_BGM.Stop();
			//Result_BGM.Play();
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//���U���g����^�C�g����
		//if (beforeScene == "Result_Hiro" && nextScene.name == "Title_Madarame")
		//{
		//	Title_BGM.Play();
		//	Game_BGM.Stop();
		//	Result_BGM.Stop();
		//}

		//�J�ڌ�̃V�[�������u�P�O�̃V�[�����v�Ƃ��ĕێ�
		beforeScene = nextScene.name;
	}
}