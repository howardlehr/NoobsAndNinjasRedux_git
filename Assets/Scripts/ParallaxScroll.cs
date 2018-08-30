using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Renderer BgBack;
    public Renderer BgMid;
    public Renderer BgFront;

    public float BgMidSpeed = 0.025f;
    public float BgFrontSpeed = 0.052f;
    public float offset = 0;

    void LateUpdate()
    {
        float BgMidOffset = offset * BgMidSpeed;
        float BgFrontOffset = offset * BgFrontSpeed;

        BgMid.material.mainTextureOffset = new Vector2(BgMidOffset, 0);
        BgFront.material.mainTextureOffset = new Vector2(BgFrontOffset, 0);
    }
}
