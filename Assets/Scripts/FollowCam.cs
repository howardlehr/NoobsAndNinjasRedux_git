using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;

	void LateUpdate ()
    {
        transform.position = new Vector3(player.transform.position.x, 0, -10);
    }
}
