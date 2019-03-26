using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoobSafe : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    public Transform door;
    public GameObject openDoor;
    public GameObject mySpeechBubble;
    public Text myTextBox;

    public float noobRunSpeed;
    float localScaleX;

    private void OnDisable()
    {
        StopCoroutine(StopTalking());
    }

    public void RunToDoor()
    {
        noobRunSpeed = 2f;
        myRigidBody = GetComponent<Rigidbody2D>();
        localScaleX = Mathf.Abs(transform.localScale.x);

        myRigidBody.velocity = new Vector2(noobRunSpeed * (Mathf.Sign(door.transform.position.x - transform.position.x)), 0);//run
        transform.localScale = new Vector2(Mathf.Sign(door.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
    }

    public void Talk(string myText)
    {
        mySpeechBubble.SetActive(true);
        FixBackwardText();
        myTextBox.text = myText;
        StartCoroutine(StopTalking());
    }

    public void FixBackwardText()
    {
        if (transform.localScale.x < 1)
        {
            myTextBox.transform.localScale = new Vector2(Mathf.Abs(myTextBox.transform.localScale.x) * -1, myTextBox.transform.localScale.y);
        }
        else
        {
            myTextBox.transform.localScale = new Vector2(Mathf.Abs(myTextBox.transform.localScale.x), myTextBox.transform.localScale.y);
        }
    }

    IEnumerator StopTalking()
    {
        yield return new WaitForSeconds(2);
        mySpeechBubble.SetActive(false);
        myTextBox.text = "???";
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
