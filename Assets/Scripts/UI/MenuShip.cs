using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShip : MonoBehaviour {

    public GameObject menuShip;
    public GameObject[] ships;

    private void Start()
    {
        ChangeShipGraphic();
    }

    public void ChangeShipForward()
    {
        GameControl.control.currentShip++;
        if (GameControl.control.currentShip >= ships.Length)
        {
            GameControl.control.currentShip = 0;
        }
        ChangeShipGraphic();
        GameControl.control.SaveGame();
    }

    public void ChangeShipGraphic()
    {
        foreach (GameObject s in ships)
        {
            s.SetActive(false);
        }
        ships[GameControl.control.currentShip].SetActive(true);
    }
}
