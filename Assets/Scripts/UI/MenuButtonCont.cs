using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonCont : MonoBehaviour {

    public GameObject menuShip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeLevel()
    {
        GameControl.control.level++;
        if (GameControl.control.level > GameControl.control.maxLevel)
        {
            GameControl.control.level = 0;
        }
    }

    public void ChangeShipForward()
    {
        GameControl.control.currentShip++;
        if (GameControl.control.currentShip >= menuShip.GetComponent<ShipSwap>().ships.Length)
        {
            GameControl.control.currentShip = 0;
        }
        menuShip.GetComponent<ShipSwap>().ChangeShip();
    }
}
