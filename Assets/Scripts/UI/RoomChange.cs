using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChange : MonoBehaviour
{

    public void LoadRoom(string roomName)
    {
        GameControl.control.SaveGame();
        SceneManager.LoadScene(roomName);
        //Debug.Log("Loaded" + roomName);
    }
}
