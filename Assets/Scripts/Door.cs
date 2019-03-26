using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	void OnEnable()
    {
        StartCoroutine(CloseDoor());
	}

    private void OnDisable()
    {
        StopCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(.15f);
        gameObject.SetActive(false);
    }
}
