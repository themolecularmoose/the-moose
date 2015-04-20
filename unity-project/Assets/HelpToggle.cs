using UnityEngine;
using System.Collections;

public class HelpToggle : MonoBehaviour {

	public bool helpUp = true; 
	public GameObject Canvas; 
	// Use this for initialization
	void Start () {
		//Canvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("h")) {
			if(helpUp == true)
			{
				Canvas.gameObject.SetActive (false); 
				Screen.showCursor = false; 
				Screen.lockCursor = true;
				helpUp = false;
			}
			else{
				Canvas.gameObject.SetActive (true); 
				Screen.showCursor = true;
				Screen.lockCursor = false; 
				helpUp = true;
			}
		}
	}
}
