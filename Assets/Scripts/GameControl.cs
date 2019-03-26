using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    //gameplay variables
    public int riders;
    
    //Score Variables
    public int level;
    public int maxLevel = 2;
    public int noobNuggets;
    public int rubies;
    public int scoreInc;
    public int currentShip;

    //Items Databases
    public Ship[] shipArr = new Ship[10];
    public Gear[] gearArr = new Gear[12];
    public Buff[] buffArr = new Buff[6];
    public Loot[] lootArr = new Loot[8];

    //Cheats
    public bool godMode;
    public bool hurryBombOff;

    //singleton
    void Awake ()
    {
        //Singleton
		if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }

        Debug.Log("Starting Database Creation!!!");
        ItemDatabaseSetup();

        LoadSaveGame();

        SetScoreIncrement();
    }

    void ItemDatabaseSetup()
    {
        //ship array
        Debug.Log("creating Ship Array");
        shipArr[0] = new Ship("Saucer        ", "Classic ship", 0, true, true);
        shipArr[1] = new Ship("Redneck Rescue", "Heavy, drops fast", 5000);
        shipArr[2] = new Ship("Moon Buggy    ", "Ascends fast", 30000);
        shipArr[3] = new Ship("Camo Carrier  ", "Camouflaged from Missiles", 150000);
        shipArr[4] = new Ship("Microbus      ", "Holds 6", 500000);
        shipArr[5] = new Ship("Rescue Pod    ", "Flame Jets kill ninjas, not noobs", 2000000);
        shipArr[6] = new Ship("Rescue Chopper", "Tail Gun", 5000000);
        shipArr[7] = new Ship("Submarine     ", "Can take a hit", 8000000);
        shipArr[8] = new Ship("Armoured Beast", "Can take 2 hits", 15000000);
        shipArr[9] = new Ship("Mini          ", "Fun size, fits through tight spaces", 20000000);
        //gear array
        gearArr[0] = new Gear("Loot Magnet   ", "Attracts powerups", 20);
        gearArr[1] = new Gear("Shrinker      ", "Fit through small spaces", 1000);
        gearArr[2] = new Gear("Ghost Ship    ", "Don't crush noobs", 15000);
        gearArr[3] = new Gear("Xbow          ", "Inexpensive projectile", 100000);
        gearArr[4] = new Gear("Rifle         ", "Good range, slow fire rate", 250000);
        gearArr[5] = new Gear("Shotgun       ", "Short range, skatterific", 800000);
        gearArr[6] = new Gear("Missile Tube  ", "Clear a path quickly", 1200000);
        gearArr[7] = new Gear("Flame Thrower ", "Destroy ice and gas", 3000000);
        gearArr[8] = new Gear("Freeze Gun    ", "Freeze fire and acid", 5000000);
        gearArr[9] = new Gear("Minigun       ", "No explanation needed", 8000000);
        gearArr[10] = new Gear("Abduction Ray", "Get noobs without landing", 10000000);
        gearArr[11] = new Gear("Super Shield ", "Take a hit, keep on fighting", 20000000);
        //buffs array
        buffArr[0] = new Buff("Remove Ads      ", "Remove video ads", 2, 2);
        buffArr[1] = new Buff("Unlimited Ships ", "Best Deal!!", 5, 2);
        buffArr[2] = new Buff("Extra Free Ship ", "Larger free ship maximum", 100000, 0);
        buffArr[3] = new Buff("PU Spawn Speed 1", "More gear per second", 50000, 0);
        buffArr[4] = new Buff("PU Spawn Speed 2", "Even more gear per second", 1000000, 0);
        buffArr[5] = new Buff("PU Spawn Speed 3", "Even more more gear per second", 10000000, 0);
        //loot array
        lootArr[0] = new Loot("Nugget Bucket   ", "500,000  ", 500, 1, 0, 500000);
        lootArr[1] = new Loot("Nugget Heap     ", "250,000  ", 2500, 1, 0, 2500000);
        lootArr[2] = new Loot("Nugget Truckload", "1,000,000", 10000, 1, 0, 1000000);
        lootArr[3] = new Loot("Noobie Rubies   ", "500      ", 5, 2, 1, 500);
        lootArr[4] = new Loot("Bag o' Rubies   ", "1,200    ", 10, 2, 1, 1200);
        lootArr[5] = new Loot("Mess o' Rubies  ", "2,500    ", 20, 2, 1, 2500);
        lootArr[6] = new Loot("Barrel o' Rubies", "7,000    ", 50, 2, 1, 7000);
        lootArr[7] = new Loot("Ruby Truckload  ", "15,000   ", 99, 2, 1, 15000);
    }

    public void SaveGame()
    {
        //saveload methodology taken from https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/NNSave.dat");//create file

        //these variables must be added below in the serializable class PlayerData or they will not work
        PlayerData data = new PlayerData();//create data container
        data.level = level;//serialze variables
        data.noobNuggets = noobNuggets;
        data.rubies = rubies;
        data.currentShip = currentShip;

        //Cheats
        data.godMode = godMode;
        data.hurryBombOff = hurryBombOff;

        bf.Serialize(file, data);//write to file
        file.Close();
    }

    public void LoadSaveGame()
    {
        if(File.Exists(Application.persistentDataPath + "/NNSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/NNSave.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //these variables must be added below in the [serializable] class PlayerData or they will not work (This is BELOW the GameControl class!!!!)
            level = data.level;
            noobNuggets = data.noobNuggets;
            rubies = data.rubies;
            currentShip = data.currentShip;

            //Cheats
            godMode = data.godMode;
            hurryBombOff = data.hurryBombOff;
        }
    }

    void SetScoreIncrement()
    {
        // set scoreInc
        switch (level)
        {
            case 0: scoreInc = 1; break;
            case 1: scoreInc = 2; break;
            case 2: scoreInc = 3; break;
        }
    }
}

[Serializable] //needed for each variable so it can be put into the data.serialzedStructure above in Save().
class PlayerData
{
    public int level;
    public int noobNuggets;
    public int rubies;
    public int currentShip;

    //Cheats
    public bool godMode;
    public bool hurryBombOff;
}
