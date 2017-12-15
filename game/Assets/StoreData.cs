using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
																									
public class StoreData : MonoBehaviour
{
    public float secTime = 2.0f;
    public float totTime = 0.0f;

    public bool season_2_unlocked = false;
    public bool season_3_unlocked = false;
    //public bool season_all_unlocked = false;
    static StoreData mInstance = null;

    public static StoreData Instance
    {
        get
        {
            return mInstance;
        }
    }

    void Start()
    {
        if (!mInstance)
            mInstance = this;

        Application.LoadLevel("MainMenu");															
        DontDestroyOnLoad(transform.gameObject);			
		season_2_unlocked = true;
		season_3_unlocked = true;
    }

    public void onSoomlaStoreIntitialized()
    {

    }

   
}