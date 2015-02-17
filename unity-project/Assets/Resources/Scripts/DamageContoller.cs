using UnityEngine;
using System.Collections;
using System.Linq;
using System.Threading;

public class DamageContoller : MonoBehaviour {

	// List of objects that can cause damage
	private string[] damagers = {"Wall", "Obsticle"};

	// Convience var for modifing damage upwards
	private float damageScalar = 1;

	// Ensures order of damage taken
	private static Mutex _m;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		_m = new Mutex ();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.GetHealth() <= 0) {
			gameController.SendMessage("OnDeath");
		}
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
		gameController.SendMessage ("OnDamage", damage);
		_m.ReleaseMutex ();
	}

	float CalcDamage(Collision hit) {
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
}
