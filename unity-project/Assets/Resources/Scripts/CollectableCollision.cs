using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player"){
			/*gameController.CollectCollectable(this.gameObject);
			//Destroy(this.gameObject);
			this.gameObject.SetActive(false);
			gameController.AddScore(100);
			gameController.CollectedIncrease(this.gameObject.tag);*/

			//Destroy(this);
		}
	}
}

