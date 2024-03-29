using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FinishLine : MonoBehaviour
{
    public float finishLineCreationSec;
    [SerializeField] private Player_Hareket player_Hareket;
    private bool isFinished = false;
    [SerializeField] GameObject levelFinishMenu;
    public static bool finishLineCreated = false;


    void Start()
    {
        finishLineCreated = false;
    }

    public void FinishLineSpawnCounter()
    {
        Invoke("FinishLineCreator", finishLineCreationSec);
    }

    private void FinishLineCreator()
    {
        finishLineCreated = true;

        var topPosofScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));
        this.transform.position = Vector2.up * (topPosofScreen.y + 10);    //Additional value for safe factor
        this.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5;      //Approx. speed value
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFinished)
        {
            if (other.tag == "Player")
            {
                SoundFX_Control.instance.PlayWinSound();
                isFinished = true;
                player_Hareket.FinishLinePlayerControl();
                Ship_to_Game.aktiveGemi.GetComponent<PolygonCollider2D>().enabled = false;

                StartCoroutine("WaitLittle");
            }
        }
    }

    IEnumerator WaitLittle()
    {
        yield return new WaitForSeconds(1.5f);
        levelFinishMenu.SetActive(true);
    }
}
