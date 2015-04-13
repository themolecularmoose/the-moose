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

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Got a collision here!");
		ContactPoint c = collision.contacts [0];
		m_spark.transform.position = c.point;
		//m_spark.transform.forward = c.normal;
		m_spark.particleSystem.Play ();
	}
}
