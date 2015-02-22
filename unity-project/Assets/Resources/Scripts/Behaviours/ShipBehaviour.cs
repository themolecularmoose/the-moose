using UnityEngine;
using System.Collections;
using System.Linq;
using System.Threading;

public class ShipBehaviour : MonoBehaviour {
	private bool tractorBeam;
	private int beamEnergy;
	private float health;

	// Convience var for modifing damage upwards
	private float damageScalar = 1;
	// List of object tags that can cause damage
	private string[] damagers = {"Wall", "Obsticle"};
	// Ensures order of damage taken
	private static Mutex _m;

	void OnEnable() 
	{
		_m = new Mutex ();
		beamEnergy = 100;
		tractorBeam = false;
		health = 100;
	}

	public int BeamEnergy
	{
		get{ return beamEnergy;}
		set{ beamEnergy = value;}
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

	float CalcDamage(Collision hit) 
	{
		float hitMagnitude = hit.relativeVelocity.magnitude;
		float pointsOfContact = hit.contacts.Length;
		float mass = 0;
		if (hit.rigidbody != null) {
			mass = hit.rigidbody.mass;
		} else {
			mass = 1; // TODO: Brian S -> Add custom variables to game objects to avoid use of rigidbody on static objects
		}
		float force = mass * hitMagnitude;
		float forceSpread = 1; // Default to one damamge for any collision
		if (force > pointsOfContact && pointsOfContact != 0) {
			// Naively spread damamge over points of collision
			forceSpread = Mathf.FloorToInt (force) / pointsOfContact;
		}
		return forceSpread * damageScalar;
	}
	
	void CheckDeath()
	{
		if(health <= 0) {
			gameObject.SendMessageUpwards("OnDeath");
		}
	}
	
	public void DecreaseHealth(float damage) 
	{
		health -= damage;
	}

	/// <summary>
	/// Move to controller
	/// </summary>
	void FixedUpdate () 
	{
		CheckDeath ();
		if(Input.GetButton("Tractor Beam"))
		{
			beamState(true);
		}
		else
		{
			beamState(false);
		}
	}

	public float Health
	{
		get{ return health;}
		set{ health = value;}
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		string collidedWithTag = collision.gameObject.tag;
		
		// If game object collided with is not in damagers list -> exit
		if (!damagers.Contains (collidedWithTag)) {
			return;
		}
		_m.WaitOne();
		float damage = CalcDamage (collision);
		DecreaseHealth (damage);
		gameObject.SendMessageUpwards ("OnDamage", damage);
		_m.ReleaseMutex ();
	}

	public bool TractorBeam
	{
		get{ return tractorBeam;}
		set{ tractorBeam = value;}
	}
}