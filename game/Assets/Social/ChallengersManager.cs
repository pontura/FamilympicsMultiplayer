using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChallengersManager : MonoBehaviour {

    public bool showFacebookFriends;

    public List<PlayerData> received;
    public List<PlayerData> made;

    [Serializable]
    public class PlayerData
    {
        public string objectID;
        public string facebookID;
        public string playerName;
        public float score;
        public int level;

        public float score2;
        public string winner;
        public bool notificated;
    }
    void Start()
    {
    }
   
    
}
