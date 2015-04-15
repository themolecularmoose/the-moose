using UnityEngine;
using System.Collections;

public class KillSwitchBehaviour : MonoBehaviour {
	bool m_exploded;
	public GameObject m_shipAvatar;

	// Use this for initialization
	void Start () {
		//check for nulls
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_exploded && Input.GetKeyDown (KeyCode.T)) {
			//disable avatar
			m_shipAvatar.SetActive (false);
			//play explosion
			particleSystem.Play ();
			m_exploded = true;
			Invoke("Respawn", 2.0f);
		}
	}

	void OnDeath(GameEvent a_event)
	{
		DeathEvent dth = a_event as DeathEvent;
		if (dth != null) {
			particleSystem.Play();
		}
	}

	void Respawn()
	{
		m_shipAvatar.SetActive (true);
		m_exploded = false;
	}
}
