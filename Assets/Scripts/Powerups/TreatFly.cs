using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatFly : MonoBehaviour {

    //public GameObject player;
    //public Rigidbody2D myRb;

    //Vector2 playerFacing;
    //public float mySpeed;
    public float myZAngle;
    public float myRotationSpeed;
    //public float forwardDirection;

    // Use this for initialization
    void OnEnable()
    {
        //playerFacing = player.transform.localScale;
        //mySpeed = playerFacing.x * Random.Range(.1f, .3f);
        //myRotationSpeed = Random.Range(2f, 4f);
        //forwardDirection = player.transform.localScale.x;
    }

    private void Update()
    {
        myZAngle += myRotationSpeed;
        transform.eulerAngles = Vector3.forward * myZAngle;
    }

    //void LateUpdate ()
    //   {
    //       myRb.MovePosition(new Vector2(gameObject.transform.position.x - mySpeed, gameObject.transform.position.y));
    //}
}
