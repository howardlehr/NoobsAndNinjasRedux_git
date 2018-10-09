using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaParachute : MonoBehaviour {

    public GameObject player;
    public GameObject myChute;
    public GameObject[] myBloodHoriz;
    public GameObject myGroundNinja;
    public Rigidbody2D myRb;
    Animator myAnim;
    

    public float myDownSpeed;
    bool falling;
    float bounceDirection;
    public float myRotation;
    public float myAngle;

	void Start ()
    {
        myAnim = GetComponent<Animator>();
        myDownSpeed = -5f;
        myRb.velocity = new Vector2(0, myDownSpeed);
        player = GameObject.Find("Player");
        myRotation = 25f;
    }
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (falling)
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
            //Debug.Log(other);
            if (!falling)
            {
                bounceDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
                if (player.GetComponent<PlayerControl>().forwardDirection != 0)
                {
                    AirSplat();
                }
                else
                {
                    //sommersault
                    myChute.SetActive(false);
                    myAnim.SetInteger("AnimState", 2);
                    myRb.velocity = new Vector2(5f * bounceDirection, 5f);
                    myRb.gravityScale = 1;
                    transform.localScale = new Vector2(bounceDirection, transform.localScale.y);
                    //set rotation direction and call rotation routine
                    myRotation *= -bounceDirection;
                    InvokeRepeating("SummerSaultRotation",.05f,.05f);
                }
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
        myRb.velocity = new Vector2(0, myDownSpeed*1.5f);
        myAnim.SetInteger("AnimState", 1);
        falling = true;
    }

    public void GroundSplat()
    {
        myRb.velocity = new Vector2(0, 0);
        myAnim.SetInteger("AnimState", 3);
        transform.position = new Vector2(transform.position.x, -4.8f);
    }

    public void AirSplat()
    {
        foreach (GameObject splat in myBloodHoriz)
        {
            splat.SetActive(true);
            splat.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0f,.02f)*bounceDirection,gameObject.transform.position.y + Random.Range(0f, .02f));

            if (bounceDirection > 0)
            {
                splat.GetComponent<Rigidbody2D>().transform.eulerAngles = Vector3.forward * Random.Range(80f, 110f);
            }
            else
            {
                
                splat.GetComponent<Rigidbody2D>().transform.eulerAngles = Vector3.forward * Random.Range(260f, 280f);
                
            }
            splat.GetComponent<BloodSplatHorizontal>().FlyAway();
            splat.GetComponent<BloodSplatHorizontal>().myRotationSpeed *= -bounceDirection;
            GameObject.Destroy(gameObject);
            //Debug.Log(splat.GetComponent<Rigidbody2D>().transform.eulerAngles);
        }
    }
}
