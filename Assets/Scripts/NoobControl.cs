using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobControl : MonoBehaviour {

    public Transform player;
    public float noobSpeed = .025f;
    
	void Start () {
		
	}
	
	void Update ()
    {
		if (Vector2.Distance(transform.position,player.transform.position) < 50)
        {
            if (player.transform.position.y < -4.42)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x,transform.position.y),noobSpeed);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
