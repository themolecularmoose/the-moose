using UnityEngine;
using System.Collections;

public class CartoonBehaviour : MonoBehaviour {
	public float m_spinRate;
	public float m_bobRate;
	public float m_bobPeak;
	float m_lastBobHeight;
	private bool paused;

	void OnPause() {
		paused = !paused;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused) {
			float bobHeight = Mathf.Sin (Time.time) * m_bobPeak;
			float change = bobHeight - m_lastBobHeight;
			if (rigidbody == null) {
				transform.Rotate (0, m_spinRate, 0);
				transform.Translate (0, change, 0);
			}
			m_lastBobHeight = bobHeight;
		}
	}
}
