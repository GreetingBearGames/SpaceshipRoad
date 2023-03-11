using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Hareket : MonoBehaviour
{
    Vector2 startposition, finalpositionleft, finalpositionright, finalpositionleft2, finalpositionright2;
    private float ekranYukseklik, gemiGenislik;
    [SerializeField] SpriteRenderer camerasize;
    private Rigidbody2D rb;
    private float moveAmount = 0f;
    private float moveSpeedTilt = 1900f;
    public static bool kontrolYontemi;    //kontrolYontemi = 1 ise tilt, 0 ise touch
    private bool isFinished = false;
    private float extraMoveforSmooth = 1f;
    private bool isMovingStarted = false;



    //BUTONLU GEMİ HAREKET YÖNTEMİ
    bool leftMove = false;
    bool rightMove = false;
    private float speed_yeni = 1500f;


    //HYPERCASUAL GEMİ HAREKET YÖNTEMİ
    private Touch touch;
    private Vector2 touchPos, previousTouchPos;
    private float normalizedDeltaPosition, targetPos;
    [SerializeField] private float horizontalSpeedFactor, inputSensitivity;




    /*      ESKİ KONTROL YÖNTEMİ
    void FixedUpdate()
    {
        if (kontrolYontemi)
        {
            MoveWithSensor();
        }

        else
        {
            MoveWithTouch();
        }
    }
    */

    void Update()
    {
        if (GameManager.Instance.IsGameStarted && !isMovingStarted)
        {
            StartMoving();
        }


        if (!isFinished && isMovingStarted)
        {
            if (!kontrolYontemi)
            {
                TouchInput();
                MovewithSlide();
            }
            else
            {
                MoveWithSensor();
            }
        }
    }

    public void MovetoLeftDown() { leftMove = true; }
    public void MovetoLeftUp() { leftMove = false; }
    public void MovetoRightDown() { rightMove = true; }
    public void MovetoRightUp() { rightMove = false; }



    void Start()
    {

    }

    private void StartMoving()
    {
        if (!isMovingStarted)
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

            isMovingStarted = true;
        }

    }
    void MoveWithSensor()
    {
        if (Mathf.Abs(Input.acceleration.x) > 0.01f)
        {
            moveAmount = Input.acceleration.x * moveSpeedTilt * Time.fixedDeltaTime;
            rb.velocity = new Vector2(moveAmount, 0f);

            targetPos = Mathf.Clamp(rb.gameObject.transform.position.x, finalpositionleft.x, finalpositionright.x);
            rb.gameObject.transform.position = new Vector3(targetPos, transform.position.y, transform.position.z);
        }
    }


    void MoveWithTouch()
    {
        if (leftMove == true)
        {
            rb.AddForce(Vector2.left * speed_yeni * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        if (rightMove == true)
        {
            rb.AddForce(Vector2.right * speed_yeni * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        Ship_to_Game.aktiveGemi.transform.position = new Vector2(Mathf.Clamp(Ship_to_Game.aktiveGemi.
                                                                transform.position.x, finalpositionleft.x,
                                                                finalpositionright.x), Ship_to_Game.aktiveGemi.transform.position.y);
    }



    //--------------------------------------------------------------------------------------------------------



    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPos = touch.position;
                touchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                touchPos = touch.position;
            }

            normalizedDeltaPosition = ((touchPos.x - previousTouchPos.x) / Screen.width) * inputSensitivity;
        }
        targetPos = targetPos + normalizedDeltaPosition;
        targetPos = Mathf.Clamp(targetPos, finalpositionleft.x, finalpositionright.x);

        previousTouchPos = touchPos;
    }



    private void MovewithSlide()
    {

        float horizontalSpeed = Time.deltaTime * horizontalSpeedFactor;
        float newPositionTarget = Mathf.Lerp(transform.position.x, targetPos, horizontalSpeed);
        float newPositionDifference = newPositionTarget - transform.position.x;

        newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
        transform.position = new Vector3(transform.position.x + newPositionDifference, transform.position.y, transform.position.z);
    }


    public void FinishLinePlayerControl()
    {
        isFinished = true;

        StartCoroutine("FinishSpeedIncrease");

    }

    IEnumerator FinishSpeedIncrease()
    {
        float acceleration = 0.4f;
        float maxSpeed = 60.0f;
        float currentSpeed = 0.0f;
        GameObject aktifGemi = Ship_to_Game.aktiveGemi;

        //egzos trailrendererlarını aktif hale getirme
        TrailRenderer[] trails = aktifGemi.GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trail in trails)
        {
            trail.enabled = true;
        }


        while (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration;
            aktifGemi.GetComponent<Rigidbody2D>().velocity = Vector2.up * currentSpeed;
            yield return new WaitForEndOfFrame();

            if (aktifGemi.GetComponent<SpriteRenderer>().isVisible == false)
            {
                //Gemi ekrandan çıktı
                break;
            }
        }
    }
}