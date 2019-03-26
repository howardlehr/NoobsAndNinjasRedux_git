using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour {

    //public Animator anim;

    //private void OnEnable()
    //{
    //    anim = GetComponent<Animator>();
    //}

    public void Reset()
    {
        //anim.SetInteger("AnimState", 0);
        gameObject.SetActive(false);
    }

    //private void OnDisable()
    //{
    //    Reset();
    //}
}
