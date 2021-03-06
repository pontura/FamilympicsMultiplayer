﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    public bool FreeLevels;
    public bool OnlyMultiplayer;

    public float musicVolume = 1;
    public float soundsVolume = 1;

    const string PREFAB_PATH = "Data";

    static Data mInstance = null;

    public List<Color> colors; 
    [HideInInspector]
    public LevelData levelData;
    [HideInInspector]
    public Levels levels;
    [HideInInspector]
    public LevelsData levelsData;
    [HideInInspector]
    public MultiplayerData multiplayerData;

     [HideInInspector]
    public UserData userData;
     [HideInInspector]
    public LoginManager loginManager;
     [HideInInspector]
    public string lastScene;
     [HideInInspector]
    public GameSettings gameSettings;
     [HideInInspector]
    public MusicManager musicManager;
     [HideInInspector]
    public SoundManager soundManager;
     [HideInInspector]
    public Notifications notifications;
     [HideInInspector]
     public FacebookShare facebookShare;
     [HideInInspector]
     public ScreenManager screenManager;
     [HideInInspector]
     public ChallengersManager challengesManager;
     [HideInInspector]
     public FacebookFriends facebookFriends;
     [HideInInspector]
     public Tournament tournament;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                }
            }
            return mInstance;
        }
    }

    void Awake()
    {

        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1); 
        soundsVolume = PlayerPrefs.GetFloat("soundsVolume", 1); 

        // PlayerPrefs.DeleteAll();

        if (!mInstance)
            mInstance = this;

        else
        {
            Destroy(this.gameObject);
            return;
        }
        gameSettings = GetComponent<GameSettings>();
        loginManager = GetComponent<LoginManager>();
        levelData = GetComponent<LevelData>();
        levels = GetComponent<Levels>();
        levelsData = GetComponent<LevelsData>();
        multiplayerData = GetComponent<MultiplayerData>();
        musicManager = GetComponent<MusicManager>();
        soundManager = GetComponentInChildren<SoundManager>();
        notifications = GetComponent<Notifications>();
        facebookShare = GetComponent<FacebookShare>();
        screenManager = GetComponent<ScreenManager>();
        challengesManager = GetComponent<ChallengersManager>();
        facebookFriends = GetComponent<FacebookFriends>();
        tournament = GetComponent<Tournament>();
       // levelsData.Init();

        DontDestroyOnLoad(this.gameObject);

        userData = GetComponent<UserData>();
        userData.Init();
        multiplayerData.Init();

        GetComponent<MusicManager>().Init();

        Events.ResetApp += ResetApp;
    }
    void OnMusicVolumeChanged(float value)
    {
        musicVolume = value;
    }
    void OnSoundsVolumeChanged(float value)
    {
        soundsVolume = value;
    }
    void OnSaveVolumes(float _musicVolume, float _soundsVolume)
    {
        this.musicVolume = _musicVolume;
        this.soundsVolume = _soundsVolume;
    }    
    public void Load(string nextScene)
    {
        Events.OnLoading(false);
        lastScene = Application.loadedLevelName;
        if (nextScene == "") nextScene = "LevelSelector";
        Debug.Log("Load Scene: " + nextScene);
        Application.LoadLevel(nextScene);
    }
    public void Back()
    {
        Load(lastScene);
    }
    public void ResetApp()
    {
        PlayerPrefs.DeleteAll();
        Data.Instance.levelsData.Reset();
        Data.Instance.userData.Reset();
        Data.Instance.multiplayerData.Reset();
        Events.OnResetApp();
    }

}
