using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlaceSelf();
    }

    void PlaceSelf()
    {
        transform.position = new Vector2(transform.position.x + Random.Range(-3f, 3f), transform.position.y + Random.Range(-3f, 3f));
    }
}