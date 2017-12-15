using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour {

    public Button friendsButton;
    public Button loginButton;
    public GameObject masker;
    public GameObject SettingsButton;

	void Start () {
        masker.SetActive(false);
      
            friendsButton.gameObject.SetActive(false);
            SetLoginButton(false);
        Events.OnFacebookLogin += OnFacebookLogin;
    }
    void OnDestroy()
    {
        Events.OnFacebookLogin -= OnFacebookLogin;
    }

    void SetLoginButton(bool logged)
    {
        if(logged)
            loginButton.GetComponentInChildren<Text>().text = "DISCONNECT";
        else
            loginButton.GetComponentInChildren<Text>().text = "CONNECT FACEBOOK";
    }
    public void LoginOrOut()
    {
        
    }
    public void Rules()
    {

    }
    public void Policy()
    {
        Data.Instance.Load("Policy");
    }
    public void Open()
    {
        SettingsButton.SetActive(false);
        masker.SetActive(true);
        GetComponent<Animation>().Play("SettingsOpen");
    }
    public void InviteFriends()
    {
        Events.OnFacebookInviteFriends();
    }
    public void Close()
    {
        SettingsButton.SetActive(true);
        masker.SetActive(false);
        GetComponent<Animation>().Play("SettingsClose");
    }
    void OnFacebookLogin()
    {
        Data.Instance.userData.mode = UserData.modes.SINGLEPLAYER;
        Data.Instance.Load("MainMenu");
    }
    void FBLogin()
    {
    }
    public void RestoreApp()
    {
        Events.OnLoading(true);
    }
    void OnRestoreTransactionsFinished(bool events)
    {
        Events.OnLoading(false);
        if (events)
            Data.Instance.Load("MainMenu");
    }
}
