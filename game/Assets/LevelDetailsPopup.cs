﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDetailsPopup : MonoBehaviour {

    public Button challengesButton;
    public GameObject challengesButtonDisable;
    public Text field;
    public Text subTitle;
    public Text goalText;
    public GameObject panel;
    private bool isActive;
    public GameObject Logout;
    private int levelId;
    public Stars stars;
    int starsInLevel;
    public GameObject singlePanel;
    public GameObject multiPanel;
    public Ranking ranking;

	void Start () {
        panel.SetActive(false);
        
	}
    public void StartRace()
    {
        Events.OnSoundFX("buttonPress");
        GetComponent<LevelSelector>().GotoLevel(levelId);
    }
    public void Open(int levelId)
    {
        panel.transform.localScale = Data.Instance.screenManager.scale;

        if (Data.Instance.userData.mode == UserData.modes.SINGLEPLAYER)
        {
            ranking.LoadSinglePlayerWinners(levelId);


            singlePanel.SetActive(true);
            multiPanel.SetActive(false);
        }
        else
        {
            ranking.LoadMultiplayerWinners(levelId);

            singlePanel.SetActive(false);
            multiPanel.SetActive(true);
            Logout.SetActive(false);
        }

        Events.OnSoundFX("buttonPress");
        this.levelId = levelId;
        isActive = true;
        panel.SetActive(true);
        panel.GetComponent<Animation>().Play("PopupOn");
        field.text = "LEVEL " + levelId;

        float _myScore = PlayerPrefs.GetFloat("Run_Level_" + levelId);

        starsInLevel = 0;

        if (_myScore == 0)
            stars.Reset();
        else
        {
            starsInLevel = Data.Instance.levels.GetCurrentLevelStarsByScore(levelId, _myScore);
            stars.Init(starsInLevel);
        }

        string _score = Data.Instance.levelsData.GetScoreString(levelId, _myScore);

        if (Data.Instance.userData.mode == UserData.modes.SINGLEPLAYER)
            subTitle.text = "MY BEST: " + _score;
        else
            subTitle.text = "";

        Levels.LevelData levelData = Data.Instance.levels.levels[levelId];

        if (starsInLevel < 1)
        {
            challengesButtonDisable.SetActive(true);
            challengesButton.interactable = false;
        }
        else
        {
            challengesButton.interactable = true;
            challengesButtonDisable.SetActive(false);
        }

        
        if (levelData.Sudden_Death)
            goalText.text = "SUDDEN DEATH!";
        else if (levelData.totalLaps > 0)
            goalText.text = (levelData.totalLaps * 1000).ToString() + "m";
        else
            goalText.text = (levelData.totalTime).ToString() + " SECONDS";
    }
    public void Close()
    {
        Events.OnSoundFX("buttonPress");
        panel.GetComponent<Animation>().Play("PopupOff");
        Invoke("CloseOff", 0.2f);
    }
    void CloseOff()
    {
        isActive = false;
        panel.SetActive(false);
    }
    public void Login()
    {
        Events.OnFacebookNotConnected();
        Close();
    }
    public void Challenge()
    {
     
    }
}
