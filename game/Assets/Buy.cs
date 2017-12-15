using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buy : MonoBehaviour {

	void Start () {
       // StoreEvents.OnMarketPurchase += onMarketPurchase;
	}
    void OnDestroy()
    {
      //  StoreEvents.OnMarketPurchase -= onMarketPurchase;
    }
   
}
