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
		if (beforeScene == "Day2" && nextScene.name == "Day3")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day3����Day4��
		if (beforeScene == "Day3" && nextScene.name == "Day4")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day4����Day5��
		if (beforeScene == "Day4" && nextScene.name == "Day5")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day5����GameClear��
		if (beforeScene == "Day5" && nextScene.name == "GameClear")
		{
			OutGameBGM.Stop();
			InGameBGM.Stop();
			GameClearBGM.Play();
			GameOverBGM.Stop();
		}
		//Day5����GameOver��
		if (beforeScene == "Day5" && nextScene.name == "GameOver")
		{
			OutGameBGM.Stop();
			InGameBGM.Stop();
			GameClearBGM.Stop();
			GameOverBGM.Play();
		}
		//GameClear����Titler��
		if (beforeScene == "GameClear" && nextScene.name == "Title")
		{
			OutGameBGM.Play();
			InGameBGM.Stop();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//GameOver����Titler
		if (beforeScene == "GameOver" && nextScene.name == "Title")
		{
			OutGameBGM.Play();
			InGameBGM.Stop();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}

		//�J�ڌ�̃V�[�������u�P�O�̃V�[�����v�Ƃ��ĕێ�
		beforeScene = nextScene.name;
	}
}