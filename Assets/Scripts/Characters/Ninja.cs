using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour {

    GameObject player;
    Animator myAnim;

    Rigidbody2D myRb;
    public GameObject[] noobs;
    public GameObject closestNoob;
    public GameObject[] myBloodHoriz;

    public float RunSpeed;
    float localScaleX;
    public float bounceDirection;

    void OnEnable()
    {
        RunSpeed = 2f;
        player = GameObject.Find("Player");
        myAnim = GetComponent<Animator>();
        localScaleX = transform.localScale.x;
        myRb = GetComponent<Rigidbody2D>();
        RunToShipOrNoob();
    }

    public GameObject FindClosestNoob()
    {
        noobs = GameObject.FindGameObjectsWithTag("Noob");
        GameObject closestNoob = null;
        float distance = Mathf.Infinity;
        Vector3 myPosition = transform.position;
        foreach (GameObject myNoob in noobs)
        {
            Vector3 diff = myNoob.transform.position - myPosition;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closestNoob = myNoob;
                distance = curDistance;
            }
        }
        return closestNoob;
    }

    public void RunToShipOrNoob()
    {
        closestNoob = FindClosestNoob();

        if(closestNoob != null)
        {
            myRb.velocity = new Vector2(RunSpeed * (Mathf.Sign(closestNoob.transform.position.x - transform.position.x)), 0);
            transform.localScale = new Vector2(Mathf.Sign(closestNoob.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
            myAnim.SetInteger("AnimState", 1);
            //Debug.Log("Running to Closest Noob");
        }
        else
        {
            myRb.velocity = new Vector2(RunSpeed * (Mathf.Sign(player.transform.position.x - transform.position.x)), 0);
            transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
            myAnim.SetInteger("AnimState", 1);
            //Debug.Log("RunningToShip");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bounceDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
            if (player.GetComponent<PlayerControl>().forwardDirection != 0)
            {
                AirSplat();
            }
            else
            {
                myAnim.SetInteger("AnimState", 3);
                gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-.2f, gameObject.GetComponent<CapsuleCollider2D>().offset.y);
                myRb.velocity = new Vector2(0f,0f);
            }
        }

        if (other.gameObject.CompareTag("Noob"))
        {
            if (other.GetComponent<NoobControl>().dead != true)
            {
                //swing sword
                myAnim.SetInteger("AnimState", 2);
                gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-.2f, gameObject.GetComponent<CapsuleCollider2D>().offset.y);
                myRb.velocity = new Vector2(0f, 0f);
                //noob head off
                other.gameObject.GetComponent<NoobControl>().HeadOff();
            }
        }
    }

    void AirSplat()
    {
        foreach (GameObject splat in myBloodHoriz)
        {
            splat.SetActive(true);
            splat.transform.position = new Vector2(gameObject.transform.position.x + Random.Range(0f, .02f) * bounceDirection, gameObject.transform.position.y + Random.Range(0f, .02f));

            if (bounceDirection > 0)
            {
                splat.GetComponent<Rigidbody2D>().transform.eulerAngles = Vector3.forward * Random.Range(80f, 110f);
            }
            else
            {

                splat.GetComponent<Rigidbody2D>().transform.eulerAngles = Vector3.forward * Random.Range(260f, 280f);

            }
            splat.GetComponent<BloodSplatHorizontal>().FlyAway();
            splat.GetComponent<BloodSplatHorizontal>().myRotationSpeed *= -bounceDirection;
            GameObject.Destroy(gameObject);
            //Debug.Log(splat.GetComponent<Rigidbody2D>().transform.eulerAngles);
        }
    }

}
