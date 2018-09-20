using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobControl : MonoBehaviour {

    GameObject player;
    Animator anim;
    GameObject rider1;
    GameObject rider2;
    GameObject rider3;
    GameObject rider4;
    Rigidbody2D myRigidBody;
    GameObject[] noobs;

    public float noobRunSpeed;
    float localScaleX;

    void Start ()
    {
        noobRunSpeed = 2f;
        player = GameObject.Find("Player");
        anim = GetComponent<Animator> ();
        localScaleX = transform.localScale.x;
        myRigidBody = GetComponent<Rigidbody2D>();
        FindRiders();
        StartCoroutine(Blink());
    }

    //void Update ()
    //   {
    //       if (Vector2.Distance(transform.position, player.transform.position) < 10)
    //       {
    //           if (player.transform.position.y < -4.42)
    //           {
    //               transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), noobRunSpeed);//move
    //               transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
    //               anim.SetInteger("AnimState", 3);//1 = run
    //           }
    //           else
    //           {
    //               anim.SetInteger("AnimState", 0);//0 = idle
    //           }
    //       }
    //   }

    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            myRigidBody.velocity = new Vector2(0, 0);
            anim.SetInteger("AnimState", 0);//0 = idle
        }
    }

    public void RunToShip()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 10)
        {
            myRigidBody.velocity = new Vector2(noobRunSpeed * (Mathf.Sign(player.transform.position.x - transform.position.x)), 0);
            transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
            anim.SetInteger("AnimState", 3);//1 = run
            Debug.Log("RunningToShip");
        }
    }

    public void FindRiders()
    {
        rider1 = GameObject.Find("rider_1");
        rider2 = GameObject.Find("rider_2");
        rider3 = GameObject.Find("rider_3");
        rider4 = GameObject.Find("rider_4");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //increment riders
            int getRiders = PlayerControl.riders;
            getRiders++;
            PlayerControl.riders = getRiders;
            //player.GetComponent<PlayerControl>().AddSubtractRiders();
            Debug.Log(PlayerControl.riders);
            rider1.GetComponent<RiderCont_1>().TurnOnOff();
            rider2.GetComponent<RiderCont_1>().TurnOnOff();
            rider3.GetComponent<RiderCont_1>().TurnOnOff();
            rider4.GetComponent<RiderCont_1>().TurnOnOff();
        }
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(2, 9));
        var curState = anim.GetInteger("AnimState");
        if (curState != 3)
        {
            anim.SetInteger("AnimState", 1);//1 = blink
        }
        StartCoroutine(UnBlink());
    }

    IEnumerator UnBlink()
    {
        yield return new WaitForSeconds(.3f);
        var curState = anim.GetInteger("AnimState");
        if(curState != 3)
        {
            anim.SetInteger("AnimState", 0);//1 = unBlink
        }
        StartCoroutine(Blink());
    }
}
