using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_to_Game : MonoBehaviour
{
    public static GameObject aktiveGemi;


    public void SelectShiptoGame(int shipIndex)
    {
        for (int i = 0; i < 6; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            PlayerPrefs.SetInt("isShipUsed" + i, 0);
        }

        aktiveGemi = gameObject.transform.GetChild(shipIndex).gameObject;

        aktiveGemi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        aktiveGemi.SetActive(true);
        PlayerPrefs.SetInt("isShipUsed" + shipIndex, 1);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1f; f += 0.1f)
        {
            Color objectColor = aktiveGemi.GetComponent<SpriteRenderer>().color;
            objectColor.a = f;
            aktiveGemi.GetComponent<SpriteRenderer>().color = objectColor;
            yield return new WaitForSeconds(0.08f);
        }
    }
}
