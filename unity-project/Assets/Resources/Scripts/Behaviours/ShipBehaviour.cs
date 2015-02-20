using UnityEngine;
using System.Collections;

public class ShipBehaviour : MonoBehaviour {
	private int beamEnergy;
	private int collected;
	private int waterCount;
	private int methaneCount;
	public ArrayList collected_collectables;
	private float health;
	private bool tractorBeam;

	// Use this for initialization
	void Start () {
		collected_collectables = new ArrayList();
		beamEnergy = 100;
		waterCount = 0;
		methaneCount = 0;
		collected = 0;
		tractorBeam = false;
		waterCount = 0;
		methaneCount = 0;
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Move to controller
	/// </summary>
	void FixedUpdate () {
		if(Input.GetButton("Tractor Beam"))
		{
			beamState(true);
		}
		else
		{
			beamState(false);
		}
	}
	
	public void beamState(bool state)
	{
		if(beamEnergy <= 0)
		{
			tractorBeam = false;
		}
		else
		{
			tractorBeam = state;
		}
	}
}
