using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour
{
    public GameObject myPickup;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameControl.control.noobNuggets += GameControl.control.level * 5;
            myPickup.SetActive(false);
        }

        if (other.gameObject.CompareTag("LevelLimiter"))
        {
            myPickup.SetActive(false);
        }
    }
}
