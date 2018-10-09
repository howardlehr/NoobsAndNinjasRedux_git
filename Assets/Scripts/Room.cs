using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public GameObject pad;
    public GameObject player;

    public float myWidth;
    public float padSide;

    void Start()
    {
        if(pad)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            padSide = Mathf.Sign(player.transform.position.x - transform.position.x);
            pad.transform.position = new Vector2(gameObject.transform.position.x + (padSide * myWidth), 0);
        }
    }
}
