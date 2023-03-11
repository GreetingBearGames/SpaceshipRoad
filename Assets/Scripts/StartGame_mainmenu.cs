using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame_mainmenu : MonoBehaviour
{
    public GameObject LoadingScreen;
    //[SerializeField] Image LoadingBarFill;
    public Slider loadingSlider;

    public void StartGame(int sceneId)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundFX_Control.instance.PlayButtonSound();
        StartCoroutine(SahneYukleyici(sceneId));
    }

    IEnumerator SahneYukleyici(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progressValue;
            //LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }


        /*
        AsyncOperation operation = SceneManager.LoadSceneAsync(newSceneId);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(2.3f);
        operation.allowSceneActivation = true;
        */
    }
}
