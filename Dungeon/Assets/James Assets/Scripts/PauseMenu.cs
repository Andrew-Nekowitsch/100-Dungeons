using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public static bool GameIsPause = false;
	public Button pause;
	public Button resumeGame;
	public Button QuitMission;
	public Button SettingButton;
	public Button SettingReturn;
	public Button Bag;
	public Button Bagclose;
	public Button mapOpen;
	public Button mapClose;
	public GameObject pauseMenuUI;
	public GameObject settingMenuUI;
	public GameObject BagMenuUI;
	public GameObject MapUI;

	void Start()
	{
		pause.onClick.AddListener(SettingPause);
		resumeGame.onClick.AddListener(SettingResume);
		QuitMission.onClick.AddListener(LoadMenu);
		SettingButton.onClick.AddListener(Setting);
		SettingReturn.onClick.AddListener(Setting1);
		Bag.onClick.AddListener(OpenBag);
		Bagclose.onClick.AddListener(CloseBag);
		mapOpen.onClick.AddListener(MapOpen);
		mapClose.onClick.AddListener(MapClose);
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		if (GameIsPause == false)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		GameIsPause = false;
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		GameIsPause = true;
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Hub");
		Debug.Log("Loading Hub....");
		GameIsPause = false;
	}

	public void Setting()
	{
		settingMenuUI.SetActive(true);
	}
	public void Setting1()
	{
		settingMenuUI.SetActive(false);
	}

	public void OpenBag()
	{
		BagMenuUI.SetActive(true);
		GameIsPause = true;
	}

	public void CloseBag()
	{
		BagMenuUI.SetActive(false);
		GameIsPause = false;
	}

	public void SettingPause()
	{
		pauseMenuUI.SetActive(true);
		GameIsPause = true;
	}

	public void SettingResume()
	{
		pauseMenuUI.SetActive(false);
		GameIsPause = false;
	}

	public void MapOpen()
	{
		MapUI.SetActive(true);
		GameIsPause = true;
	}

	public void MapClose()
	{
		MapUI.SetActive(false);
		GameIsPause = false;
	}
}
