using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBounce : MonoBehaviour {

    private GameObject thePlayer;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thePlayer = other.gameObject;
            thePlayer.GetComponent<PlayerControl>().justBounced = true;
            StartCoroutine(WaitForBounce());
        }
    }

    IEnumerator WaitForBounce()
    {
        yield return new WaitForSeconds (.5f);
        thePlayer.GetComponent<PlayerControl>().justBounced = false;
    }
}
