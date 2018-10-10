using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonCont : MonoBehaviour {

    public GameObject menuShip;
    public GameObject[] ships;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ChangeShipGraphic();	
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
        ChangeShipGraphic();
        GameControl.control.SaveGame();
    }

    public void ChangeShipGraphic()
    {
        if (GameControl.control.currentShip >= ships.Length)
        {
            GameControl.control.currentShip = 0;
        }
        foreach (GameObject s in ships)
        {
            s.SetActive(false);
        }
        ships[GameControl.control.currentShip].SetActive(true);
    }
        
}
