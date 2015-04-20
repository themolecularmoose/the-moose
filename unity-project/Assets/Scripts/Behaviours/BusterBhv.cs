using UnityEngine;
using System.Collections;

public class BusterBhv : MonoBehaviour {
	public float m_speed;
	public float m_power;
	public float m_lift;
	public float m_lifeSpan;
	public GameObject explosionObject;

	void Die()
	{
		explosionObject.transform.position = transform.position;
		explosionObject.particleSystem.Play();
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider a_other)
	{
		if(a_other.rigidbody == null|| a_other.rigidbody.isKinematic)
		{
			if(a_other.gameObject.tag == "Player")
			{
				return;
			}
			Die();
		}
		a_other.gameObject.SendMessageUpwards("onBusterHit", this, SendMessageOptions.DontRequireReceiver);
	}

	// Use this for initialization
	void Start () {
		explosionObject = (GameObject)Instantiate(Resources.Load("Prefabs/Particles/Explosion"));
	}
	
	// Update is called once per frame
	void Update () {
		m_lifeSpan -= Time.deltaTime;
		if (m_lifeSpan <= 0)
			Die ();
		transform.position += transform.forward * Time.deltaTime * m_speed;
	}
}