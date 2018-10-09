using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNoobs : MonoBehaviour {

    public GameObject[] myNoobs = new GameObject[2];

    public int noobAwake;

	// Use this for initialization
	void Start ()
    {
        noobAwake = Random.Range(0, 3);
        if (noobAwake > 0)
            DestroyObject(myNoobs[0]);

        if (noobAwake > 1)
            DestroyObject(myNoobs[1]);
    }
}