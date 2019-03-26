using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite icon;
    public string desc;
    public int currType; // 0 = nuggets, 1 = rubies 2 = cash
    public int cost;
    public bool owned;

    //public enum CurrType { NUGGETS, RUBIES, CASH }

    public enum PuType
    {
        BULLET,
        EXPLOSIVE,
        FIRE,
        COLD,
        ACID
    }

    //constructor for Item
    public Item(string na, string de, int co)
    {
        name = na;
        icon = Resources.Load<Sprite>("ItemIcons/" + "ghost_icon");
        desc = de;
        currType = 0;
        cost = co;
    }

    // Item field accessors & mutators
    public int getID() { return id; }
    public string getName() { return name; }
    public void setName(string n) { name = n; }
    public string getDesc() { return desc; }
    public void setDesc(string de) { desc = de; }
    public int getCurrType() { return currType; }
    public void setCurrType(int ct) { currType = ct; }
    public int getCost() { return cost; }
    public void setCost(int co) { cost = co; }
    public bool getOwned() { return owned; }
    public void setOwned(bool ow) { owned = ow; }
}

[System.Serializable]
public class Ship : Item
{
    static int nextID;
    public bool equipped;

    // Ship constructors
    public Ship(string na, string de, int co) : base(na, de, co)
    {
        setID();
        setEquipped(false);
    }
    
    // Ship constructor sets owned and equipped - needed for first ship
    public Ship(string na, string de, int co, bool ow, bool eq) : base(na, de, co)
    {
        setID();
        setOwned(ow);
        setEquipped(eq);
    }

    public void setID() { id = nextID++; }

    // Ship accessors and mutators
    public bool getEquipped() { return equipped; }
    public void setEquipped(bool eq) { equipped = eq; }
}

[System.Serializable]
public class Gear : Item
{
    private static int nextID;
    public bool on;

    public Gear(string na, string de, int co) : base(na, de, co)
    {
        setID();
        setOn(false);
    }

    public void setID() { id = nextID++; }

    public bool getOn() { return on; }
    public void setOn(bool st) { on = st; }
    public void toggleOn()
    {
        on = on == true ? false : true;
    }
}

[System.Serializable]
public class Buff : Item
{
    private static int nextID;

    public Buff(string na, string de, int co, int ct) : base(na, de, co)
    {
        setID();
        setCurrType(ct);
    }

    public void setID() { id = nextID++; }
}

[System.Serializable]
public class Loot : Item
{
    static int nextID;
    int grantCurr;
    public int grant;

    //Loot constructor (only 1 needed)
    public Loot(string na, string de, int co, int ct, int gc, int gr) : base(na, de, co)
    {
        setID();
        setCurrType(ct);
        grantCurr = gc;
        grant = gr;
    }

    public void setID() { id = nextID++; }

    //accessors
    public int getGrantCurr() { return grantCurr; }
    public int getGrant() { return grant; }
}
//From GameMaker Pro--------------------------------
//gear_arr[gear_count, 1]="loot magnet";
//gear_arr[gear_count, 2]=spr_icon_gear;
//gear_arr[gear_count, 5]="noob_nuggets";
//gear_arr[gear_count, 6]=20;
//gear_arr[gear_count, 8]="gear";
//gear_arr[gear_count, 9]=obj_magnet_active;
//gear_arr[gear_count, 10]="pulls all flying#powerups and loot#to the ship";
