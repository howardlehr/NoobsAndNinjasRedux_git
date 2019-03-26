using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject bombContainer;
    public GameObject myChute;
    public Rigidbody2D myRb;
    public Animator myAnim;

    public float myDownSpeed = -5f;
    public float myRotation;
    public float myAngle;

    void OnEnable()
    {
        myDownSpeed = -5f;
        myRb.velocity = new Vector2(0, myDownSpeed);
        myRotation = 25f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            GroundExplode();
        }
        if (other.gameObject.tag == "Player")
        {
            AirExplode();
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
        myRb.velocity = new Vector2(0, myDownSpeed * 1.5f);
        //myAnim.SetInteger("AnimState", 1);
    }

    public void GroundExplode()
    {
        myRb.gravityScale = 0;
        myRb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(transform.position.x, -4.8f);
        myAnim.SetTrigger("expl_ground");
        myChute.SetActive(false);
    }

    public void AirExplode()
    {
        //myRb.velocity = new Vector2(0, 0);
        myAnim.SetTrigger("expl_air");
        myChute.SetActive(false);
    }

    public void TurnMeOff()
    {
        gameObject.transform.position = bombContainer.transform.position;
        myChute.SetActive(true);
        gameObject.SetActive(false);
    }
}
