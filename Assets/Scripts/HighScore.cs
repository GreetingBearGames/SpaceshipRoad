using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    private static int highamount;
    private static TextMeshProUGUI highscoreText, scoreValueText;


    void Start()
    {
        highamount = PlayerPrefs.GetInt("HighscoreAmount", 0);
        highscoreText = this.GetComponent<TextMeshProUGUI>();
        scoreValueText = (this.transform.GetChild(0).gameObject).GetComponent<TextMeshProUGUI>();

        DisplayAmount();
    }


    // Get highscore amount.
    public static int GetAmountofHighScore()
    {
        return highamount;
    }


    // Set highscore amount.
    public static void SetAmount(int amountToSet)
    {
        highamount = amountToSet;
        DisplayAmount();
        PlayerPrefs.SetInt("HighscoreAmount", highamount);
    }


    // Display highscore to the screen.
    public static void DisplayAmount()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            highscoreText.text = "SCORE: ";
            scoreValueText.text = highamount.ToString();
        }
        else
        {
            highscoreText.text = "TOP: ";
            scoreValueText.text = highamount.ToString();
        }
    }
}
