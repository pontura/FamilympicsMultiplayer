using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Ranking : MonoBehaviour {

    public RankingLine rankingLine;
    public GameObject container;
    private MultiplayerData multiplayerData;
    private int levelID;

    public List<ScoreData> scoresData;

    [Serializable]
    public class ScoreData
    {
        //public string objectID;
        public string facebookID;
        public string playerName;
        public float score;
    }

	void Start () {
        multiplayerData = Data.Instance.GetComponent<MultiplayerData>();
	}
    public void LoadSinglePlayerWinners(int levelID)
    {
        scoresData.Clear();
        this.levelID = levelID;
        foreach (Transform child in container.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        LoadData(levelID);

        Invoke("LoopUntilDataLoaded", 0.5f);
    }
    void LoopUntilDataLoaded()
    {
        print("LoopUntilDataLoaded");
        if (scoresData.Count > 0)
        {
            foreach (ScoreData scoreData in scoresData)
            {
                AddPlayer(scoreData.playerName, scoreData.score.ToString(), -1, scoreData.facebookID);
            }
        }
        else
        {
            Invoke("LoopUntilDataLoaded", 0.5f);
        }
    }
    public void LoadMultiplayerWinners(int levelID)
    {
        this.levelID = levelID;

        foreach (Transform child in container.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<MultiplayerData.HiscoresData> hiscoreData = multiplayerData.hiscoreLevels[levelID].hiscores;

        foreach (MultiplayerData.HiscoresData data in hiscoreData)
        {
            AddPlayer(data.username, data.score.ToString(), data.playerID, "");
        }
    }


    void AddPlayer(string username, string score, int playerId, string facebookID)
    {
        //Debug.Log(username + " scoRe: " + score + " playerId: " + playerId + " facebookID: " + facebookID);

        RankingLine rl = Instantiate(rankingLine) as RankingLine;
        rl.transform.SetParent(container.transform);
        rl.transform.localScale = Vector3.one;
        rl.Init(levelID, username, score, facebookID);

        if(playerId>-1)
             rl.SetMultiplayerColor(playerId);
        else
            rl.SetSinglePlayer();
    }




    private void LoadData(int _level)
    {

      
    }
   
}
