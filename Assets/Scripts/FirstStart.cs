using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour
{
    void Awake()
    {
        //DEFAULT SETTINGS --- RUNS ONLY ONCE

        if (PlayerPrefs.GetString("OyunDahaOnceAcildimi") != "EVET")
        {
            //TÜM KAYITLARI SİL---------------------

            PlayerPrefs.DeleteKey("OyunDahaOnceAcildimi");
            PlayerPrefs.DeleteKey("SoundOption");
            PlayerPrefs.DeleteKey("MusicOption");
            PlayerPrefs.DeleteKey("ControlTypeOption");
            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.DeleteKey("isPlanetUsed" + i);
                PlayerPrefs.DeleteKey("isPlanetBought" + i);
            }
            for (int j = 0; j < 6; j++)
            {
                PlayerPrefs.DeleteKey("isShipUsed" + j);
                PlayerPrefs.DeleteKey("isShipBought" + j);
            }
            PlayerPrefs.DeleteKey("HighscoreAmount");
            PlayerPrefs.DeleteKey("WalletAmount");

            //YENİ ATAMALAR---------------------
            ;
            PlayerPrefs.SetInt("SavedLevel", 1);
            PlayerPrefs.SetInt("SoundOption", 1);
            PlayerPrefs.SetInt("MusicOption", 1);
            PlayerPrefs.SetInt("ControlTypeOption", 0);     //touch mode

            PlayerPrefs.SetInt("isShipBought" + 0, 1);
            PlayerPrefs.SetInt("isShipUsed" + 0, 1);
            PlayerPrefs.SetInt("isPlanetBought" + 0, 1);
            PlayerPrefs.SetInt("isPlanetUsed" + 0, 1);

            //DENEMELİK-------------------SONRADAN BU KISIM SİLENECEK
            PlayerPrefs.SetInt("WalletAmount", 500);
            PlayerPrefs.SetInt("isPlanetBought" + 1, 1);
            PlayerPrefs.SetInt("isPlanetUsed" + 1, 1);
            PlayerPrefs.SetInt("isPlanetUsed" + 0, 0);
            PlayerPrefs.SetInt("isShipBought" + 2, 1);
            PlayerPrefs.SetInt("isShipUsed" + 2, 1);
            PlayerPrefs.SetInt("isShipUsed" + 0, 0);
            //-------------------

            PlayerPrefs.SetString("OyunDahaOnceAcildimi", "EVET");
        }
    }
}
