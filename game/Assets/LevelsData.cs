﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class LevelsData : MonoBehaviour {

    public bool loadingScores;
    public int totalLevels;
    public int scoresLoaded;

    [Serializable]
    public class ScoreData
    {
        //public string objectID;
        public string facebookID;
        public string playerName;
        public float score;
    }
    [Serializable]
    public class LevelsScore
    {
        public bool parseInfoLoaded;
        public List<ScoreData> scoreData;
        public float myScore;
        public float myScoreInParse;
    }

    public List<LevelsScore> levelsScore;
    private int i;
    
    public FacebookFriends facebookFriends;

    void Start()
    {
        Events.OnFacebookLogin += OnFacebookLogin;
        Events.OnFacebookFriends += OnFacebookFriends;
        Events.OnParseLogin += OnParseLogin;
        Events.OnSaveScore += OnSaveScore;
        Events.OnRefreshHiscores += OnRefreshHiscores;
     //   Events.OnLoadParseScore += OnLoadParseScore;
        Events.OnLoadLocalData += OnLoadLocalData;
    }
    public void ResetLoadings()
    {
        loadingScores = false;
        scoresLoaded = 0;
    }
    void OnFacebookLogin()
    {
        //resetea tu score local para poder grabar nuevos scores:
        loadingScores = true;
        Reset();
    }
    public float GetMyScoreIfExists(int levelID)
    {

        if (Data.Instance.userData.facebookID == "")
            return PlayerPrefs.GetFloat("Run_Level_" + levelID);

        levelsScore[levelID].myScore = PlayerPrefs.GetFloat("Run_Level_" + levelID);

        float score = levelsScore[levelID].myScore;
        string facebookID = Data.Instance.userData.facebookID;
        Levels levels = Data.Instance.levels;       

        //chequea de los amigos que llegan si estas vos y tenias un myScore mejor te lo graba:
        foreach (ScoreData scoreData in levelsScore[levelID].scoreData)
        {
            if (scoreData.facebookID == facebookID)
            {
                levelsScore[levelID].myScoreInParse = scoreData.score;

                if (levelsScore[levelID].myScore == 0 || levelsScore[levelID].myScore == levelsScore[levelID].myScoreInParse)
                    levelsScore[levelID].myScore = levelsScore[levelID].myScoreInParse;
                else
                {
                    if (
                        levelsScore[levelID].myScore > levelsScore[levelID].myScoreInParse && levels.levels[levelID].Sudden_Death
                        ||
                        levelsScore[levelID].myScore < levelsScore[levelID].myScoreInParse && levels.levels[levelID].totalLaps > 0
                        ||
                        levelsScore[levelID].myScore > levelsScore[levelID].myScoreInParse && levels.levels[levelID].totalTime > 0
                        )
                    {
                        UpdateScore(levelID, levelsScore[levelID].myScore);
                        scoreData.score = levelsScore[levelID].myScore;                        
                        levelsScore[levelID].myScoreInParse = levelsScore[levelID].myScore;
                    }
                }
                return levelsScore[levelID].myScore;
            }
        }
        if (Data.Instance.userData.facebookID != "" 
            && PlayerPrefs.GetFloat("Run_Level_" + levelID) > 0 
            && levelsScore[levelID].myScore != levelsScore[levelID].myScoreInParse
            && levelsScore[levelID].parseInfoLoaded
            )
        {
            Debug.Log("SaveNewScore local: " + PlayerPrefs.GetFloat("Run_Level_" + levelID) + "  --   myscore: " + levelsScore[levelID].myScore + " myparse SCORE : " + levelsScore[levelID].myScoreInParse);
            levelsScore[levelID].myScore = PlayerPrefs.GetFloat("Run_Level_" + levelID);
            levelsScore[levelID].myScoreInParse = PlayerPrefs.GetFloat("Run_Level_" + levelID);
            SaveNewScore(levelID, levelsScore[levelID].myScore);
        }
        return score;
    }
    void OnLoadLocalData()
    {
        int a = 0;
        foreach(Levels.LevelData data in Data.Instance.levels.levels)
        {
            Data.Instance.levelsData.levelsScore[a].myScore = PlayerPrefs.GetFloat("Run_Level_" + a);
            a++;
        }
    }
    public void Reset()
    {
     //   print("__________________RESET HISCORES_____________________");
        foreach (LevelsData.LevelsScore levelScore in levelsScore)
        {
            levelScore.myScore = 0;
            levelScore.myScoreInParse = 0;
        }
    }
    int loadFriendsAndParseLogged = 0;
    public void OnParseLogin()
    {
        StartCoroutine("WaitUntilUserLoaded");
    }
    IEnumerator WaitUntilUserLoaded()
    {
        while (Data.Instance.userData.facebookID == "")
            yield return null; 

      //  print("________________________OnParseLogin::::::::::::::::::::::::");
        Events.AddFacebookFriend(Data.Instance.userData.facebookID, Data.Instance.userData.username);
        loadFriendsAndParseLogged++;
        CheckIfBothAreReady();
    }
    public void OnFacebookFriends()
    {
       // print("________________________OnFacebookFriends::::::::::::::::::::::::");
        
        foreach (UserData.FacebookUserData facebookUserData in Data.Instance.userData.FacebookFriends)
            Events.AddFacebookFriend(facebookUserData.facebookID, facebookUserData.username);

        loadFriendsAndParseLogged++;
        CheckIfBothAreReady();
    }
    void CheckIfBothAreReady()
    {
       // return;
        if (loadFriendsAndParseLogged < 2) return;

       // print("_________________________CheckIfBothAreReady");
        i = 0;
      // levelsScore = new LevelsScore[Data.Instance.levels.levels.Length];
        totalLevels = Data.Instance.levels.GetTotalLevelsInUnblockedSeasons();
       
       Invoke("LoadNextData", 1f);
    }
    public void Refresh()
    {
        OnFacebookFriends();
    }
    void OnLoadParseScore(int levelID)
    {
        //print("_______________________________OnLoadParseScore " + levelID);

        //levelsScore[levelID].myScoreInParse = 0;
        //var query = new ParseQuery<ParseObject>("Level_" + levelID)
        //      .WhereEqualTo("facebookID", Data.Instance.userData.facebookID);       

        //query.FindAsync().ContinueWith(t =>
        //{
        //    IEnumerable<ParseObject> results = t.Result;
        //    foreach (var result in results)
        //    {
        //        levelsScore[levelID].myScore = float.Parse(result["score"].ToString());
        //        levelsScore[levelID].myScoreInParse = float.Parse(result["score"].ToString());
        //    }
        //});
    }
    void OnRefreshHiscores()
    {
        if (levelsScore != null)
        {
            print("_________OnRefreshHiscores totalLevels: " + totalLevels);
            i = 0;
            Invoke("LoadNextData", 1);
        }
    }
    private void LoadNextData()
    {
        i++;
        if (i < totalLevels)
        {
            LoadData(i);
            Invoke("LoadNextData", 1);
        }
    }
    void OnSaveScore(int level, float score)
    {
      //  print("_________CHECK if SaveScore" + level + " " + score + " myScoreInParse  " + levelsScore[level].myScoreInParse);
        // si es por laps entonces el tiempo tiene que ser menor para grabar el score
        if (Data.Instance.levels.levels[level].totalLaps > 0 && score > levelsScore[level].myScore && levelsScore[level].myScore != 0) { Debug.Log("Ya tenias menos tiempo"); return; }
        if (Data.Instance.levels.levels[level].totalTime > 0 && score < levelsScore[level].myScore) { Debug.Log("Ya hbaias recorrido mas distancia");   return; }

        PlayerPrefs.SetFloat("Run_Level_" + level, score);

        levelsScore[level].myScore = score;

        if (Data.Instance.userData.facebookID == "") return;

        if (levelsScore[level].myScoreInParse == 0)
            SaveNewScore(level, score);
        else
            UpdateScore(level, score);

        RefreshLevelHiscore(level, score);
        
        levelsScore[level].myScoreInParse = score;
        levelsScore[level].myScore = score;

        Events.OnNewHiscore(level, score);
    }
    

    void UpdateScore(int level, float score)
    {
           

    }
    void SaveNewScore(int level, float score)
    {
        ScoreData sd = new ScoreData();
        sd.facebookID = Data.Instance.userData.facebookID;
        sd.playerName = Data.Instance.userData.username;
        sd.score = score;
        levelsScore[level].scoreData.Add(sd);

        if (!levelsScore[level].parseInfoLoaded)
        {
            Debug.Log("Todavia no llego data de parse para este nivel...");
            return;
        }

        Debug.Log("___________SaveNewScore" + level);

    }



    public LevelsScore GetLevelScores(int level)
    {
        try
        {
            return levelsScore[level];
        }
        catch
        {
            return null;
        }
    }

    private void LoadData(int _level)
    {
        
    }


    public string GetScoreString(int levelID, float score)
    {
        if (Data.Instance.levels.levels[levelID].Sudden_Death || Data.Instance.levels.levels[levelID].totalLaps > 0)
        {
            return GetTimer(score);
        }
        else
            return score.ToString() + "m";
    }
    public string GetTimer(float timer)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(timer);
        return string.Format("{0:00}:{1:00}.{2:00}", t.Minutes, t.Seconds, t.Milliseconds/10);
    }
    int lastLevelHiscore;
    void RefreshLevelHiscore(int level, float score)
    {
        print("RefreshLevelHiscore + " + level + " score: " + score);
        lastLevelHiscore = level;
        Invoke("ReloadHiscores", 2);
    }
    void ReloadHiscores()
    {
        LoadData(lastLevelHiscore);
    }
    void AddNewRefreshedData(ScoreData scoreData, float score)
    {
        print("AddNewRefreshedData score: " + score);
        scoreData.score = score;
        scoreData.playerName = Data.Instance.userData.username;
        scoreData.facebookID = Data.Instance.userData.facebookID;
    }
    public void ArrengeListByScore(int levelId)
    {
        levelsScore[levelId].scoreData = levelsScore[levelId].scoreData.OrderBy(x => x.score).ToList();
        if (Data.Instance.levels.levels[levelId].totalTime > 0) levelsScore[levelId].scoreData.Reverse();
    }
}
