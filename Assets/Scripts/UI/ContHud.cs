using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContHud : MonoBehaviour {

    public Text levelText;
    public Text nuggetText;
    public Text rubyText;

    //string levelNumber;
    //string nuggetNumber;
    //string rubyNumber;

	// Use this for initialization
	void Start ()
    {
        //levelNumber = 5;

        
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //levelNumber = levelText.GetComponent<Text>();
        levelText.text = GameControl.control.level.ToString();
        nuggetText.text = GameControl.control.noobNuggets.ToString();
        rubyText.text = GameControl.control.rubies.ToString();
    }
}
