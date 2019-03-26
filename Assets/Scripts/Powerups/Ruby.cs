using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby : MonoBehaviour {

    public GameObject myPickup;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameControl.control.rubies ++;
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("LevelLimiter"))
        {
            myPickup.SetActive(false);
        }
    }
}
