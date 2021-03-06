﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetsOnOff : MonoBehaviour
{

    public GameObject player;
    private SpriteRenderer jets;
    private bool didBounce;
    private bool dead;

    void Start()
    {
        jets = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {

        didBounce = player.GetComponent<PlayerControl>().justBounced;
        dead = player.GetComponent<PlayerControl>().dead;

        if (Input.GetButton("Fire1") && !didBounce && !dead)//(jetActive)
        {
            jets.enabled = true;
        }
        else
        {
            jets.enabled = false;
        }
    }
}
