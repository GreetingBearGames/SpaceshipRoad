using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float finishLineCreationSec;
    [SerializeField] private Player_Hareket player_Hareket;
    private bool isFinished = false;
    void Start()
    {
        Invoke("FinishLineCreator", finishLineCreationSec);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FinishLineCreator()
    {
        var topPosofScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));
        this.transform.position = Vector2.up * (topPosofScreen.y + 10);    //Additional value for safe factor
        this.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5;      //Approx. speed value

        ObstacleRow.isSpawning = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFinished)
        {
            if (other.tag == "Player")
            {
                Debug.Log("oyun bitti");
                //SoundFX_Control.instance.PlayImpactSound();
                //GameOver.instance.Crashed();
                isFinished = true;
                //tasPatlamaParticle.SetActive(true);
                //StartCoroutine("FadeOut");
                player_Hareket.FinishLinePlayerControl();
            }
        }
    }
}