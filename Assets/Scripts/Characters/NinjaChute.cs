using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaChute : MonoBehaviour
{

    public GameObject myNinja;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myNinja.GetComponent<NinjaParachute>().Fall();
            Destroy(gameObject);
        }
    }
}
