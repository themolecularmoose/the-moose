using UnityEngine;
using System.Collections;

public class MooseCtrl : MonoBehaviour {
	MooseBhv m_moose;

	void Start()
	{
		m_moose = GetComponent<MooseBhv>();
	}

	void Update()
	{
		m_moose.Input(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift),
		              Input.GetKey(KeyCode.A),
		              Input.GetKey(KeyCode.D),
		              Input.GetKey(KeyCode.W),
		              Input.GetKey(KeyCode.S),
		              Input.GetKeyDown(KeyCode.Space),
		              Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
	}
}
