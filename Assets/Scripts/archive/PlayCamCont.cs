using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamCont : MonoBehaviour {

    public GameObject player;
    public GameObject power_source;
    public Rigidbody2D rbPlayer;
    public Transform floorDetectTransform;
    public Transform myTransform;
    public float forwardDirection = 1f;
    public float forwardSpeed = 0.1f;
    public float jetForce = 50f;
    public float maxUpSpeed = 10f;
    public float myTouchX;
    public float cameraOffset = 3;
    public ParallaxScroll parallax;
    public LayerMask floorDetectLayerMask;

    private bool dead;
    public bool onFloor;

    void Start()
    {
        
    }

   private void FixedUpdate()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            rbPlayer.gravityScale = -4;
            myTouchX = Input.mousePosition.x;
            if (myTouchX < Screen.width * .5)
                forwardDirection = 1f;
            else
                forwardDirection = -1f;

            rbPlayer.velocity = new Vector2(forwardSpeed * forwardDirection, rbPlayer.velocity.y);
            player.transform.localScale = new Vector2(forwardDirection,1f);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            rbPlayer.gravityScale = 2;
        }

        UpdateOnFloorStatus();

    }

    void Update()
    {
        ////move the camera
        //Vector3 camX = transform.position;
        //camX.x = transform.position.x + (forwardSpeed * forwardDirection);
        //transform.position = camX;
        ////move the ship horizontally
        //if (((transform.position.x - player.transform.position.x) < -cameraOffset) && (forwardDirection == -1))
        //{
        //    ScootShip();
        //}
        //if (((transform.position.x - player.transform.position.x) > cameraOffset) && (forwardDirection == 1))
        //{
        //    ScootShip();
        //}

        //myTransform.transform.position.x = rbPlayer.transform.position.x;

        //scoot camera
        Vector2 playerPosition = player.transform.position;
        playerPosition.x = transform.position.x;
        //Vector2 myPosition = myTransform.position;
        // myPosition.x = playerPosition.x;
        //transform.position.x = playerPosition.x;

        parallax.offset = transform.position.x;//parallax motion for backgrounds

    }

    void UpdateOnFloorStatus()
    {
        onFloor = Physics2D.OverlapCircle(floorDetectTransform.position, 0.1f, floorDetectLayerMask);
        if (onFloor)
        {
            forwardDirection = 0;
        }
    }

    //don't know why this doesn't work
    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject == power_source)
        {
            Destroy(other.gameObject);
        }
    }
}
