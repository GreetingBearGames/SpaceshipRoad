using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameButtons : MonoBehaviour
{
    [SerializeField] GameObject pauseWindow, pauseButton, continueButton, optionsButton;

    [SerializeField] GameObject LoadingScreen;
    //[SerializeField] Image LoadingBarFill;
    [SerializeField] Slider loadingSlider;


    public void Restart()
    {
        Time.timeScale = 1;
        SoundFX_Control.instance.PlayButtonSound();
        LoadingScreen.SetActive(true);
        StartCoroutine(SahneYukleme(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
        SoundFX_Control.instance.PlayButtonSound();
        LoadingScreen.SetActive(true);
        StartCoroutine(SahneYukleme(SceneManager.GetActiveScene().buildIndex + 1));
    }


    public void TurntoMainMenu()
    {
        Time.timeScale = 1;
        SoundFX_Control.instance.PlayButtonSound();
        SoundFX_Control.instance.YokEt();
        StartCoroutine(SahneYukleme(0));
    }

    IEnumerator SahneYukleme(int sahnenumarasi)
    {
        float progress = 0f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sahnenumarasi);
        asyncLoad.allowSceneActivation = false; //scene will not load while this value is false

        while (progress <= 1f)
        {
            loadingSlider.value = progress;
            progress += 0.025f;
            yield return null;
        }

        if (!asyncLoad.isDone && progress >= 1f)    //yeni sahne yüklenebilir
        {
            asyncLoad.allowSceneActivation = true; //here the scene is definitely loaded.
        }
    }



    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        optionsButton.SetActive(true);
        SoundFX_Control.instance.PlayButtonSound();
    }


    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        optionsButton.SetActive(false);
        SoundFX_Control.instance.PlayButtonSound();
    }
}
