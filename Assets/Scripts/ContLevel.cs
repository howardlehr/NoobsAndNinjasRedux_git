using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContLevel : MonoBehaviour
{
    public GameObject player;
    public GameObject power_source;
    public GameObject room_marker;
    public GameObject treat_spawner;

    private GameObject thisRoom;
    private GameObject power_source_r;
    private GameObject power_source_l;

    private float screenWidthInPoints;
    private float rightEdge;
    private float leftEdge;
    private int numberOfRooms;
    public int roomsRight;
    public int roomsLeft;
    private int roomNumber;
    private List<GameObject> roomType;
    private int newRoom;
    public GameObject newFloor;
    public BoxCollider2D newBox;
    public float roomWidth;
    
    //level arrays
    public List<GameObject> roomObst;
    public List<GameObject> roomNoob;
    public List<GameObject> level_0_obst;
    public List<GameObject> level_0_noob;
    public List<GameObject> level_1_obst;
    public List<GameObject> level_1_noob;
    public List<GameObject> level_2_obst;
    public List<GameObject> level_2_noob;

    void Start()
    {
        SetRoomLists();
        CreateRooms();
        player.GetComponent<PlayerControl>().FindNoobs();
    }

    void SetRoomLists()
    {
        switch (GameControl.control.level)
        {
            case 0:
                roomsRight = 2;
                roomsLeft = 2;
                roomObst = level_0_obst;
                roomNoob = level_0_noob;
                break;
            case 1:
                roomsRight = 2;
                roomsLeft = 2;
                roomObst = level_1_obst;
                roomNoob = level_1_noob;
                break;
            case 2:
                roomsRight = 2;
                roomsLeft = 2;
                roomObst = level_2_obst;
                roomNoob = level_2_noob;
                break;
        }
    }

    void CreateRooms()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        newRoom = 0;
        rightEdge = screenWidthInPoints * .5f;
        for (newRoom = 0; newRoom < roomsRight; newRoom++)
        {
            OddEven(newRoom);
            
            roomNumber = Random.Range(0, roomType.Count);
            thisRoom = Instantiate(roomType[roomNumber]);
            roomWidth = thisRoom.GetComponent<Room>().myWidth;
            //Debug.Log(roomWidth);
            thisRoom.transform.position = new Vector2(rightEdge + roomWidth, 0);
            room_marker = Instantiate(room_marker);
            room_marker.transform.position = new Vector2(rightEdge, 0.02f);
            rightEdge += roomWidth * 2;
            if (newRoom == roomsRight-1)
            {
                power_source_r = Instantiate(power_source);
                power_source_r.transform.position = new Vector2(rightEdge, 0.02f);
                power_source_r.name = "power_source_r";
            }
        }

        newRoom = 0;
        leftEdge = -screenWidthInPoints * .5f;
        for (newRoom = 0; newRoom < roomsLeft; newRoom++)
        {
            OddEven(newRoom);

            roomNumber = Random.Range(0, roomType.Count);
            thisRoom = Instantiate(roomType[roomNumber]);
            roomWidth = thisRoom.GetComponent<Room>().myWidth;
            //Debug.Log(roomWidth);
            thisRoom.transform.position = new Vector2(leftEdge - roomWidth, 0);
            room_marker = Instantiate(room_marker);
            room_marker.transform.position = new Vector2(leftEdge, 0.02f);
            leftEdge -= roomWidth * 2;
            if (newRoom == roomsLeft-1)
            {
                power_source_l = Instantiate(power_source);
                power_source_l.transform.position = new Vector2(leftEdge, 0.02f);
                power_source_l.name = "power_source_l";
            }
        }

        treat_spawner.GetComponent<TreatSpawner>().FindPowerSources();
    }

    List<GameObject> OddEven(int newRoom)
    {
        //test for odd/even
        if (newRoom % 2 == 0)
        {
            roomType = roomObst;
        }
        else
        {
            roomType = roomNoob;

        }
        return roomType;
    }
}