using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobSafe : MonoBehaviour {

    Rigidbody2D myRigidBody;
    public Transform door;
    public GameObject openDoor;

    public float noobRunSpeed;
    float localScaleX;

    public void RunToDoor()
    {
        noobRunSpeed = 2f;
        myRigidBody = GetComponent<Rigidbody2D>();
        localScaleX = Mathf.Abs(transform.localScale.x);

        myRigidBody.velocity = new Vector2(noobRunSpeed * (Mathf.Sign(door.transform.position.x - transform.position.x)), 0);//run
        transform.localScale = new Vector2(Mathf.Sign(door.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
    }

    //Enter Door
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            openDoor.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
