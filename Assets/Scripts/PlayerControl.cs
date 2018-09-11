using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    public Transform floorDetectTransform;
    //public LayerMask floorDetectLayerMask;
    public bool justBounced = false;
    public int forwardDirection;
    public float forwardSpeed;
    public float myTouchX;
    public float maxUpSpeed;

    private float myRotation;
    public float myRotationSpeed = .5f;
    private bool dead;

    void Start ()
    {
        forwardSpeed = 5f;
        maxUpSpeed = 10f;
        myRotation = 0;
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && !justBounced)
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") && !Input.GetButton("Fire1"))
        {
            forwardDirection = 0;
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }
    }
}
