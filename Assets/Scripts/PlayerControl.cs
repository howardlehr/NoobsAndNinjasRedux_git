using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    //public SpriteRenderer jets;
    public Transform floorDetectTransform;
    public LayerMask floorDetectLayerMask;
    public bool onFloor;
    public bool justBounced = false;
    public int forwardDirection;
    public float forwardSpeed;
    public float myTouchX;
    public float maxUpSpeed;

    private bool dead;

    void Start ()
    {
        forwardSpeed = 5f;
        maxUpSpeed = 10f;
        //jets = player.jets.GetComponent<SpriteRenderer>();
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

            rbPlayer.velocity = new Vector2(forwardSpeed * forwardDirection, rbPlayer.velocity.y);
            player.transform.localScale = new Vector2(forwardDirection, 1f);
            //jets.enabled = true;
        } else {
            rbPlayer.gravityScale = 3;
            //jets.enabled = false;
        }

        //limit up/down speed
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, (Mathf.Clamp(rbPlayer.velocity.y,-maxUpSpeed, maxUpSpeed)));

        UpdateOnFloorStatus();

    }
    void UpdateOnFloorStatus()
    {
        onFloor = Physics2D.OverlapCircle(floorDetectTransform.position, 0.1f, floorDetectLayerMask);
        if (onFloor && !Input.GetButton("Fire1"))
        {
            forwardDirection = 0;
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }
    }
}
