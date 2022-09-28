using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject menuBackground, optionsMenu, optionsContent, resetMenu, musicOnButton, musicOffButton, soundOnButton, soundOffButton;
    [SerializeField] GameObject sensorModeButton, touchModeButton;
    [SerializeField] GameObject shopMenuButton;
    [SerializeField] GameObject MusicObject;


    void Start() 
    {
        if(PlayerPrefs.GetInt("SoundOption") == 1)  {SoundOptionOn();}
        else {SoundOptionOff();}
        if(PlayerPrefs.GetInt("MusicOption") == 1)  {MusicOptionOn();}
        else {MusicOptionOff();}
        if(PlayerPrefs.GetInt("ControlTypeOption") == 1)  {SensorModeOptionOn();}
        else {TouchModeOptionOn();}
    }


    public void OpenOptionsMenu()
    {
        menuBackground.SetActive(true);
        optionsContent.SetActive(true);
        optionsMenu.SetActive(true);
        //StartCoroutine("OptionsFadeIn");
        shopMenuButton.SetActive(false);
        resetMenu.SetActive(false);
        SoundFX_Control.instance.PlayButtonSound();
    }

    public void CloseOptionsMenu()
    {
        menuBackground.SetActive(false);
        optionsMenu.SetActive(false);
        //StartCoroutine("OptionsFadeOut");
        shopMenuButton.SetActive(true);
        SoundFX_Control.instance.PlayButtonSound();
    }


    public void SoundOptionOn()
    {
        PlayerPrefs.SetInt("SoundOption", 1);

        //musicOnButton.GetComponent<Button>().interactable = true;
        
        Color objectColor1 = soundOnButton.GetComponent<Image>().color;
        objectColor1.a = 1; 
        Color objectColor2 = soundOffButton.GetComponent<Image>().color;
        objectColor2.a = 0.1f; 
        soundOnButton.GetComponent<Image>().color = objectColor1;
        soundOffButton.GetComponent<Image>().color = objectColor2;
        
        //AudioListener.volume = 1f;
        SoundFX_Control.instance.isSoundOn = true;
        SoundFX_Control.instance.PlayButtonSound();
    }

    public void SoundOptionOff()
    {
        PlayerPrefs.SetInt("SoundOption", 0);
        
        //MusicOptionOff();
        //musicOnButton.GetComponent<Button>().interactable = false;

        Color objectColor1 = soundOffButton.GetComponent<Image>().color;
        objectColor1.a = 1; 
        Color objectColor2 = soundOnButton.GetComponent<Image>().color;
        objectColor2.a = 0.1f; 
        soundOffButton.GetComponent<Image>().color = objectColor1;
        soundOnButton.GetComponent<Image>().color = objectColor2;
        
        //AudioListener.volume = 0f;
        SoundFX_Control.instance.isSoundOn = false;
    }


    public void MusicOptionOn()
    {
        PlayerPrefs.SetInt("MusicOption", 1);
        
        Color objectColor1 = musicOnButton.GetComponent<Image>().color;
        objectColor1.a = 1; 
        Color objectColor2 = musicOffButton.GetComponent<Image>().color;
        objectColor2.a = 0.1f; 
        musicOnButton.GetComponent<Image>().color = objectColor1;
        musicOffButton.GetComponent<Image>().color = objectColor2;

        MusicObject.GetComponent<AudioSource>().Play();
        SoundFX_Control.instance.PlayButtonSound();
    }

    public void MusicOptionOff()
    {
        PlayerPrefs.SetInt("MusicOption", 0);
        
        Color objectColor1 = musicOffButton.GetComponent<Image>().color;
        objectColor1.a = 1; 
        Color objectColor2 = musicOnButton.GetComponent<Image>().color;
        objectColor2.a = 0.1f; 
        musicOffButton.GetComponent<Image>().color = objectColor1;
        musicOnButton.GetComponent<Image>().color = objectColor2;

        MusicObject.GetComponent<AudioSource>().Stop();
        SoundFX_Control.instance.PlayButtonSound();
    }

    public void SensorModeOptionOn()
    {
        PlayerPrefs.SetInt("ControlTypeOption", 1);
        
        Color objectColor1 = sensorModeButton.GetComponent<Image>().color;
        objectColor1.a = 1; 
        Color objectColor2 = touchModeButton.GetComponent<Image>().color;
        objectColor2.a = 0.15f; 
        sensorModeButton.GetComponent<Image>().color = objectColor1;
        touchModeButton.GetComponent<Image>().color = objectColor2;

        Player_Hareket.kontrolYontemi = true;
        SoundFX_Control.instance.PlayButtonSound();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void TouchModeOptionOn()
    {
        PlayerPrefs.SetInt("ControlTypeOption", 0);
        
        Color objectColor1 = touchModeButton.GetComponent<Image>().color;
        objectColor1.a = 1f;
        Color objectColor2 = sensorModeButton.GetComponent<Image>().color;
        objectColor2.a = 0.15f;
        touchModeButton.GetComponent<Image>().color = objectColor1;
        sensorModeButton.GetComponent<Image>().color = objectColor2;

        Player_Hareket.kontrolYontemi = false; 
        SoundFX_Control.instance.PlayButtonSound();
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

    public void ResetButtonPress()
    {
           optionsContent.SetActive(false);
           resetMenu.SetActive(true);
           SoundFX_Control.instance.PlayButtonSound();
    }

    public void ResetButtonYes()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync(0);
    }

    public void ResetButtonNo()
    {
        optionsContent.SetActive(true);
        resetMenu.SetActive(false);
        SoundFX_Control.instance.PlayButtonSound();
    }


    /*
    IEnumerator OptionsFadeIn()
    {
        for (float f = 0f; f<= 1.05f; f += 0.05f)
        {
            Color objectColor = menuBackground.GetComponent<Image>().color;
            objectColor.a = f;
            menuBackground.GetComponent<Image>().color = objectColor;

            Color objectColor2 = optionsMenu.GetComponent<Image>().color;
            objectColor2.a = f;
            optionsMenu.GetComponent<Image>().color = objectColor2;
            
            yield return new WaitForSeconds(0.05f);
        }
    }  


    IEnumerator OptionsFadeOut()
    {
        for (float f = 1f; f>= -0.05f; f -= 0.05f)
        {
            Color objectColor = menuBackground.GetComponent<Image>().color;
            objectColor.a = f;
            menuBackground.GetComponent<Image>().color = objectColor;

            Color objectColor2 = optionsMenu.GetComponent<Image>().color;
            objectColor2.a = f;
            optionsMenu.GetComponent<Image>().color = objectColor2;
            
            yield return new WaitForSeconds(0.05f);
        }
    }  
    */


}
