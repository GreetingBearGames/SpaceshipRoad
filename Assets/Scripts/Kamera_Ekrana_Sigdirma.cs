using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera_Ekrana_Sigdirma : MonoBehaviour
{
    [SerializeField] SpriteRenderer camerasize;


    void Awake()
    {
        float orthoSize = camerasize.bounds.size.x * Screen.height / Screen.width * 0.5f;
        Camera.main.orthographicSize = orthoSize;
    }
}