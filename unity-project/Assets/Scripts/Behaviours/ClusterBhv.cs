using UnityEngine;
using System.Collections;

public class ClusterBhv : MonoBehaviour {
	GameObject m_level;
	bool m_broken;
	public bool m_respawn;
	GameObject[] m_children;

	// Use this for initialization
	void Start () {
		m_level = GameObject.Find("Level");
		Transform child;
		//disable all colectable scripts
		m_children = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; ++i)
		{
			child = transform.GetChild(i);
			m_children[i] = child.gameObject;
			//If the collectable has a rigidBody, disable it
			if(child.GetComponent<Rigidbody>() != null)
				child.GetComponent<Rigidbody>().isKinematic = true;
			//disable scripts
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
			//enable scripts
			MonoBehaviour[] scripts = child.GetComponents<MonoBehaviour>();
			for(int s = 0; s <scripts.Length; ++s)
			{
				scripts[s].enabled = true;
			}
			//break child off
			child.SetParent(m_level.transform);
			i--;
			//add explosive force
			if(child.GetComponent<Rigidbody>() != null)
			{
				child.GetComponent<Rigidbody>().isKinematic = false;
				//child.rigidbody.constraints = RigidbodyConstraints.None;
				child.GetComponent<Rigidbody>().AddExplosionForce(a_power, a_center, 0, a_lift);
			}
		}
		if (m_respawn) {
			Invoke("respawn", 4);
		}

	}

	void onBusterHit(object a_sender)
	{
		if(!m_broken)
		{
			BusterBhv buster = (BusterBhv)a_sender;
			Explode(buster.m_power, buster.m_lift, buster.gameObject.transform.position);
			buster.SendMessage("Die");
		}
	}

	void respawn()
	{
		for (int i = 0; i < m_children.Length; ++i) {
			m_children[i].transform.SetParent(gameObject.transform);
			if(!m_children[i].activeSelf)
			{
				//set to active to recieve messages
				m_children[i].SetActive(true);
				m_children[i].SendMessageUpwards("Reactivate");
			}
		}
		m_broken = false;
		Start ();
	}
}
