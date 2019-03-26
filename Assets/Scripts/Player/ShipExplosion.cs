using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartAnim(Random.Range(.001f, .5f)));
    }

    IEnumerator StartAnim(float mySeconds)
    {
        yield return new WaitForSeconds(mySeconds);
        anim.SetTrigger("start");
    }

    public void TurnMeOff()
    {
        Destroy(gameObject);
    }
}
