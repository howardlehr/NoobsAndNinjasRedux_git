using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController shopCont;

    public GameObject itemHolderPrefab;
    public Transform grid;
    int len;
    public GameObject[] holders;

    public enum CurrType { NUGGETS, RUBIES, CASH }

    private void Start()
    {
        shopCont = this;
        holders = new GameObject[0];
        fillGear();
    }

    public void fillGear()
    {
        if (holders.Length != 0)
        {
            foreach(GameObject ih in holders)
            {
                Destroy(ih);
            }
        }
        len = GameControl.control.gearArr.Length;
        holders = new GameObject[len];

        for (int i = 0; i < GameControl.control.gearArr.Length; i++)
        {

            GameObject holder = Instantiate(itemHolderPrefab, grid);
            ItemHolderGear holderScript = holder.GetComponent<ItemHolderGear>();

            holderScript.gearName.text = GameControl.control.gearArr[i].name;
            holderScript.id = i;
            holderScript.icon.sprite = GameControl.control.gearArr[i].icon;
            holderScript.desc.text = GameControl.control.gearArr[i].desc;
            holderScript.currType = GameControl.control.gearArr[i].currType;
            holderScript.owned = GameControl.control.gearArr[i].owned;
            holderScript.buyButton.GetComponent<BuyButton>().id = GameControl.control.gearArr[i].id;
            //onButton is gear specific???
            holderScript.onButton.GetComponent<OnButton>().id = GameControl.control.gearArr[i].id;
            if (holderScript.owned)
            {
                holderScript.gearCost.text = "owned";
            }  
            else
            {
                holderScript.gearCost.text = GameControl.control.gearArr[i].cost.ToString("N0");
            }
            holders[i] = holder;
        }
    }

}


/*public int id;
    public Sprite icon;
    public string desc;
    public CurrType currType;
    public int cost;
    public bool owned;
*/