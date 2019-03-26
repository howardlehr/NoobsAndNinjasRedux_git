using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{

    Text myTextBox;
    IEnumerator stopTalking;

    // Use this for initialization
    void Awake ()
    {
        myTextBox = transform.Find("bubble_text").GetComponent<UnityEngine.UI.Text>();
        gameObject.SetActive(false);
        stopTalking = StopTalking();
    }
	
	// Update is called once per frame
	void OnDisable ()
    {
        if(stopTalking != null)
        {
            StopCoroutine(stopTalking);
        }
    }

    public void Say(string myText)
    {
        gameObject.SetActive(true);
        myTextBox.text = myText;
        if(stopTalking != null)
        {
            StopCoroutine(stopTalking);
            stopTalking = StopTalking();
            StartCoroutine(stopTalking);
        }
    }

    public void FixBackwardText(float parentDirection)
    {
        myTextBox.transform.localScale = new Vector2(Mathf.Abs(myTextBox.transform.localScale.x) * parentDirection, myTextBox.transform.localScale.y);
    }

    IEnumerator StopTalking()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
        myTextBox.text = "?!?!";
    }
}
