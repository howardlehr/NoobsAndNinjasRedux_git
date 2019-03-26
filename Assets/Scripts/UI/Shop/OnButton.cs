using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButton : MonoBehaviour {

    public int id;
    public Text myText;

    private void Start()
    {
        if (!GameControl.control.gearArr[id].owned)
        {
            gameObject.SetActive(false);
        }
    }

    public void ToggleOn()
    {
        if (GameControl.control.gearArr[id].on)
        {
            GameControl.control.gearArr[id].on = false;
            myText.text = "off";
        }
        else
        {
            GameControl.control.gearArr[id].on = true;
            myText.text = "on";
        }
    }

    public void Activate(int i)
    {
        if (i == id)
        {
            gameObject.SetActive(true);
        }
    }
}
