using UnityEngine;
using System.Collections;

///Usage: For objects without a rigidBody, use Direct
///For objects with a rigidBody choose velocity.
public enum ModificationType
{
	DIRECT, VELOCITY
}
public class RotationBehaviour : MonoBehaviour {
	public ModificationType m_modificationType;
	public Vector3 m_rateOfSpin;
	public bool m_world;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 change = m_rateOfSpin * Time.deltaTime;
		Vector3 changeLocal = transform.right * change.x + transform.up * change.y + transform.forward * change.z;
		switch(m_modificationType)
		{
			case ModificationType.DIRECT:
				if(m_world)
				{
					transform.RotateAround(transform.position, Vector3.right, change.x);
					transform.RotateAround(transform.position, Vector3.up, change.y);
					transform.RotateAround(transform.position, Vector3.forward, change.z);
				}
				else
				{
					transform.Rotate(change);
				}
				break;
			case ModificationType.VELOCITY:
				if(m_world)
				{
					rigidbody.angularVelocity = change;
				}else
				{
					rigidbody.angularVelocity = changeLocal;
				}
				break;
		}
	}
}