using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsInGame : MonoBehaviour {

    public Button restartButton;

	void Start () {
        Events.OpenIngameMenu += OpenIngameMenu;
        if (Data.Instance.tournament.isOn) restartButton.interactable = false;
	}
    void OnDestroy()
    {
        Events.OpenIngameMenu -= OpenIngameMenu;
    }
    public void OpenIngameMenu()
    {
        Time.timeScale = 0;
        GetComponent<AnimationExtensions>().Play( GetComponent<Animation>(), "SettingsOpen", false, () => Debug.Log("onComplete") );
    }
    public void Close()
    {
        Time.timeScale = 1;
        GetComponent<Animation>().Play("SettingsClose");
    }
    public void Challenge()
    {
            Events.OnFacebookNotConnected();
    }
    public void NextRace()
    {
        Debug.Log("NEXTTTT");
        if (Data.Instance.levels.currentLevel == 24)
        {
            Time.timeScale = 1;
            Data.Instance.Load("LevelSelector");
            return;
        }

        if ( !Data.Instance.levels.CanPlayNext() )
        {
            ExitToMap();
            return;
        }
        Data.Instance.levels.currentLevel++;
        Time.timeScale = 1;

        if (Data.Instance.userData.mode == UserData.modes.MULTIPLAYER)
            Data.Instance.Load("Game");
        else Data.Instance.Load("GameSingle");
    }
    public void Restart()
    {

        Time.timeScale = 1;

        if (Data.Instance.userData.mode == UserData.modes.MULTIPLAYER)
            Data.Instance.Load("Game");
        else Data.Instance.Load("GameSingle");
    }
    public void ExitToMap()
    {
        Time.timeScale = 1;
        Data.Instance.Load("LevelSelector");
    }
}
