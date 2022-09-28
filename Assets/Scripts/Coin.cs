using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool taken;


    void Start()
    {
        taken = false;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        // Checks if coin has been taken. (Because player can re-enter coin while coin destroy animation is playing)
        if (!taken)
        {
            // Checks if coin collides with one of the player colliders.
            if (col.tag == "Player")
            {
                // Increases player wallet amount.
                Wallet.SetAmount(Wallet.GetAmount() + 1);
                SoundFX_Control.instance.PlayCoinSound();
                taken = true;
                StartCoroutine("DestroyCoin");
            }
        }
    }

    IEnumerator DestroyCoin()
    {
        Color objectColor = this.GetComponent<SpriteRenderer>().color;
        objectColor.a = 0f;
        this.GetComponent<SpriteRenderer>().color = objectColor;

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }  
}