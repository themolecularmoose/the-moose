using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	public float m_motionScale = 5;
	public float m_thrustStrength = 10;
	public float m_strafeStrength = 10;
	public float m_brakeStrength = 10;
	public float m_riseStrength = 10;
	public float m_fallStrength = 10;
	public float m_boostStrength = 100;
	ShipBehaviour m_shipBhv;
	Transform m_cameraTransform;

	void checkCenterMouse()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			toggleMouse();
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		checkCenterMouse();
		pollInputClusterBuster();
		pollInputTractorBeam();
		pollInputFlying();
	}

	void lockMouse()
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

	void pollInputClusterBuster()
	{
		if(Input.GetMouseButtonDown(0))
		{
			m_shipBhv.FireBuster();
		}
	}

	void pollInputFlying()
	{
		Vector3 force = Vector3.zero;
		//base movement off of the model transform
		Transform model = m_cameraTransform;
		
		if(Input.GetKey(KeyCode.W))
		{
			force += model.forward * m_thrustStrength;
		}
		if(Input.GetKey(KeyCode.S))
		{
			force -= model.forward * m_brakeStrength;
		}
		if(Input.GetKey(KeyCode.D))
		{
			force += model.right * m_strafeStrength;
		}
		if(Input.GetKey(KeyCode.A))
		{
			force -= model.right * m_strafeStrength;
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
			rigidbody.velocity += model.forward * m_boostStrength;
		}
	}
	
	void pollInputTractorBeam()
	{
		if(Input.GetButton("Tractor Beam"))
		{
			m_shipBhv.beamState(true);
		}
		else
		{
			m_shipBhv.beamState(false);
		}
	}

	void setupMouse()
	{
		lockMouse();
		unlockMouse();
	}
	
	// Use this for initialization
	void Start () {
		m_shipBhv = gameObject.GetComponent<ShipBehaviour>();
		m_cameraTransform = transform.Find("Attachments");
		setupMouse();
		lockMouse();
	}
	
	void toggleMouse()
	{
		if(Screen.lockCursor)
		{
			unlockMouse();
		}else{
			lockMouse();
		}
	}

	void unlockMouse()
	{
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}
}
