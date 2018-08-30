//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraControl : MonoBehaviour
//{

//    public float forwardDirection = 1f;
//    public int forwardSpeed = 10;
//    //public float jetForce = 50;
//    public float myTouchX;
//    public ParallaxScroll parallax;

//    private Rigidbody2D rb;
//    private bool dead = false;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    // Update is called once per frame
//    private void FixedUpdate()
//    {
//        bool jetActive = Input.GetButton("Fire1");

//        if (jetActive)
//        {
//            //rb.AddForce(new Vector2(0, jetForce));
//            myTouchX = Input.mousePosition.x;
//            if (myTouchX < Screen.width * .5)
//                forwardDirection = 1f;
//            else
//                forwardDirection = -1f;

//            //transform.localScale = new Vector3(forwardDirection, 1, 1);//turn the ship
//            //move the ship
//            if (!dead)
//            {
//                Vector2 newVelocity = rb.velocity;
//                newVelocity.x = forwardSpeed * forwardDirection;
//                rb.velocity = newVelocity;
//            }
//        }

//        parallax.offset = transform.position.x;//parallax motion for backgrounds
//    }
//}
