using UnityEngine;
using System.Collections;

public class ClusterBhv : MonoBehaviour {
	GameObject m_level;
	bool m_broken;

	// Use this for initialization
	void Start () {
		m_level = GameObject.Find("Level");
		Transform child;
		//disable all colectable scripts
		for(int i = 0; i < transform.childCount; ++i)
		{
			child = transform.GetChild(i);
			child.rigidbody.isKinematic = true;
			MonoBehaviour[] scripts = child.GetComponents<MonoBehaviour>();
			for(int s = 0; s <scripts.Length; ++s)
			{
				scripts[s].enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void Explode(float a_power, float a_lift){
		Explode(a_power, a_lift, transform.position);
	}
	void Explode(float a_power, float a_lift, Vector3 a_center){
		m_broken = true;
		Transform child;
		//disable all colectable scripts
		for(int i = 0; i < transform.childCount; ++i)
		{
			child = transform.GetChild(i);
			MonoBehaviour[] scripts = child.GetComponents<MonoBehaviour>();
			//enable scripts
			for(int s = 0; s <scripts.Length; ++s)
			{
				scripts[s].enabled = true;
			}
			//break child off
			child.SetParent(m_level.transform);
			i--;
			//add explosive force
			if(child.rigidbody != null)
			{
				child.rigidbody.isKinematic = false;
				child.rigidbody.constraints = RigidbodyConstraints.None;
				child.rigidbody.AddExplosionForce(a_power, a_center, 0, a_lift);
			}
		}
	}

	void onBusterHit(object a_sender)
	{
		if(!m_broken)
		{
			BusterBhv buster = (BusterBhv)a_sender;
			Explode(buster.m_power, buster.m_lift, buster.gameObject.transform.position);
			Destroy(buster.gameObject);
		}
	}
}
