using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoobControl : MonoBehaviour {

    GameObject player;
    Animator anim;
    public GameObject mySpeechBubble;
    
    Rigidbody2D myRigidBody;
    GameObject[] noobs;

    public float noobRunSpeed;
    public bool dead;
    float localScaleX;

    public IEnumerator sayHelp;
    public IEnumerator blink;
    public IEnumerator unBlink;
    
    void Awake()
    {
        noobRunSpeed = 2f;
        player = GameObject.Find("Player");
        anim = GetComponent<Animator> ();
        localScaleX = transform.localScale.x;
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpeechBubble = transform.Find("speech_bubble").gameObject;
        sayHelp = SayHelp();//making variable so it's stoppable
        StartCoroutine(sayHelp);
        blink = Blink();
        StartCoroutine(blink);
        unBlink = UnBlink();
    }

    private void OnDisable()
    {
        StopCoroutine(sayHelp);
        StopCoroutine(blink);
        StopCoroutine(unBlink);
    }

    //void Update ()
    //   {
    //       if (Vector2.Distance(transform.position, player.transform.position) < 10)
    //       {
    //           if (player.transform.position.y < -4.42)
    //           {
    //               transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), noobRunSpeed);//move
    //               transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
    //               anim.SetInteger("AnimState", 3);//1 = run
    //           }
    //           else
    //           {
    //               anim.SetInteger("AnimState", 0);//0 = idle
    //           }
    //       }
    //   }

    private void FixedUpdate()//Stop running if button touched
    {
        if(Input.GetButtonDown("Fire1") && !dead)
        {
            myRigidBody.velocity = new Vector2(0, 0);
            anim.SetInteger("AnimState", 0);//0 = idle
        }
    }

    public void RunToShip()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 10 && !dead)
        {
            myRigidBody.velocity = new Vector2(noobRunSpeed * (Mathf.Sign(player.transform.position.x - transform.position.x)), 0);
            transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * localScaleX, transform.localScale.y);//facing direction
            if(mySpeechBubble)
            {
                mySpeechBubble.GetComponent<Talk>().FixBackwardText(Mathf.Sign(transform.localScale.x));
            }
            anim.SetInteger("AnimState", 3);
        }
    }

    public void StopRunning()
    {
        if (!dead)
        {
            myRigidBody.velocity = new Vector2(0, 0);
            anim.SetInteger("AnimState", 0);
        }

    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(2, 9));
        var curState = anim.GetInteger("AnimState");
        if (!dead && curState != 3)
        {
            anim.SetInteger("AnimState", 1);
        }
        //StartCoroutine(unBlink);
        StartCoroutine(UnBlink());
    }

    IEnumerator UnBlink()
    {
        yield return new WaitForSeconds(.3f);
        var curState = anim.GetInteger("AnimState");
        if(!dead && curState != 3)
        {
            anim.SetInteger("AnimState", 0);
        }
        //StartCoroutine(blink);
        StartCoroutine(Blink());
    }

    IEnumerator SayHelp()
    {
        //Debug.Log("SayHelp started");
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        var curState = anim.GetInteger("AnimState");
        if (!dead && curState != 3)
        {
            mySpeechBubble.GetComponent<Talk>().Say("Help!");
            mySpeechBubble.GetComponent<Talk>().FixBackwardText(Mathf.Sign(transform.localScale.x));
        }
        sayHelp = SayHelp();
        StartCoroutine(sayHelp);
    }

    public void ShipFull()
    {
        if (!dead)
        {
            if (sayHelp != null)
            {
                StopCoroutine(sayHelp);
            }
            mySpeechBubble.GetComponent<Talk>().Say("It's Full");
            mySpeechBubble.GetComponent<Talk>().FixBackwardText(Mathf.Sign(transform.localScale.x));
            sayHelp = SayHelp();
            StartCoroutine(sayHelp);
        }
    }

    //IEnumerator Talk(string myText)
    //{
    //    yield return new WaitForSeconds(Random.Range(2f,8f));
    //    mySpeechBubble.SetActive(true);
    //    FixBackwardText();
    //    myTextBox.text = myText;
    //    StartCoroutine(stopTalking);
    //}

    //public void FixBackwardText()
    //{
    //    if (transform.localScale.x < 1)
    //    {
    //        myTextBox.transform.localScale = new Vector2(Mathf.Abs(myTextBox.transform.localScale.x) * -1, myTextBox.transform.localScale.y);
    //    }
    //    else
    //    {
    //        myTextBox.transform.localScale = new Vector2(Mathf.Abs(myTextBox.transform.localScale.x), myTextBox.transform.localScale.y);
    //    }
    //}

    //IEnumerator StopTalking()
    //{
    //    yield return new WaitForSeconds(1f);
    //    mySpeechBubble.SetActive(false);
    //    myTextBox.text = "???";
    //    StartCoroutine(sayHelp);
    //}

    public void HeadOff()
    {
        myRigidBody.velocity = new Vector2(0, 0);
        anim.SetInteger("AnimState", 4);
        dead = true;
        tag = "Untagged";
        player.GetComponent<PlayerControl>().EndLevelTest();
    }
}
