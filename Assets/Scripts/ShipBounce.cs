using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBounce : MonoBehaviour {

    public GameObject player;

    private Rigidbody2D rb2d;
    
    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //forwardDirection *= -1f;
            Destroy(other.gameObject);
        }
    }
}
