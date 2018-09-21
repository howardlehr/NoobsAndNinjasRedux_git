using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    public bool justBounced = false;
    public int forwardDirection;
    public float forwardSpeed;
    public float myTouchX;
    public float maxUpSpeed;
    public static int riders;
    public GameObject[] riderGroup;
    public GameObject[] noobs;
    public bool dead;

    private float myRotation;
    public float myRotationSpeed = .5f;

    public GameObject dGameOver;

    void Start ()
    {
        forwardSpeed = 5f;
        maxUpSpeed = 10f;
        myRotation = 0;
        FindRiders();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && !justBounced && !dead)
        {
            //vertical motion
            rbPlayer.gravityScale = -4;
            //horizontal motion
            myTouchX = Input.mousePosition.x;
            if (myTouchX < Screen.width * .5)
                forwardDirection = 1;
            else
                forwardDirection = -1;

            //set horizontal velocity
            rbPlayer.velocity = new Vector2(forwardSpeed * forwardDirection, rbPlayer.velocity.y);
            player.transform.localScale = new Vector2(forwardDirection, 1f);//facing direction
            myRotation += forwardDirection * myRotationSpeed;
            myRotation = Mathf.Clamp(myRotation,-7f,7f);
            transform.eulerAngles = Vector3.forward * myRotation;
        } else {
            rbPlayer.gravityScale = 3;
            if(Mathf.Abs(myRotation) > 0)
            {
                myRotation -= forwardDirection * myRotationSpeed;
            }
            transform.eulerAngles = Vector3.forward * myRotation;
        }

        //limit up/down speed
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, (Mathf.Clamp(rbPlayer.velocity.y,-maxUpSpeed, maxUpSpeed)));
    }

    public void FindNoobs()
    {
        noobs = GameObject.FindGameObjectsWithTag("Noob");
    }

    public void FindRiders()
    {
        riderGroup[0] = GameObject.Find("rider_1");
        riderGroup[1] = GameObject.Find("rider_2");
        riderGroup[2] = GameObject.Find("rider_3");
        riderGroup[3] = GameObject.Find("rider_4");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") && !Input.GetButton("Fire1"))
        {
            forwardDirection = 0;
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
            if (riders < 4 && !dead)
            {
                foreach (GameObject n in noobs)
                {
                    if (n != null)
                    {
                        n.GetComponent<NoobControl>().RunToShip();
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Noob"))
        {
            DestroyObject(other.gameObject);
            if (riders < 4)
            {
                riders++;
                foreach (GameObject n in riderGroup)
                {
                    n.GetComponent<RiderCont_1>().TurnOnOff();
                }
                if(riders > 3)
                {
                    foreach (GameObject n in noobs)
                    {
                        if (n != null)
                        {
                            n.GetComponent<NoobControl>().StopRunning();
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ObstacleExplosion"))
        {
            Debug.Log("DEAD by EXPLOSION");
            dead = true;
            player.transform.localScale = new Vector2(forwardDirection, -1f);//turn upside down
            dGameOver.SetActive(true);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Base"))
        {
            if (riders > 0)
            {
                riders--;
                foreach (GameObject n in riderGroup)
                {
                    n.GetComponent<RiderCont_1>().TurnOnOff();
                }
            }
        }
    }
}
