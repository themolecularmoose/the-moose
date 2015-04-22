using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleHelp : MonoBehaviour {

	public GameObject Canvas; 

	public bool helpUp = true; 
	
	// Use this for initialization
	void Start () {
		// Untoggle the canvas
		//Canvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("h")) {
			if(helpUp == true)
			{
				Canvas.gameObject.SetActive (false); 
				Cursor.visible = false; 
				Screen.lockCursor = true;
				helpUp = false;
			}
			else{
				Canvas.gameObject.SetActive (true); 
				Cursor.visible = true;
				Screen.lockCursor = false; 
				helpUp = true;
			}
		}
	}
}
