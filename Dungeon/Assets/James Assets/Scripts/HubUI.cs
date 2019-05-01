using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    public static bool GameIsPause = false;

    public Button Bag;
    public Button Return1;
    public Button ItemShop;
    public Button Return2;
    public Button Recruit;
    public Button Return3;
    public Button Dungeon;
    public Button Setting;
    public Button Return4;
    public Button Settings;
    public Button Return5;
    public Button Quit;


    public GameObject RecruitMenuUI;
    public GameObject settingMenuUI;
    public GameObject settingUI;
    public GameObject BagMenuUI;
    public GameObject ShopUI;

    // Start is called before the first frame update
    void Start()
    {
        Bag.onClick.AddListener(openBag);
        ItemShop.onClick.AddListener(openItemShop);
        Recruit.onClick.AddListener(OpenRecruit);
        Setting.onClick.AddListener(OpensettingMenuUI);
        Settings.onClick.AddListener(OpensettingUI);
        Return1.onClick.AddListener(BagReturn);
        Return2.onClick.AddListener(ItemShopReturn);
        Return3.onClick.AddListener(RecruitReturn);
        Return4.onClick.AddListener(SettingMenuReturn);
        Return5.onClick.AddListener(SettingUIReturn);
        Dungeon.onClick.AddListener(EnterDungeon);
        Quit.onClick.AddListener(QuitGame);
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

    public void openBag()
    {
        BagMenuUI.SetActive(true);
        GameIsPause = true;
    }

    public void openItemShop()
    {
        ShopUI.SetActive(true);
        GameIsPause = true;
    }

    public void OpenRecruit()
    {
        RecruitMenuUI.SetActive(true);
        GameIsPause = true;
    }

    public void OpensettingMenuUI()
    {
        settingMenuUI.SetActive(true);
        GameIsPause = true;
    }
    public void OpensettingUI()
    {
        settingUI.SetActive(true);
        GameIsPause = true;
    }

    public void BagReturn()
    {
        BagMenuUI.SetActive(false);
        GameIsPause = false;
    }
    public void ItemShopReturn()
    {
        ShopUI.SetActive(false);
        GameIsPause = false;
    }

    public void RecruitReturn()
    {
        RecruitMenuUI.SetActive(false);
        GameIsPause = false;
    }

    public void SettingMenuReturn()
    {
        settingMenuUI.SetActive(false);
        GameIsPause = false;
    }

    public void SettingUIReturn()
    {
        settingUI.SetActive(false);
        GameIsPause = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void EnterDungeon()
    {
        SceneManager.LoadScene("tester");
        Debug.Log("Loading Ingame....");
        GameIsPause = false;
    }

}
