using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwap : MonoBehaviour
{

    public GameObject player;
    public string CurrentShip;
    public GameObject oSaucer;
    public GameObject oTruck;

    //GameObject[] noobs;

    void Start ()
    {
        CurrentShip = "oSaucer";
        oSaucer.SetActive(true);
        oTruck.SetActive(false);
        //noobs = GameObject.FindGameObjectsWithTag("Noob");
    }

    void Update()
    {
        ChangeShip();
    }

    public void ChangeShip()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (CurrentShip)
            {
                case "oSaucer":
                CurrentShip = "oTruck";
                oSaucer.SetActive(false);
                oTruck.SetActive(true);
                break;
 
                case "oTruck":
                CurrentShip = "oSaucer";
                oSaucer.SetActive(true);
                oTruck.SetActive(false);
                break;
            }
            player.GetComponent<PlayerControl>().FindRiders();
            //noobs = GameObject.FindGameObjectsWithTag("Noob");
            //foreach (GameObject n in noobs)
            //{
            //    n.GetComponent<NoobControl>().FindRiders();
            //}
            //player.GetComponent<PlayerControl>().FindRiders();

        }
    }
}
