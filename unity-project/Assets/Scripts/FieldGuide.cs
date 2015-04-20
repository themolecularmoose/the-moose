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
	public Text flavorText;

	public List<Molecule> moleculeInfo;
	public bool guideUp = false; 
	// Use this for initialization
	void Start () {
		Canvas.gameObject.SetActive (false);
		moleculeInfo = new List<Molecule> ();
		moleculeInfo.Add (new Molecule ("Hydrogen Dioxide", "It's water!", 0)); 
		moleculeInfo.Add (new Molecule ("Silicon Dioxide", "Solar panels", 0)); 
		moleculeInfo.Add (new Molecule ("Carbon", "Fuel", 0)); 
		moleculeInfo.Add (new Molecule ("Sodium Chloride", "Salt", 0)); 
		moleculeInfo.Add (new Molecule ("Carbon Dioxide", "Consumed by plants", 0)); 
		moleculeInfo.Add (new Molecule ("Hydrogen Chloride", "Acidic and highly corrosive", 0)); 
		moleculeInfo.Add (new Molecule ("Methane", "Fuel / natural gas", 0)); 
		moleculeInfo.Add (new Molecule ("Ammonia", "Fertilizer and cleaner.", 0)); 
		moleculeInfo.Add (new Molecule ("Trinitrotoluene", "TNT: Explosive!", 0)); 

		LoadMolInfo (moleculeInfo [0]);
	}

	void LoadMolInfo(Molecule m)
	{
		moleculeName.text = m.name;
		flavorText.text = m.flavorText;
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
