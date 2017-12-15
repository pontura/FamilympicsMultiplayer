using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;



public class LoginManager : MonoBehaviour
{
    private bool triedToLogin;

    void Start()
    {
      
        if(triedToLogin) return;

        triedToLogin = true;
        print("LoginManager Awake");
        
    }
    
  
}
