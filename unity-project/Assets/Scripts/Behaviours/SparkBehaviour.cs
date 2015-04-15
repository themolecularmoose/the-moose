using UnityEngine;
using System.Collections;

public class SparkBehaviour : MonoBehaviour {
	public GameObject m_spark;
	public ShipBehaviour m_player;
	private float lifespan;

	// Use this for initialization
	void Start () {
		if (m_spark == null) {
			throw new UnityException("In player, spark not set. Should be by default!");
		}
		//TODO: check if player is null
		m_player.OnContact += HandleOnContact;
	}

	void HandleOnContact (Collision collision)
	{
		ContactPoint contact = collision.contacts [0];
		particleSystem.transform.position = contact.point;
		particleSystem.transform.forward = contact.normal;

		particleSystem.Play();
		lifespan = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;
		if (lifespan <= 0) {
			lifespan = 0;
			particleSystem.Stop();
		}
	}
}
