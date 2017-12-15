
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class NotificationsEnergyPopup : MonoBehaviour {

    public List<string> FriendsThatGaveYouEnergy;
    public GameObject panel;

    [Serializable]
    public class NotificationData
    {
        public string asked_facebookID;
        public string facebookID;
        public string status;
    }    

    void Start()
    {
       
    }
    
}
