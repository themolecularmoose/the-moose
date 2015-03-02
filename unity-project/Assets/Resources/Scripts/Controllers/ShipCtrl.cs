using UnityEngine;
using System.Collections;

public class ShipCtrl : MonoBehaviour {
	public float m_motionScale = 5;
	public float m_thrustStrength = 10;
	public float m_strafeStrength = 10;
	public float m_brakeStrength = 10;
	public float m_riseStrength = 10;
	public float m_fallStrength = 10;
	public float m_boostStrength = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 force = Vector3.zero;

		if(Input.GetKey(KeyCode.W))
		{
			force += transform.forward * m_thrustStrength;
		}
		if(Input.GetKey(KeyCode.S))
		{
			force -= transform.forward * m_brakeStrength;
		}
		if(Input.GetKey(KeyCode.D))
		{
			force += transform.right * m_strafeStrength;
		}
		if(Input.GetKey(KeyCode.A))
		{
			force -= transform.right * m_strafeStrength;
		}
		if (Input.GetKey (KeyCode.E)) {
			force += transform.up * m_riseStrength;
		}
		if (Input.GetKey (KeyCode.Q)) {
			force -= transform.up * m_fallStrength;
		}

		force *= m_motionScale;

		rigidbody.AddForce(force);
		rigidbody.velocity *= 0.95f;

		if (Input.GetKeyDown (KeyCode.Space)) {
			rigidbody.velocity += transform.forward * m_boostStrength;
		}
	}
}
