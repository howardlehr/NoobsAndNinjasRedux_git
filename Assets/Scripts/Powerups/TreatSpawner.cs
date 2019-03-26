using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject[] nugget;
    public Rigidbody2D[] rbNugget;
    public GameObject[] nuggetAnimated;
    public GameObject[] ruby;
    public Rigidbody2D[] rbRuby;
    public GameObject[] rubyAnimated;
    public GameObject power_source_r;
    public GameObject power_source_l;

    int chooser;
    Vector2 playerFacing;

    IEnumerator spawnLoot;

	void OnEnable ()
    {
        spawnLoot = SpawnLoot();
        StartCoroutine(spawnLoot);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void FindPowerSources()
    {
        power_source_l = GameObject.Find("power_source_l");
        power_source_r = GameObject.Find("power_source_r");
    }

    //private void FixedUpdate()
    //{
    //    //constantVelocity = Vector3.Lerp(constantVelocity, new Vector3(playerInputX, playerInputY, 0) * speed, 0.05f);
    //    //transform.position = Vector2.Lerp(transform.position, new Vector2(Random.Range(2f, 5f) * -playerFacing.x,transform.position.y), Time.deltaTime * .005f);
    //}

    IEnumerator SpawnLoot()
    {
        yield return new WaitForSeconds(Random.Range(6f,12f));
        if (player)
        {
            playerFacing = player.transform.localScale;
            transform.position = new Vector2(player.transform.position.x + (15f * playerFacing.x), Random.Range(-3f, 3f));
            //if spawner is outside
            if (gameObject.transform.position.x < power_source_l.transform.position.x)
            {
                transform.position = new Vector2(player.transform.position.x + 15f, transform.position.y);
                playerFacing *= -1;
            }
            if (gameObject.transform.position.x > power_source_r.transform.position.x)
            {
                transform.position = new Vector2(player.transform.position.x - 15f, transform.position.y);
                playerFacing *= -1;
            }
            chooser = Random.Range(0, 10);
            if (chooser < 5)
            {
                int i;
                for (i = 0; i < 3; i++)
                {
                    if (nugget[i].activeSelf == false)
                    {
                        nugget[i].SetActive(true);
                        nugget[i].transform.position = gameObject.transform.position;
                        rbNugget[i].velocity = new Vector2(Random.Range(3f, 4f) * -playerFacing.x, 0);
                        nuggetAnimated[i].GetComponent<TreatFly>().myRotationSpeed = Random.Range(2f, 4f) * playerFacing.x;
                        break;
                    }
                }
            }
            else
            {
                int i;
                for (i = 0; i < 3; i++)
                {
                    if (ruby[i].activeSelf == false)
                    {
                        ruby[i].SetActive(true);
                        ruby[i].transform.position = gameObject.transform.position;
                        rbRuby[i].velocity = new Vector2(Random.Range(3f, 4f) * -playerFacing.x, 0);
                        rubyAnimated[i].GetComponent<TreatFly>().myRotationSpeed = Random.Range(2f, 4f) * playerFacing.x;
                        break;
                    }
                }
            }
        }
        spawnLoot = SpawnLoot();
        StartCoroutine(spawnLoot);
    }
}
