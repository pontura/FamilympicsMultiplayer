using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

    public Text usernameTXT;
    public Text scoreInputTXT;

	void Start () {

	}
	
	public void SendScore () {
	}
    public void backToMain()
    {
        Data.Instance.Load("Menu");
    }
}
