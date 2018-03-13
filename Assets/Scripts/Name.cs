using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour {

    public InputField InputName;
	// Use this for initialization
	void Start () {
        if(PlayerPrefs.HasKey("name") == false)
        {
            PlayerPrefs.SetString("name", "毛球");
        }
        InputName.text = PlayerPrefs.GetString("name");	
	}

    public void SetPlayName(string value)
    {
        PlayerPrefs.SetString("name", value);
    }
	
}
