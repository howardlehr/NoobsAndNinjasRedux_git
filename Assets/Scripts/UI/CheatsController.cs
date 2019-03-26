using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsController : MonoBehaviour {

    public GameObject togGodMode;
    public GameObject togHurryBombOff;

    private void Start()
    {
        togGodMode.GetComponent<Toggle>().isOn = GameControl.control.godMode;
        togHurryBombOff.GetComponent<Toggle>().isOn = GameControl.control.hurryBombOff;
    }

    public void GodModeOnOff()
    {
        if (GameControl.control.godMode)
        {
            GameControl.control.godMode = false;
        }
        else
        {
            GameControl.control.godMode = true;
        }
        //Debug.Log("OnOff -- godMode = " + GameControl.control.godMode);
    }

    public void HurryBombOnOff()
    {
        if (GameControl.control.hurryBombOff)
        {
            GameControl.control.hurryBombOff = false;
        }
        else
        {
            GameControl.control.hurryBombOff = true;
        }
        //Debug.Log("OnOff -- HurryBombOff = " + GameControl.control.hurryBombOff);
    }
}
