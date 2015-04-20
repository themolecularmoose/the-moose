using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FieldGuide : MonoBehaviour {

	public struct Molecule
	{
		public string name; // eg. "Hydrogen Dioxide"
		public string flavorText; // eg. "This molecule is absolutely necessary for life. It is composed of two oxygens and a hydrogen."
		public int numCollected; // eg. "0"

		public Molecule(string name, string flavor, int numCollected)
		{
			this.name = name; 
			this.flavorText = flavor; 
			this.numCollected = numCollected;
		}
	}

	public GameObject Canvas; 
	public Text moleculeName; 

	public List<Molecule> moleculeInfo;
	public bool guideUp = false; 
	// Use this for initialization
	void Start () {
		Canvas.gameObject.SetActive (false);
		moleculeInfo = new List<Molecule> ();
		moleculeName.text = "Hydrogen Dioxide";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
		if(guideUp == true)
			{
				Canvas.gameObject.SetActive (false); 
				Screen.showCursor = false; 
				Screen.lockCursor = true;
				guideUp = false;
			}
			else{
				Canvas.gameObject.SetActive (true); 
				Screen.showCursor = true;
				Screen.lockCursor = false; 
				guideUp = true;
			}
		}
	}
}
