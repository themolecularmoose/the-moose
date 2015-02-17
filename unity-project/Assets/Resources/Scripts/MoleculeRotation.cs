using UnityEngine;
using System.Collections;

public class MoleculeRotation : MonoBehaviour
{
	private Vector3 rotation = new Vector3 (20, 0, 50);
	// Update is called once per frame
	void Update (){
		transform.Rotate (rotation * Time.deltaTime);
	}
}

