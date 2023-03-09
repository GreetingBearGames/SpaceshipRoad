using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _isGameStarted = false;
    private static GameManager _instance;   //Create instance and make it static to be sure that only one instance exist in scene.

    public static GameManager Instance
    {     //To access GameManager, we use GameManager.Instance
        get => _instance;
    }

    public bool IsGameStarted
    {
        get => _isGameStarted;
        set => _isGameStarted = value;
    }

    private void Awake()
    {
        _instance = this;
    }

}
