using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour {

    public GameObject myObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(myObject)
            {
                myObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
