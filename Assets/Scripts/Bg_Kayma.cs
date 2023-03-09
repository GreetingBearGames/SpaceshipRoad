using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_Kayma : MonoBehaviour
{
    private Material textureMaterial;
    private float offset;
    public float scrollSpeed;
    private bool isScrolling = false;


    void Start()
    {
        textureMaterial = GetComponent<Renderer>().material;
        StartScrolling();
    }


    void Update()
    {
        if (isScrolling)
        {
            offset = Mathf.Repeat(Time.time * scrollSpeed, 1);
            textureMaterial.SetTextureOffset("_MainTex", new Vector2(0f, offset));

            //HizArttirma();   //Arka planın da gitgide hızlanmasını istiyorsan fonksiyon oluştur 
        }
    }

    public void StartScrolling()
    {
        isScrolling = true;
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }
}
