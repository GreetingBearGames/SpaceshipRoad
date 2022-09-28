using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hareket : MonoBehaviour
{
    Vector2 startposition, finalpositionleft, finalpositionright, finalpositionleft2, finalpositionright2;
    private float ekranYukseklik, gemiGenislik;
    [SerializeField] SpriteRenderer camerasize;
    private Rigidbody2D rb;
	private float moveAmount = 0f;
	private float moveSpeedTilt = 1900f;
    public static bool kontrolYontemi;    //kontrolYontemi = 1 ise tilt, 0 ise touch
    private float extraMoveforSmooth = 1f;



    //BUTONLU GEMİ HAREKET YÖNTEMİ
    bool leftMove = false;
    bool rightMove = false;
    private float speed_yeni = 1500f;


    void FixedUpdate() 
    {
        
        if(kontrolYontemi)      
        {
            MoveWithSensor(); 
        }
        
        else                    
        {
            if(leftMove == true)   
            {
                rb.AddForce(Vector2.left * speed_yeni * Time.fixedDeltaTime, ForceMode2D.Force);
                //rb.velocity = Vector2.left * speed_yeni * Time.fixedDeltaTime;
            }
            if(rightMove == true)   
            {
                rb.AddForce(Vector2.right * speed_yeni * Time.fixedDeltaTime, ForceMode2D.Force);
                //rb.velocity = Vector2.right * speed_yeni * Time.fixedDeltaTime;
            } 
            
        }
    
    Ship_to_Game.aktiveGemi.transform.position = new Vector2 (Mathf.Clamp (Ship_to_Game.aktiveGemi.
        transform.position.x, finalpositionleft.x, finalpositionright.x), Ship_to_Game.aktiveGemi.transform.position.y);

    }

    public void MovetoLeftDown()  {leftMove = true;}
    public void MovetoLeftUp()  {leftMove = false;}
    public void MovetoRightDown()  {rightMove = true;}
    public void MovetoRightUp()  {rightMove = false;}



    void Start()
    {
        ekranYukseklik = camerasize.bounds.size.x * Screen.height / Screen.width;
        startposition = new Vector2(0, (ekranYukseklik * -0.3f));
        Ship_to_Game.aktiveGemi.transform.position = startposition;

        gemiGenislik = Ship_to_Game.aktiveGemi.GetComponent<SpriteRenderer>().bounds.size.x;
        
        finalpositionleft = new Vector2(camerasize.bounds.min.x + (gemiGenislik / 2), startposition.y);
        finalpositionright = new Vector2(camerasize.bounds.max.x - (gemiGenislik / 2), startposition.y);

        finalpositionleft2 = new Vector2(finalpositionleft.x - extraMoveforSmooth, finalpositionleft.y);
        finalpositionright2 = new Vector2(finalpositionright.x + extraMoveforSmooth, finalpositionleft.y);


        rb = Ship_to_Game.aktiveGemi.GetComponent<Rigidbody2D>();
        //kontrolYontemi = false;
    }

    /*
	void Update () 
    {
        if(kontrolYontemi)      
        {
            MoveWithSensor(); 
        }
        else                    
        {
            MoveWithTouch();
        }
    }
    */


    void MoveWithSensor()
    {
        if(Mathf.Abs(Input.acceleration.x) > 0.01f)
        {
            moveAmount = Input.acceleration.x * moveSpeedTilt * Time.fixedDeltaTime;
            rb.velocity = new Vector2 (moveAmount, 0f);    
        }
    }

    /*
    void MoveWithTouch()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2 (Mathf.Clamp (transform.position.x, finalpositionleft.x, finalpositionright.x), transform.position.y);

        /*
        if (Input.touchCount > 1)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                //isContinue = false;
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    Debug.Log("Ekrana temas eden " + i + " numaralı parmak dokunmayı bıraktı.");
                    //isContinue = true;
                }
            }       
        }
        
        
        
        
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                gemiIlkHareketNoktasiX = transform.position.x;                    
            }

            if(touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x < Screen.width / 2) //sol duvara dogru gidecek.
                {
                    transform.position = Vector2.SmoothDamp(transform.position, finalpositionleft2, ref refVelocity, smoothTime);
                    transform.position = new Vector2 (Mathf.Clamp (transform.position.x, finalpositionleft.x, gemiIlkHareketNoktasiX), transform.position.y);
                }

                if (touch.position.x > Screen.width / 2) //sag duvara dogru gidecek.
                {
                    transform.position = Vector2.SmoothDamp(transform.position, finalpositionright2, ref refVelocity, smoothTime);
                    transform.position = new Vector2 (Mathf.Clamp (transform.position.x, gemiIlkHareketNoktasiX, finalpositionright.x), transform.position.y);
                }
            }
        }  
         
    }
    */
}