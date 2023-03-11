using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Slider levelProgressBar;
    [SerializeField] private TextMeshProUGUI currentLevelText, levelInfoText;
    [SerializeField] private FinishLine finishLine;
    private float maxDistancetoFinish, gameDuration;
    //[SerializeField] private PlayerController playerController;
    private void Start()
    {
        //maxDistancetoFinish = finishLine.transform.position.z - playerController.transform.position.z;
        currentLevelText.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        levelInfoText.text = "LEVEL " + currentLevelText.text;
    }

    void Update()
    {
        //float distance = finishLine.transform.position.z - playerController.transform.position.z;
        //levelProgressBar.value = 1 - (distance / maxDistancetoFinish);
    }

    public void StartProgressBar()
    {
        gameDuration = finishLine.finishLineCreationSec + 3;        //3 saniye finish line'ın aşağı düşene kadarki geçen süre
        InvokeRepeating("SliderUpdater", 0.2f, 0.2f);
    }

    private void SliderUpdater()
    {
        if (!GameOver.instance.crashedWithStone)
        {
            var incrementAmount = (1 / gameDuration) * 0.2f;
            levelProgressBar.value += incrementAmount;
        }
    }
}
