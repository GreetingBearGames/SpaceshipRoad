using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameButtons : MonoBehaviour
{
    [SerializeField] GameObject pauseWindow, pauseButton, continueButton;


    public void Restart(){
        Time.timeScale = 1;
        SoundFX_Control.instance.PlayButtonSound();
        StartCoroutine(SahneYukleme(1));
    }


    public void TurntoMainMenu(){
        Time.timeScale = 1;
        SoundFX_Control.instance.PlayButtonSound();
        SoundFX_Control.instance.YokEt();
        StartCoroutine(SahneYukleme(0));
    }

    IEnumerator SahneYukleme(int sahnenumarasi)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sahnenumarasi);
    }  


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        SoundFX_Control.instance.PlayButtonSound();
    } 


    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        SoundFX_Control.instance.PlayButtonSound();
    }
}
