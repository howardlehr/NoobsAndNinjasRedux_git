using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetsOnOff : MonoBehaviour {

    private SpriteRenderer jets;

    void Start ()
    {
        jets = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate ()
    {
		if (Input.GetButton("Fire1"))//(jetActive)
        {
            jets.enabled = true;
        }
        else
        {
            jets.enabled = false;
        }
	}
}
