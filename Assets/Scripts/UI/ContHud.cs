using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContHud : MonoBehaviour {

    public Text levelText;
    public Text nuggetText;
    public Text rubyText;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //levelNumber = levelText.GetComponent<Text>();
        levelText.text = GameControl.control.level.ToString("N0");
        nuggetText.text = GameControl.control.noobNuggets.ToString("N0");
        rubyText.text = GameControl.control.rubies.ToString("N0");
    }
}
