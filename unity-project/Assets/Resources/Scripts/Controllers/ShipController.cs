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

	void checkCenterMouse()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			toggleMouse();
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetKey (KeyCode.W))
			m_shipBhv.Thrust (m_thrustStrength * m_motionScale);
		if (Input.GetKey (KeyCode.S))
			m_shipBhv.Thrust (-m_thrustStrength * m_motionScale);
		if (Input.GetKey (KeyCode.D))
			m_shipBhv.Strafe (m_strafeStrength * m_motionScale);
		if (Input.GetKey (KeyCode.A))
			m_shipBhv.Strafe (-m_strafeStrength * m_motionScale);
		if (Input.GetKey (KeyCode.E))
			m_shipBhv.Climb (m_riseStrength * m_motionScale);
		if (Input.GetKey (KeyCode.Q))
			m_shipBhv.Climb (-m_riseStrength * m_motionScale);
		if (Input.GetKeyDown (KeyCode.Space))
			m_shipBhv.JumpDrive (m_boostStrength);
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.F))
			m_shipBhv.FireBuster();
		m_shipBhv.beamState(Input.GetButton("Tractor Beam"));
	}

	void lockMouse()
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

	void setupMouse()
	{
		lockMouse();
		unlockMouse();
	}
	
	// Use this for initialization
	void Start () {
		m_shipBhv = gameObject.GetComponent<ShipBehaviour>();
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
