using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public GameObject player;
    public Renderer BgBack;
    public Renderer BgMid;
    public Renderer BgFront;

    public float BgMidSpeed = 0.025f;
    public float BgFrontSpeed = 0.052f;
    public float offset = 0;

    public Material[] BgBackArr;
    public Material[] BgMidArr;
    public Material[] BgFrontArr;

    private void Start()
    {
        BackgroundSwap();
    }

    void LateUpdate()
    {
        offset = player.transform.position.x;
        float BgMidOffset = offset * BgMidSpeed;
        float BgFrontOffset = offset * BgFrontSpeed;

        BgMid.material.mainTextureOffset = new Vector2(BgMidOffset, 0);
        BgFront.material.mainTextureOffset = new Vector2(BgFrontOffset, 0);
    }

    public void BackgroundSwap()
    {
        BgBack.material = new Material(BgBackArr[GameControl.control.level]);
        BgMid.material = new Material(BgMidArr[GameControl.control.level]);
        BgFront.material = new Material(BgFrontArr[GameControl.control.level]);
    }
}


