using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwap : MonoBehaviour
{

    public string CurrentShip;
    public GameObject oSaucer;
    public GameObject oTruck;

    void Start ()
    {
        CurrentShip = "oSaucer";
        oSaucer.SetActive(true);
        oTruck.SetActive(false);
    }

    void Update()
    {
        ChangeShip();
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
