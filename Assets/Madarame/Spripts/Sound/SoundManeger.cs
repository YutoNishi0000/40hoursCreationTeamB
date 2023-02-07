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

	//１つ前のシーン名
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
		//自分と各BGMオブジェクトをシーン切り替え時も破棄しないようにする
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(OutGameBGM.gameObject);
		DontDestroyOnLoad(InGameBGM.gameObject);
		DontDestroyOnLoad(GameClearBGM.gameObject);
		DontDestroyOnLoad(GameOverBGM.gameObject);

		//シーンが切り替わった時に呼ばれるメソッドを登録
		UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
	}

	//シーンが切り替わった時に呼ばれるメソッド
	void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
	{
		//シーンがどう変わったかで判定

		//TitleからDay1へ
		if (beforeScene == "Title" && nextScene.name == "Day1")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day1からDay2へ
		if (beforeScene == "Day1" && nextScene.name == "Day2")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day2からDay3へ
		if (beforeScene == "Day2" && nextScene.name == "Day3")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day3からDay4へ
		if (beforeScene == "Day3" && nextScene.name == "Day4")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day4からDay5へ
		if (beforeScene == "Day4" && nextScene.name == "Day5")
		{
			OutGameBGM.Stop();
			InGameBGM.Play();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//Day5からGameClearへ
		if (beforeScene == "Day5" && nextScene.name == "GameClear")
		{
			OutGameBGM.Stop();
			InGameBGM.Stop();
			GameClearBGM.Play();
			GameOverBGM.Stop();
		}
		//Day5からGameOverへ
		if (beforeScene == "Day5" && nextScene.name == "GameOver")
		{
			OutGameBGM.Stop();
			InGameBGM.Stop();
			GameClearBGM.Stop();
			GameOverBGM.Play();
		}
		//GameClearからTitlerへ
		if (beforeScene == "GameClear" && nextScene.name == "Title")
		{
			OutGameBGM.Play();
			InGameBGM.Stop();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}
		//GameOverからTitler
		if (beforeScene == "GameOver" && nextScene.name == "Title")
		{
			OutGameBGM.Play();
			InGameBGM.Stop();
			GameClearBGM.Stop();
			GameOverBGM.Stop();
		}

		//遷移後のシーン名を「１つ前のシーン名」として保持
		beforeScene = nextScene.name;
	}
}