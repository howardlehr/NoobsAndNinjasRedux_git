using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaParachute : MonoBehaviour {

    public GameObject player;
    public GameObject myChute;
    public GameObject[] myBloodHoriz;
    public GameObject myGroundNinja;
    public Rigidbody2D myRb;
    public GameObject mySpeechBubble;
    Animator myAnim;

    public float myDownSpeed;
    public bool falling;
    public bool flying;
    float bounceDirection;
    public float bounceHeight;
    public float myRotation;
    public float myAngle;

	void OnEnable ()
    {
        myAnim = GetComponent<Animator>();
        mySpeechBubble = transform.Find("speech_bubble").gameObject;
        myDownSpeed = -5f;
        myRb.velocity = new Vector2(0, myDownSpeed);
        player = GameObject.Find("Player");
        myRotation = 25f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (flying == true || falling == true)
            {
                GroundSplat();
            }
            else
            {
                //swap for ninja basic
                myRb.gravityScale = 0;
                myRb.velocity = new Vector2(0, 0);
                myGroundNinja.SetActive(true);
                myGroundNinja.transform.position = new Vector2(gameObject.transform.position.x, -4.5f);
                GameObject.Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            bounceDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
            if (!falling && !flying && player.GetComponent<PlayerControl>().forwardDirection == 0)
            {
                //sommersault
                myChute.SetActive(false);
                myAnim.SetInteger("AnimState", 2);
                myRb.velocity = new Vector2(5f * bounceDirection, 5f);
                myRb.gravityScale = 1;
                transform.localScale = new Vector2(bounceDirection, transform.localScale.y);
                //set rotation direction and call rotation routine
                myRotation *= -bounceDirection;
                InvokeRepeating("SummerSaultRotation", .05f, .05f);
            }
            else
            {
                bounceHeight = transform.position.y - other.transform.position.y;
                if(bounceHeight > 1.5)
                {
                    player.GetComponent<PlayerControl>().ShipBlood("top");
                }
                else if(bounceHeight < -1.5)
                {
                    player.GetComponent<PlayerControl>().ShipBlood("bottom");
                }
                else
                {
                    player.GetComponent<PlayerControl>().ShipBlood("front");
                }
                
                AirSplat();
            }
        }
    }

    void SummerSaultRotation()
    {
        //Debug.Log(myRotation);
        myAngle += myRotation;
        myRb.transform.eulerAngles = Vector3.forward * myAngle;
    }

    public void Fall()
    {
        myRb.velocity = new Vector2(0, myDownSpeed*2f);
        myAnim.SetInteger("AnimState", 1);
        falling = true;
        Debug.Log("I'm Falling!!!");
        SayAaa();
    }

    public void SayAaa()
    {
        mySpeechBubble.GetComponent<Talk>().Say("Aaaa!");
        mySpeechBubble.GetComponent<Talk>().FixBackwardText(Mathf.Sign(transform.localScale.x));
    }

    public void GroundSplat()
    {
        //myCapColl2D.enabled = false;
        myRb.gravityScale = 0;
        myRb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(transform.position.x, -4.8f);
        //myRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //transform.Rotate(new Vector3(0,0,0));
        //transform.Rotate(Vector3.right * Time.deltaTime * speed);
        myAnim.SetInteger("AnimState", 3);
    }

    public void AirSplat()
    {
        foreach (GameObject splat in myBloodHoriz)
        {
            splat.SetActive(true);
            splat.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0f,.02f)*bounceDirection,gameObject.transform.position.y + Random.Range(0f, .02f));

            if (bounceDirection > 0)
            {
                splat.GetComponent<Rigidbody2D>().transform.eulerAngles = Vector3.forward * Random.Range(85f, 120f);
            }
            else
            {
                
                splat.GetComponent<Rigidbody2D>().transform.eulerAngles = Vector3.forward * Random.Range(240f, 275f);
                
            }
            splat.GetComponent<BloodSplatHorizontal>().FlyAway();
            splat.GetComponent<BloodSplatHorizontal>().myRotationSpeed *= -bounceDirection;
            GameObject.Destroy(gameObject);
            //Debug.Log(splat.GetComponent<Rigidbody2D>().transform.eulerAngles);
        }
    }
}
