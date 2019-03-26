using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonCont : MonoBehaviour {

    public void ChangeLevel()
    {
        GameControl.control.level++;
        if (GameControl.control.level > GameControl.control.maxLevel)
        {
            GameControl.control.level = 0;
        }
    }
}
