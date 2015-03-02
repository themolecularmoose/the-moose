using UnityEngine;
using System.Collections;

public class MooseBhv : MonoBehaviour {
	public float m_walkSpeed;
	public float m_runSpeed;
	public float m_jumpHeight;
	public float m_strafeMultiplier;
	public float m_playerHeightTEMP;
	
	public void Input(bool a_run, bool a_left, bool a_right, bool a_forward, bool a_back, bool a_jump, bool a_crouch)
	{
		Vector3 movement = Vector3.zero;
		float speed = m_walkSpeed;
		//only run if moving forward
		if(a_run && a_forward && !a_back)
			speed = m_runSpeed;
		if(a_right)
		{
			movement += transform.right * speed * m_strafeMultiplier;
		}
		if(a_left)
		{
			movement -= transform.right * speed * m_strafeMultiplier;
		}
		if(a_forward)
		{
			movement += transform.forward * speed;
		}
		if(a_back)
		{
			movement -= transform.forward * speed;
		}
		move (movement);
		
		if(a_jump)
		{
			jump ();
		}
	}
	void jump()
	{
		//pseudo "on the groun" detection
		Ray footRay = new Ray(transform.position, Vector3.down);
		float distance = m_playerHeightTEMP + 0.1f;
		RaycastHit result;
		Debug.DrawLine(footRay.origin, footRay.origin + footRay.direction * distance, Color.red, 5);
		if(Physics.Raycast(footRay, out result, distance))
		{
			Vector3 jump = new Vector3(0, m_jumpHeight, 0);
			rigidbody.AddForce(jump, ForceMode.Impulse);
		}
	}
	void move(Vector3 a_impulse)
	{
		rigidbody.AddForce(a_impulse, ForceMode.Acceleration);
	}
}
