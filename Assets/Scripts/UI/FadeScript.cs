using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

    SpriteRenderer rend;
    Color myColor;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color myColor = rend.material.color;
    }

    public void StartFade(string inOut, float startTime, float speed)
    {
        if (inOut == "in")
        {
            InvokeRepeating("FadeIn", startTime, speed);
        }
            
        else
            InvokeRepeating("FadeOut", startTime, speed);

    }

    void FadeIn()
    {
        if(rend.material.color.a < 1)
        {
            myColor = rend.material.color;
            myColor.a += .05f;
            rend.material.color = myColor;
        }
        else
        {
            CancelInvoke("FadeIn");
        }
    }

    void FadeOut()
    {
        if (rend.material.color.a > 0)
        {
            myColor = rend.material.color;
            myColor.a -= .05f;
            rend.material.color = myColor;
        }
        else
        {
            CancelInvoke("FadeOut");
            myColor.a = 1;
            rend.material.color = myColor;
            rend.enabled = false;
        }
    }
}
