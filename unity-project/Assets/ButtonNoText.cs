using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class ButtonNoText : MonoBehaviour {

	Text buttonText; 
	// Use this for initialization
	void Start () {
		buttonText = transform.FindChild("Text").GetComponent<Text>();
		buttonText.text = ""; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
