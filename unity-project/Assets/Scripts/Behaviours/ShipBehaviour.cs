using UnityEngine;
using System.Collections;
using System.Linq;
using System.Threading;

public class ShipBehaviour : MonoBehaviour {
	private ShipAudio sa;
	private const int MAX_BEAM_ENERGY = 100; // "Constant" - not sure if this should be upgradeable.
	private const float MAX_HEALTH = 100.0f; // "Constant" - not sure if this should be upgradeable?
	private bool tractorBeam;
	private int beamEnergy;
	private float health;
	private float maxTurnRate = 5;

	// Convience var for modifing damage upwards
	private float damageScalar = 0.5f;
	// List of object tags that can cause damage
	private string[] damagers = {"Wall", "Obstacle", "Collectables"};
	// Ensures order of damage taken
	private static Mutex _m;

	private EventPublisher eventPublisher;
	public GameObject m_attachments;
	public GameObject m_avatar;
	
	//store so we can shoot these later
	GameObject m_buster;

	public delegate void PlayerContactEvent(Collision collision);
	public event PlayerContactEvent OnContact;

	void OnEnable() 
	{
		_m = new Mutex ();
		beamEnergy = MAX_BEAM_ENERGY;
		tractorBeam = false;
		health = MAX_HEALTH;
		sa = gameObject.GetComponent<ShipAudio> ();
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
			forceSpread = Mathf.FloorToInt(force) / pointsOfContact;
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
			sa.PlayDamage();
			if(health <= 0) {
				Die();
			}
		}
	}

	public void Die()
	{
		//confirm death
		health = 0;
		gameObject.rigidbody.velocity = Vector3.zero;
		//disable the visualization
		setVisibility(false);
		//inform first of kin
		eventPublisher.publish (new DeathEvent());
		//disable this behaviour
		enabled = false;
	}

	void setVisibility(bool isVisible)
	{
		for (int i = 0; i < m_avatar.transform.childCount; ++i) {
			var child = m_avatar.transform.GetChild(i);
			child.gameObject.SetActive(isVisible);
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
		sa.PlayBoost ();
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
		/*
		 * Temporarily Disabling
		 * if (!damagers.Contains (collidedWithTag)) {
			return;
		}*/
		_m.WaitOne();
		float damage = CalcDamage (collision);
		eventPublisher.publish (new DamageEvent(damage, health, MAX_HEALTH));
		DecreaseHealth (damage);
		_m.ReleaseMutex ();

		if (OnContact != null) {
			OnContact (collision);
		}
	}

	public void Respawn()
	{
		enabled = true;
		setVisibility (true);
	}

	// Use this for initialization
	void Start () {
		if (m_attachments == null)
			Debug.Log ("Error: Attachments not set in player. These should be set by default.");
		if (m_avatar == null)
			Debug.Log ("Error: Avatar is not set in player. This should be loaded by default.");
		m_buster = (GameObject)Resources.Load("Prefabs/Buster");
		eventPublisher = GameObject.Find("Level").GetComponent<EventPublisher>();
	}
	
	public void Strafe(float a_speed)
	{
		rigidbody.AddForce(m_attachments.transform.right * a_speed);
	}
	public void Lean(float a_speed, float a_max)
	{
		float value = rigidbody.angularVelocity.y;
		float sign = Mathf.Sign(a_speed);
		float speed = value * sign;
		float momentum = speed * rigidbody.mass;
		float dif = a_max - momentum;
		if (dif > 0) {
			rigidbody.AddRelativeTorque (0, dif * sign, 0);
		}
	}
	public void Tilt(float a_speed)
	{
		//broken
		if (rigidbody.angularVelocity.x < maxTurnRate) {
			rigidbody.AddRelativeTorque (a_speed, 0, 0);
		}
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