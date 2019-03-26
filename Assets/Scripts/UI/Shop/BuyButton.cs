using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {

    public int id;

    private void Start()
    {
        if (GameControl.control.gearArr[id].owned)
        {
            gameObject.SetActive(false);
        }
    }

    public void buyGear ()// on click event
    {
        if (GameControl.control.gearArr[id].cost <= GameControl.control.noobNuggets)
        {
            GameControl.control.noobNuggets -= GameControl.control.gearArr[id].cost;
            GameControl.control.gearArr[id].owned = true;
            GameControl.control.gearArr[id].on = true;
            //var onbuts = GameObject.FindGameObjectsWithTag("OnButton");
            //foreach (var on in onbuts)
            //{
            //    //OnButton.on.Activate(id);
            //    //if (OnButton.on.id == id)
            //    //{
            //    //    on.ActivateOnButton();
            //    //}
            //}
            //gameObject.SetActive(false);
            ShopController.shopCont.fillGear();
        }
        else
        {
            Debug.Log("Not Enough Noob Nuggets!!!");
        }
    }
}
