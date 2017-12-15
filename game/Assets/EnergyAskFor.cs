using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class EnergyAskFor : MonoBehaviour {


    public string facebookFriendName;
    public string facebookFriendId;

    public List<string> friendsDone;

    bool filterReady;

    public GameObject container;

    [SerializeField]
    EnergyAskButton button;

    private int buttonsSeparation = 80;
   // public List<FriendData> friendsData;

    void Start()
    {
      //  CreateList();
        FilterChellengers();
    }
    
    void FilterChellengers()
    {
       
    }
    void Update()
    {
        if (filterReady)
        {
            filterReady = false;
            CreateList();
        }
    }
    public void CreateList()
    {
        for (int a = 0; a < Data.Instance.userData.FacebookFriends.Count; a++)
        {
            UserData.FacebookUserData data = Data.Instance.userData.FacebookFriends[a];

            EnergyAskButton newButton = Instantiate(button) as EnergyAskButton;
            newButton.transform.SetParent(container.transform);
            newButton.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            string facebookID = data.facebookID;
            bool done = false;
            foreach (string challengesMadeFBId in friendsDone)
            {
                if (challengesMadeFBId == facebookID)
                    done = true;
            }

            newButton.Init(this, a + 1, data.username, facebookID, done);
        }
    }
    public void InviteFriends()
    {
        Events.OnFacebookInviteFriends();
    }

    public void Back()
    {
        Data.Instance.Load("LevelSelector");
    }
    public void Select(string _facebookID, string username)
    {
        Events.SendNotificationTo(_facebookID, username);
        Debug.Log("Select " + _facebookID);
    }
    public void ShowFacebookFriends()
    {
        Data.Instance.GetComponent<ChallengersManager>().showFacebookFriends = true;
        Data.Instance.Load("ChallengeCreator");
    }
    public void Accept()
    {
       // Events.OnChallengeCreate(facebookFriendName, facebookFriendId, levelId, score);
        //CloseOff();
    }  
}
