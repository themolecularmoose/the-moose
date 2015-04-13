using UnityEngine;
using System.Collections;

public class SparkBehaviour : MonoBehaviour {
	public GameObject m_spark;

	// Use this for initialization
	void Start () {
		if (m_spark == null) {
			throw new UnityException("In player, spark not set. Should be by default!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDamage(GameEvent a_event)
	{
		//attempt conversion to dmgEvent
		DamageEvent dmg = a_event as DamageEvent;
		if (dmg != null) {
			particleSystem.Play();
		}
	}
}
