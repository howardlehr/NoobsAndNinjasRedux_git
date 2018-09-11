using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobControl : MonoBehaviour {

    public Transform player;
    public float noobRunSpeed = .025f;

    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator> ();
	}
	
	void Update ()
    {
		if (Vector2.Distance(transform.position,player.transform.position) < 10)
        {
            if (player.transform.position.y < -4.42)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x,transform.position.y),noobRunSpeed);//move
                transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x), 1f);//facing direction
                anim.SetInteger("AnimState", 1);//1 = run
            }
            else
            {
                anim.SetInteger("AnimState", 0);//0 = idle
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
