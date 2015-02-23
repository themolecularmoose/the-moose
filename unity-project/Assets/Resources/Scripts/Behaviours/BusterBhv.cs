using UnityEngine;
using System.Collections;

public class BusterBhv : MonoBehaviour {
	public float m_speed;
	public float m_power;
	public float m_lift;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * m_speed;
	}

	void OnTriggerEnter(Collider a_other)
	{
		a_other.gameObject.SendMessageUpwards("onBusterHit", this, SendMessageOptions.DontRequireReceiver);
	}
}