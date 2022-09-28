using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_to_Game : MonoBehaviour
{
    public static GameObject aktiveGemi;

    void Awake() 
    {        
        for(int i = 0; i < 6; i++)
        {
            GameObject deaktiveGemi = gameObject.transform.GetChild(i).gameObject;
            deaktiveGemi.SetActive(false);

            if(PlayerPrefs.GetInt("isShipUsed" + i) == 1)
            {
                aktiveGemi = gameObject.transform.GetChild(i).gameObject;
                aktiveGemi.SetActive(true);
            }
        }  
    }
}
