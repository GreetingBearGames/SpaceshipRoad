using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_to_Game : MonoBehaviour
{
    public static GameObject aktiveGezegen;
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] AudioClip[] muzikler = new AudioClip[4];

    void Awake() 
    {        
        for(int i = 0; i < 4; i++)
        {
            GameObject deaktiveGezegen = gameObject.transform.GetChild(i).gameObject;
            deaktiveGezegen.SetActive(false);

            if(PlayerPrefs.GetInt("isPlanetUsed" + i) == 1)
            {
                GameObject aktiveGezegen = gameObject.transform.GetChild(i).gameObject;
                aktiveGezegen.SetActive(true);
                //GezegenObstacleSecici();
                
                if(PlayerPrefs.GetInt("MusicOption") == 1)
                {
                    this.GetComponent<AudioSource>().clip = muzikler[i];
                    this.GetComponent<AudioSource>().Play();
                }
                break;
            }
        }  
    }

    /*
    private void GezegenObstacleSecici()
    {
        
        obstacles = new List<GameObject>();
        Object[] objects = Resources.LoadAll("Obstacles") as Object[];
        foreach (Object item in objects)
        {
            obstacles.Add(item as GameObject);
        }
        

        if(PlayerPrefs.GetInt("isPlanetUsed" + 1) == 1)      //mars ise
        {
            MarsObstacles();
        }
        else
        {
            OtherPlanetObstacles();
        }
    }


    private void MarsObstacles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].GetComponent<SpriteRenderer>().color = new Color(0.945f, 0.945f, 0.415f);
        }
    }

    private void OtherPlanetObstacles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
    }
    */


}
