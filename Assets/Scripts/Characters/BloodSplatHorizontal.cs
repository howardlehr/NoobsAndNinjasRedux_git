using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatHorizontal : MonoBehaviour
{
    public float myRotationSpeed;
    public float myRotation;
    public Rigidbody2D myRb;
    Animator myAnim;

    void OnEnable()
    {
        myRotationSpeed = 5f;
        myAnim = GetComponent<Animator>();
    }

    public void FlyAway()
    {
        
        myRb.AddForce(gameObject.transform.up * -200f);
        InvokeRepeating("SplatRotate", .1f, .1f);
    }

    // Update is called once per frame
    void SplatRotate()
    {
        transform.eulerAngles += Vector3.forward * myRotationSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            GroundSplat();
        }
    }

    public void GroundSplat()
    {
        myRb.velocity = new Vector2(0, 0);
        transform.eulerAngles = new Vector3(0f,0f,0f);
        CancelInvoke("SplatRotate");
        myRb.gravityScale = 0;
        myAnim.SetInteger("AnimState", 1);
        transform.position = new Vector2(transform.position.x, -5f);
        transform.localScale = new Vector2(.8f, .8f);
    }
}
