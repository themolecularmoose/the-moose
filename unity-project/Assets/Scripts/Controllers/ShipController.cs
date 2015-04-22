using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	public float m_motionScale = 5;
	public float m_thrustStrength = 10;
	public float m_strafeStrength = 0;
	public float m_leanStrength = 10;
	public float m_tiltStrength = 1;
	public float m_brakeStrength = 10;
	public float m_riseStrength = 10;
	public float m_fallStrength = 10;
	public float m_boostStrength = 100;
	public float m_leanMax = 20;
	Vector2 m_mouseCurrent, m_mousePrevious, m_mouseDifference;
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
		//updateMouse ();
		if (m_shipBhv.enabled) {
			//rotateShip ();
			moveShip ();
			if (Input.GetKeyDown (KeyCode.Space)) {
				m_shipBhv.JumpDrive (m_boostStrength);
			}
			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.F))
				m_shipBhv.FireBuster ();
			m_shipBhv.beamState (Input.GetButton ("Tractor Beam"));
		}
	}

	void lockMouse()
	{
		Screen.lockCursor = true;
	}

	void moveShip()
	{
		if (Input.GetButton ("Forward")) {
			m_shipBhv.Thrust(m_thrustStrength * m_motionScale);
		}
		if (Input.GetButton ("Back")) {
			m_shipBhv.Thrust(-m_thrustStrength * m_motionScale);
		}
		if (Input.GetButton ("Left")) {
			m_shipBhv.Strafe(-m_thrustStrength * m_motionScale);
		}
		if (Input.GetButton ("Right")) {
			m_shipBhv.Strafe(m_thrustStrength * m_motionScale);
		}
		if (Input.GetButton ("Rise")) {
			m_shipBhv.Climb(m_thrustStrength * m_motionScale);
		}
		if (Input.GetButton ("Fall")) {
			m_shipBhv.Climb(-m_thrustStrength * m_motionScale);
		}
	}

	void setupMouse()
	{
		Cursor.visible = false;
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
	}

	void updateMouse()
	{
		m_mousePrevious = m_mouseCurrent;
		m_mouseCurrent = Input.mousePosition;
		
		if (!Screen.lockCursor) {
			m_mouseDifference = m_mouseCurrent - m_mousePrevious;
		}
		//gotcha ;)
		toggleMouse ();
	}

	void rotateShip()
	{
		float max = 100;
		float shrink = 5;
		float turnSide = Mathf.Clamp(m_mouseDifference.x / shrink, -max, max);
		float turnVertical = Mathf.Clamp(-m_mouseDifference.y / shrink, -max, max);
		transform.Rotate(turnVertical, 0, 0, Space.Self);
		transform.Rotate(0, turnSide, 0, Space.World);
	}
}
