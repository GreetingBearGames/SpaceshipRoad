using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverWindow, patlamaParticle, pauseButtton, optionsButton;
    public bool crashedWithStone;
    public static GameOver instance;
    private GameObject aktifOlanGemi;
    [SerializeField] GameObject finishLine;


    void Start()
    {
        instance = this;
        crashedWithStone = false;
    }


    public void Crashed()
    {
        if (!crashedWithStone)
        {
            crashedWithStone = true;
            ObstacleRow.speed = 0;
            aktifOlanGemi = Ship_to_Game.aktiveGemi;

            patlamaParticle.GetComponent<Transform>().position = aktifOlanGemi.GetComponent<Transform>().position;
            patlamaParticle.SetActive(true);
            GameOverWindow.SetActive(true);
            pauseButtton.SetActive(false);
            optionsButton.SetActive(true);
            finishLine.SetActive(false);

            (aktifOlanGemi.transform.GetChild(0).gameObject).SetActive(false); //aktif geminin flamelerini gizleme
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.2f)
        {
            Color objectColor = aktifOlanGemi.GetComponent<SpriteRenderer>().color;
            objectColor.a = f;
            aktifOlanGemi.GetComponent<SpriteRenderer>().color = objectColor;
            yield return new WaitForSeconds(0.15f);
        }
    }
}

