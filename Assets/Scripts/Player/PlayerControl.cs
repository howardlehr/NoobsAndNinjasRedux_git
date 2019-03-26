using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    public GameObject[] ships;
    public GameObject[] riderGroup;
    public GameObject blood_front;
    public GameObject blood_bottom;
    public GameObject blood_top;
    public GameObject blood_type;
    public GameObject[] noobsSafe;
    public GameObject[] noobs;
    public GameObject[] ninjas;
    public GameObject dustLanding;
    public GameObject dustTakeoff;
    public GameObject bombContainer;

    public bool justBounced = false;
    public int forwardDirection;
    public float forwardSpeed;
    public float myTouchX;
    public float maxUpSpeed;
    public bool dead;
    //public bool onBase;
    bool noobIsDead;

    private float myRotation;
    public float myRotationSpeed = .5f;

    public GameObject dGameOver;

    void Start ()
    {
        forwardSpeed = 7f;
        maxUpSpeed = 14f;
        myRotation = 0;
        GameControl.control.riders = 0;
        ChangeShip();
        FindRiders();
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (forwardDirection == 0)
                {
                    //remove onBase to stop noobs ejecting
                    //onBase = false;
                    CancelInvoke("EjectNoobs");
                    //tell ninjas to run to ship if no Noobs left
                    NotifyNinjas();
                    CancelInvoke("HurryBomb");
                }
            }

            if (Input.GetButton("Fire1") && !justBounced && !dead)
            {
                //vertical motion
                rbPlayer.gravityScale = -6;
                //horizontal motion
                myTouchX = Input.mousePosition.x;
                if (myTouchX < Screen.width * .5)
                    forwardDirection = 1;
                else
                    forwardDirection = -1;

                //make dust cloud
                if (transform.position.y < -3.5)
                {
                    dustTakeoff.SetActive(true);
                    dustTakeoff.transform.position = new Vector2(transform.position.x - forwardDirection * 1.5f, -5.2f);
                    dustTakeoff.transform.localScale = new Vector2(forwardDirection, 1f);
                }

                //set horizontal velocity
                rbPlayer.velocity = new Vector2(forwardSpeed * forwardDirection, rbPlayer.velocity.y);
                
                player.transform.localScale = new Vector2(forwardDirection, 1f);//facing direction
                myRotation += forwardDirection * myRotationSpeed;
                myRotation = Mathf.Clamp(myRotation, -7f, 7f);
                transform.eulerAngles = Vector3.forward * myRotation;

            }
            else
            {
                rbPlayer.gravityScale = 3;
                if (Mathf.Abs(myRotation) > 0)
                {
                    myRotation -= forwardDirection * myRotationSpeed;
                }
                transform.eulerAngles = Vector3.forward * myRotation;
            }

            //limit up/down speed
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, (Mathf.Clamp(rbPlayer.velocity.y, -maxUpSpeed, maxUpSpeed)));
        }
    }

    public void ChangeShip()
    {
        foreach (GameObject s in ships)
        {
            s.SetActive(false);
        }
        ships[GameControl.control.currentShip].SetActive(true);
        FindRiders();
    }

    public void FindNoobs()
    {
        noobs = GameObject.FindGameObjectsWithTag("Noob");
    }

    //includes riders, powerups, blood, ceiling check
    public void FindRiders()
    {
        riderGroup[0] = GameObject.Find("rider_1");
        riderGroup[1] = GameObject.Find("rider_2");
        riderGroup[2] = GameObject.Find("rider_3");
        riderGroup[3] = GameObject.Find("rider_4");

        blood_front = GameObject.Find("blood_front");
        blood_bottom = GameObject.Find("blood_bottom");
        blood_top = GameObject.Find("blood_top");
    }

    //floor collision handler
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!dead)
        {
            if (other.gameObject.CompareTag("Floor") && !Input.GetButton("Fire1"))
            {
                forwardDirection = 0;
                rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
                //sit flat (make rotation 0)
                transform.eulerAngles = Vector3.forward * 0;
                //transform.rotation = new Vector3(0f, 0f, 0f);
                //make dust cloud
                dustLanding.SetActive(true);
                dustLanding.transform.position = new Vector2(transform.position.x, -5.2f);
                //tell noobs to run to ship
                if (!dead)
                {
                    //tell Ninjas to change direction
                    NotifyNinjas();
                    //start hurry bomb
                    InvokeRepeating("HurryBomb", 3f, 2f);
                    //tell noobs to run to ship
                    if (GameControl.control.riders < 4)
                    {
                        foreach (GameObject n in noobs)
                        {
                            if (n != null)
                            {
                                n.GetComponent<NoobControl>().RunToShip();
                            }
                        }
                    }
                    else
                    {
                        foreach (GameObject n in noobs)
                        {
                            if (n != null)
                            {
                                n.GetComponent<NoobControl>().ShipFull();
                            }
                        }
                    }
                }
            }
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
                            n.GetComponent<NoobControl>().ShipFull();
                        }
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("ObstacleExplosion"))
        {
            KillPlayer("Explosion");
            Destroy(other.gameObject);
            
        }

        if (other.gameObject.CompareTag("Base"))
        {
            if (GameControl.control.riders > 0)
            {
                InvokeRepeating("EjectNoobs", .1f, .5f);
            }
        }

    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    onBase = true;
    //}

    void EjectNoobs()
    {
        if(GameControl.control.riders > 0)
        {
            GameControl.control.riders--;
            riderGroup[GameControl.control.riders].GetComponent<SpriteRenderer>().enabled = false;
            noobsSafe[GameControl.control.riders].SetActive(true);
            noobsSafe[GameControl.control.riders].transform.position = new Vector2(gameObject.transform.position.x, noobsSafe[GameControl.control.riders].transform.position.y);
            noobsSafe[GameControl.control.riders].GetComponent<NoobSafe>().RunToDoor();
            noobsSafe[GameControl.control.riders].GetComponent<NoobSafe>().Talk("Thank You!");
            if (GameControl.control.riders < 1)
            {
                EndLevelTest();
            }
        }
    }

    public void ShipBlood(string spot)
    {
        switch (spot)
        {
            case "front": { blood_type = blood_front; break; }
            case "bottom": { blood_type = blood_bottom; break; }
            case "top": { blood_type = blood_top; break; }
        }
        blood_type.GetComponent<SpriteRenderer>().enabled = true;
        blood_type.GetComponent<FadeScript>().StartFade("out", 4f, .1f);
    }

    public void KillPlayer(string deathType)
    {
        if (!GameControl.control.godMode)
        {
            dead = true;
            if (deathType == "Explosion")
            {
                ships[GameControl.control.currentShip].SetActive(false);
                ships[ships.Length -1].SetActive(true);
                if(rbPlayer.velocity.y > 0)
                {
                    rbPlayer.gravityScale = .5f;
                }
                else
                {
                    rbPlayer.gravityScale = 0f;
                }
                rbPlayer.drag = 1.5f;
            }
            GameControl.control.riders = 0;
            
            CancelInvoke("HurryBomb");
            CancelInvoke("EjectNoobs");
            StartCoroutine(EndLevel(2f));
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
                StartCoroutine(EndLevel(.5f));
                dead = true;
            }
        }
    }

    IEnumerator EndLevel(float mySeconds)
    {
        yield return new WaitForSeconds(mySeconds);
        dGameOver.SetActive(true);
        
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

    public void HurryBomb()
    {
        if (!GameControl.control.hurryBombOff)
        {
            bombContainer.transform.position = new Vector2(player.transform.position.x, bombContainer.transform.position.y);
            bombContainer.GetComponent<BombContainer>().DropBomb();
        }
        
    }
}
