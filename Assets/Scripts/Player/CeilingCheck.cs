using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour {

    public GameObject[] sparks;

    bool turnOn;

	//void Awake ()
 //   {
        
 //   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ceiling"))
        {
            InvokeRepeating("MakeSparks", .01f, .01f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ceiling"))
        {
            CancelInvoke("MakeSparks");
            foreach (GameObject n in sparks)
            {
                n.SetActive(false);
            }
        }
    }

    void MakeSparks()
    {
        foreach (GameObject n in sparks)
        {
            turnOn = Random.Range(0, 2) == 0 ? false : true;
            n.SetActive(turnOn);
        }
    }
}
