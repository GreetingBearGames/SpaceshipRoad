using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX_Control : MonoBehaviour
{
    [SerializeField] AudioClip buton, buy, altin, impact;
    public static SoundFX_Control instance;
    public bool isSoundOn;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }


    public void PlayButtonSound()
    {
        if (isSoundOn)
        {
            GetComponent<AudioSource>().clip = buton;
            GetComponent<AudioSource>().Play();
        }
    }


    public void PlayBuySound()
    {
        if (isSoundOn)
        {
            GetComponent<AudioSource>().clip = buy;
            GetComponent<AudioSource>().Play();
        }
    }

    public void PlayCoinSound()
    {
        if (isSoundOn)
        {
            GetComponent<AudioSource>().clip = altin;
            GetComponent<AudioSource>().Play();
        }
    }

    public void PlayImpactSound()
    {
        if (isSoundOn)
        {
            GetComponent<AudioSource>().clip = impact;
            GetComponent<AudioSource>().Play();
        }
    }

    public void YokEt()
    {
        Destroy(this.gameObject);
    }



}
