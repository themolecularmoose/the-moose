using UnityEngine;
using System.Collections;

public class DialogueMarkerScript : MonoBehaviour {
	CartoonBehaviour m_behaviour;
	float m_spinRate;

	// Use this for initialization
	void Start () {
		m_behaviour = GetComponent<CartoonBehaviour>();
		m_spinRate = m_behaviour.m_spinRate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter()
	{
		m_behaviour.m_spinRate = m_spinRate * 6;
	}

	void OnTriggerExit()
	{
		m_behaviour.m_spinRate = m_spinRate;
	}
}
