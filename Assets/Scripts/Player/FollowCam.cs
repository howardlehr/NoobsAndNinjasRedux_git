using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;

    float myY;
    float myZ;

    private void Awake()
    {
        myY = gameObject.transform.position.y;
        myZ = gameObject.transform.position.z;
    }

    void LateUpdate ()
    {
        transform.position = new Vector3(player.transform.position.x, myY, myZ);
    }
}
