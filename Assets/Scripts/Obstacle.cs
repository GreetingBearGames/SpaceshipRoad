using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float rotationspeed;
    private int rotationDirection;
    private bool crashed;
    private GameObject tasPatlamaParticle;


    void Start()
    {
        rotationspeed = Random.Range(1f, 100f);
        rotationDirection = Random.Range(0, 2) * 2 - 1;
        tasPatlamaParticle = gameObject.transform.GetChild(0).gameObject;
        crashed = false;
    }


    void Update()
    {
        transform.Rotate(0, 0, rotationspeed * rotationDirection * Time.deltaTime, Space.Self);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        // Check if did not crashed already.
        if (!crashed)
        {
            if (col.tag == "Player")
            {
                //SoundFX_Control.instance.PlayImpactSound();
                GameOver.instance.Crashed();
                // Set crashed value to true so that this function would not be called again.
                crashed = true;
                tasPatlamaParticle.SetActive(true);
                StartCoroutine("FadeOut");
            }
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.2f)
        {
            Color objectColor = GetComponent<SpriteRenderer>().color;
            objectColor.a = f;
            GetComponent<SpriteRenderer>().color = objectColor;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
