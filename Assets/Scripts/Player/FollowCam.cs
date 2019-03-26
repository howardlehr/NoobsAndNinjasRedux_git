using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;

    float myY;
    float myZ;

    public float dampTime = 1f;
    public Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        myY = gameObject.transform.position.y;
        myZ = gameObject.transform.position.z;
    }

    void LateUpdate ()
    {
        Vector3 destination = new Vector3(player.transform.position.x,myY,myZ);
        //destination.x = player.transform.position.x;
        //LERP --destination.x = Mathf.Lerp(transform.position.x, destination.x, dampTime);
        //velocity = player.GetComponent<Rigidbody2D>().velocity;
        transform.position = new Vector3(destination.x, myY, myZ);
        //transform.position = Vector3.MoveTowards(transform.position, destination, player.GetComponent<PlayerControl>().forwardSpeed * Time.deltaTime);
        //transform.position = new Vector3(player.transform.position.x, myY, myZ);
    }
}
