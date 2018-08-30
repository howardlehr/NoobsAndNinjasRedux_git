using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwap : MonoBehaviour
{

    public string CurrentShip;
    public GameObject oSaucer;
    public GameObject oTruck;
    //public Transform playerTransform;
    //public Transform saucerTransform;
    //public Transform truckTransform;

    // Use this for initialization
    void Start ()
    {
        CurrentShip = "oSaucer";
        oSaucer.SetActive(true);
        oTruck.SetActive(false);
        //Object.Instantiate(pTruck);
        //pTruck.transform.parent = playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeShip();
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {

            
            if (oSaucer)
            {
                Object.DestroyImmediate(oSaucer,true);
                Object.Instantiate(oTruck);
                pTruck.transform.parent = playerTransform;
            }
            
            if (truck)
            {
                Object.DestroyImmediate(truck,true);
                Object.Instantiate(saucer);
            }
            
        }
        */
    }

    public void ChangeShip()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentShip == "oSaucer")
            {
                CurrentShip = "oTruck";
                oSaucer.SetActive(false);
                oTruck.SetActive(true);
                return;
            }
            if (CurrentShip == "oTruck")
            {
                CurrentShip = "oSaucer";
                oSaucer.SetActive(true);
                oTruck.SetActive(false);
                return;
            }
        }
    }
}
