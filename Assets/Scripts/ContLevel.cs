using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContLevel : MonoBehaviour
{
    public GameObject[] room = new GameObject[3];
	public GameObject oPowerSource;
    //public GameObject oTeleporterBase;

    private GameObject roomR;
    private GameObject roomL;
    private GameObject powerSourceL;
    private GameObject powerSourceR;
    private float screenWidthInPoints;
    private int numberOfRooms = 3;
    private int roomNumber;

    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        //create rooms
        //room[0] = new GameObject();
        //room[1] = new GameObject();
        //room[2] = new GameObject();

        int i;
        for (i = 1; i < numberOfRooms; i++)
        {
            roomNumber = Random.Range(0,3);
            roomR = Instantiate(room[roomNumber]);
            roomR.transform.position = new Vector2(screenWidthInPoints * i, 0);
            if (i == numberOfRooms - 1)
            {
                powerSourceR = Instantiate(oPowerSource);
                powerSourceR.transform.position = new Vector2(screenWidthInPoints * i + screenWidthInPoints * .5f,0.02f);
            }
            roomNumber = Random.Range(0,3);
            roomL = Instantiate(room[roomNumber]);
            roomL.transform.position = new Vector2(screenWidthInPoints * -i, 0);
            if (i == numberOfRooms - 1)
            {
                powerSourceL = Instantiate(oPowerSource);
                powerSourceL.transform.position = new Vector2(screenWidthInPoints * -i - screenWidthInPoints * .5f, 0.02f);
            }
        }

    }
}
