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
    public GameObject[] riderGroup;
    public GameObject[] noobsSafe;
    public GameObject[] noobs;
    public GameObject[] ninjas;
    public bool dead;
    public bool onBase;
    bool noobIsDead;

    private float myRotation;
    public float myRotationSpeed = .5f;

    public GameObject dGameOver;

    void Start ()
    {
        forwardSpeed = 5f;
        maxUpSpeed = 10f;
        myRotation = 0;
        GameControl.control.riders = 0;
        FindRiders();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(forwardDirection == 0)
            {
                //remove onBase to stop noobs ejecting
                onBase = false;
                CancelInvoke("EjectNoobs");
                NotifyNinjas();
            }
        }

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

    //floor collision handler
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") && !Input.GetButton("Fire1"))
        {
            forwardDirection = 0;
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
            //tell noobs to run to ship
            if (GameControl.control.riders < 4 && !dead)
            {
                foreach (GameObject n in noobs)
                {
                    if (n != null)
                    {
                        n.GetComponent<NoobControl>().RunToShip();
                    }
                }
            }
            //tell Ninjas to change direction
            NotifyNinjas();
        }
    }

    //noobs, obstacles, base
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Noob"))
        {
            if (GameControl.control.riders < 4)
            {
                DestroyObject(other.gameObject);
                riderGroup[GameControl.control.riders].GetComponent<SpriteRenderer>().enabled = true;
                GameControl.control.riders++;
                GameControl.control.noobNuggets += GameControl.control.scoreInc;
                if (GameControl.control.riders > 3)
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

        if (other.gameObject.CompareTag("ObstacleExplosion"))
        {
            //Debug.Log("DEAD by EXPLOSION");
            dead = true;
            player.transform.localScale = new Vector2(forwardDirection, -1f);//turn upside down
            Destroy(other.gameObject);
            EndLevel();
        }

        if (other.gameObject.CompareTag("Base"))
        {
            if (GameControl.control.riders > 0)
            {
                InvokeRepeating("EjectNoobs", .1f, .5f);
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        onBase = true;
    }

    void EjectNoobs()
    {
        if(onBase && GameControl.control.riders > 0)
        {
            GameControl.control.riders--;
            riderGroup[GameControl.control.riders].GetComponent<SpriteRenderer>().enabled = false;
            noobsSafe[GameControl.control.riders].SetActive(true);
            noobsSafe[GameControl.control.riders].transform.position = new Vector2(gameObject.transform.position.x, noobsSafe[GameControl.control.riders].transform.position.y);
            noobsSafe[GameControl.control.riders].GetComponent<NoobSafe>().RunToDoor();
            if(GameControl.control.riders < 1)
            {
                EndLevelTest();
            }
        }
    }

    public void EndLevelTest()
    {
        if (GameControl.control.riders < 1)
        {
            int livingNoobs = 0;
            foreach (GameObject n in noobs)
            {
                if(n != null)
                {
                    noobIsDead = n.GetComponent<NoobControl>().dead;
                    if(!noobIsDead)
                        livingNoobs++;
                }
            }
            if(livingNoobs < 1)
            {
                EndLevel();
            }
        }
    }

    public void EndLevel()
    {
        dGameOver.SetActive(true);
        GameControl.control.riders = 0;
        dead = true;
    }

    public void NotifyNinjas()
    {
        ninjas = GameObject.FindGameObjectsWithTag("Ninja");
        if(ninjas.Length > 0)
        {
            foreach (GameObject ninja in ninjas)
            {
                if (ninja != null)
                {
                    ninja.GetComponent<Ninja>().RunToShipOrNoob();
                }
            }
        }
        
    }
}
