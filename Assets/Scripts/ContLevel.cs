using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContLevel : MonoBehaviour
{
    public GameObject player;
    public GameObject[] roomObst = new GameObject[3];
    public GameObject[] roomNoob = new GameObject[3];
    public GameObject oPowerSource;
    public GameObject pad;
    //public GameObject[] rooms;

    private GameObject thisRoom;
    private GameObject powerSourceL;
    private GameObject powerSourceR;
    private float screenWidthInPoints;
    private int numberOfRooms;
    private int roomNumber;
    private GameObject[] roomType;

    void Start()
    {
        numberOfRooms = 2;
        CreateRooms();
        player.GetComponent<PlayerControl>().FindNoobs();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CreateRooms()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        int i;
        for (i = 1; i <= numberOfRooms; i++)
        {
            switch (i)
            {
                case 0:
                case 1: roomType = roomObst; break;
                case 2: roomType = roomNoob; break;
                case 3: roomType = roomObst; break;
                case 4: roomType = roomNoob; break;
            }

            roomNumber = Random.Range(0, 3);
            thisRoom = Instantiate(roomType[roomNumber]);
            thisRoom.transform.position = new Vector2(screenWidthInPoints * i, 0);
            pad = Instantiate(pad);
            pad.transform.position = new Vector2(screenWidthInPoints * i - screenWidthInPoints * .5f, 0.02f);
            if (i == numberOfRooms)
            {
                powerSourceR = Instantiate(oPowerSource);
                powerSourceR.transform.position = new Vector2(screenWidthInPoints * i + screenWidthInPoints * .5f, 0.02f);
            }
            roomNumber = Random.Range(0, 3);
            thisRoom = Instantiate(roomType[roomNumber]);
            thisRoom.transform.position = new Vector2(screenWidthInPoints * -i, 0);
            pad = Instantiate(pad);
            pad.transform.position = new Vector2(screenWidthInPoints * -i + screenWidthInPoints * .5f, 0.02f);
            if (i == numberOfRooms)
            {
                powerSourceL = Instantiate(oPowerSource);
                powerSourceL.transform.position = new Vector2(screenWidthInPoints * -i - screenWidthInPoints * .5f, 0.02f);
            }
        }
    }
}
