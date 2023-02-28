using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLineSpawner : MonoBehaviour
{
    [Range(0, 4)] public int minObstacles, maxObstacles;
    [Range(2f, 30f)] public float newspawn_deltaheight;
    [Range(0, 15f)] public float randomizeObstaclesOffest;
    [Range(0, 1.0f)] public float coinSpawnRate;
    public static ObstacleLineSpawner instance;
    private int lineIndex;
    public float ObstacleSpeedStart;


    void Start()
    {
        instance = this;
        SpawnLine();
    }


    public void SpawnLine()
    {
        // Create new line gameobject.
        GameObject line = new GameObject("Line-" + lineIndex);
        // Make line gameobject child of current gameobject.
        line.transform.parent = transform;
        // Add ObstaclesLine script to the gameobject.
        line.AddComponent<ObstacleRow>();
        line.GetComponent<ObstacleRow>().ilkHiz = ObstacleSpeedStart;

        if (lineIndex > 0)
        {
            //Increase player score.
            IncreaseScore();
        }
        // Increase line index.
        lineIndex++;
    }


    private void IncreaseScore()
    {
        Score.SetAmount(Score.GetAmount() + 1);
    }
}
