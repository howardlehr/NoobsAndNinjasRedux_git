using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolderGear : MonoBehaviour
{
    public int id;
    public Text gearName;
    public Image icon;
    public Text desc;
    public int currType; // 0 = nuggets, 1 = rubies 2 = cash
    public Text gearCost;
    public bool owned;
    public bool on;
    public GameObject buyButton;
    public GameObject onButton;

    //public enum CurrType { NUGGETS, RUBIES, CASH }

    //public enum PuType
    //{
    //    BULLET,
    //    EXPLOSIVE,
    //    FIRE,
    //    COLD,
    //    ACID
    //}
}
