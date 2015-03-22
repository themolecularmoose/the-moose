using UnityEngine;
using System.Collections;
using System.Linq;
using System.Threading;

public class ShipBehaviour : MonoBehaviour {
	private bool tractorBeam;
	private int beamEnergy;
	private const int MAX_BEAM_ENERGY = 100; // "Constant" - not sure if this should be upgradeable.
	private float health;
	private const float MAX_HEALTH = 100.0f; // "Constant" - not sure if this should be upgradeable?

	// Convience var for modifing damage upwards
	private float damageScalar = 1;
	// List of object tags that can cause damage
	private string[] damagers = {"Wall", "Obstacle"};
	// Ensures order of damage taken
	private static Mutex _m;

	private EventPublisher eventPublisher;
	private GameObject m_attachments;
	
	//store so we can shoot these later
	GameObject m_buster;

	void OnEnable() 
	{
		_m = new Mutex ();
		beamEnergy = MAX_BEAM_ENERGY;
		tractorBeam = false;
		health = MAX_HEALTH;
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
		float forceSpread = 0; // Default to one damamge for any collision
		if (force > pointsOfContact && pointsOfContact != 0) {
			// Naively spread damamge over points of collision
			forceSpread = Mathf.FloorToInt (force) / pointsOfContact;
		}
		return forceSpread * damageScalar;
	}
	
	public void Climb(float a_speed)
	{
		rigidbody.AddForce(Vector3.up * a_speed);
	}
	
	public void DecreaseHealth(float damage) 
	{
		if(health > 0) {
			health -= damage;
			if(health <= 0) {
				eventPublisher.publish (new DeathEvent());
			}
		}
	}

	public void FireBuster()
	{
		Instantiate(m_buster, m_attachments.transform.position + m_attachments.transform.forward * 2, m_attachments.transform.rotation);
	}

	public float Health
	{
		get{ return health;}
		set{ health = value;}
	}

	public void JumpDrive(float a_strength)
	{
		rigidbody.velocity += m_attachments.transform.forward * a_strength;
	}

	public float MaxHealth
	{
		get{ return MAX_HEALTH;}
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
		eventPublisher.publish (new DamageEvent(damage, health, MAX_HEALTH));
		DecreaseHealth (damage);
		_m.ReleaseMutex ();
	}

	// Use this for initialization
	void Start () {
		m_buster = (GameObject)Resources.Load("Prefabs/Buster");
		eventPublisher = GameObject.Find("Level").GetComponent<EventPublisher>();
		m_attachments = transform.Find("Attachments").gameObject;
	}
	
	public void Strafe(float a_speed)
	{
		rigidbody.AddForce(m_attachments.transform.right * a_speed);
	}

	public void Thrust(float a_speed)
	{
		rigidbody.AddForce(m_attachments.transform.forward * a_speed);
	}

	public bool TractorBeam
	{
		get{ return tractorBeam;}
		set{ tractorBeam = value;}
	}

	void Update()
	{
		rigidbody.velocity *= 0.95f;
	}
}