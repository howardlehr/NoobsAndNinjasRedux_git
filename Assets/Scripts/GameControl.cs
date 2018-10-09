using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    //gameplay items
    //public List<GameObject> ninjasList = new List<GameObject>();
    public int riders;

    //UI items
    public int level;
    public int maxLevel = 2;
    public int noobNuggets;
    public int rubies;
    public int scoreInc;
    public int currentShip;

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

        LoadSaveGame();

        SetScoreIncrement();
    }

    //nothing in here yet
    void Update()
    {

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

}
