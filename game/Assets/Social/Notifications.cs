using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Notifications : MonoBehaviour {

    [Serializable]
    public class NotificationData
    {
        public string asked_facebookID;
        public string facebookID;
        public string status;
        public bool notificated;
    }

    public List<NotificationData> notifications;
    public List<NotificationData> notificationsReceived;

    private int lastNotificationsQty;

	void Start () {
        Events.OnNotificationReceived += OnNotificationReceived;
        Events.OnResetApp += OnResetApp;
    }    
    void OnDestroy()
    {
        Events.OnNotificationReceived -= OnNotificationReceived;
        Events.OnResetApp -= OnResetApp;
    }
    void OnResetApp()
    {
        notifications.Clear();
        notificationsReceived.Clear();
    }
    void OnNotificationReceived(string facebookId)
    {
        Debug.Log("OnNotificationReceived from :" + facebookId);
    }
   
}
