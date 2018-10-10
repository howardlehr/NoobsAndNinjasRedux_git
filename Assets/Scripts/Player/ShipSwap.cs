using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwap : MonoBehaviour
{
    public GameObject player;
    public GameObject[] ships;

    void Start ()
    {
        ChangeShip();
    }

    public void ChangeShip()
    {
        foreach(GameObject s in ships)
        {
            s.SetActive(false);
        }
        ships[GameControl.control.currentShip].SetActive(true);
        if (player)
        {
            player.GetComponent<PlayerControl>().FindRiders();
        }
    }
}
