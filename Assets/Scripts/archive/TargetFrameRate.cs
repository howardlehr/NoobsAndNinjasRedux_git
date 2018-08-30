using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int targetFrameRate = 60;

	// Use this for initialization
	void Start ()
    {
        QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 30;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (targetFrameRate != Application.targetFrameRate)
        {
            Application.targetFrameRate = targetFrameRate;
        }
	}
}
