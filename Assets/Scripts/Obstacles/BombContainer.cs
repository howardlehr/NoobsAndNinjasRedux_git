using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombContainer : MonoBehaviour {

    public GameObject[] bombs;

    public void DropBomb()
    {
        foreach(GameObject b in bombs)
        {
            if (b)
            {
                if (b.activeSelf == false)
                {
                    b.SetActive(true);
                    break;
                }
            }
        }
    }
}
