using UnityEngine;
using System.Collections;

public class ObsticleRotation : MonoBehaviour
{
	private Vector3 rotation = new Vector3 (5, 0, 20);

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (rotation * Time.deltaTime);
	}
}

