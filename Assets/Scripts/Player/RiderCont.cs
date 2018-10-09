using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderCont : MonoBehaviour
{

    Animator anim;
    //public int myNumber;
    
    // Use this for initialization
	void Start ()
    {
		anim = GetComponent<Animator>();
        StartCoroutine(Blink());
    }

    //public void TurnOnOff()
    //{
    //    int getRiders = GameControl.control.riders;
    //    if(getRiders >= myNumber)
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //    }
    //}

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(2,9));
        anim.SetInteger("AnimState", 1);//1 = blink
        StartCoroutine(UnBlink());
    }

    IEnumerator UnBlink()
    {
        yield return new WaitForSeconds(.3f);
        anim.SetInteger("AnimState", 0);//1 = unBlink
        StartCoroutine(Blink());
    }
}
