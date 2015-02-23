using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			Debug.Log("Hit Collectable");
			gameObject.SendMessageUpwards("OnCollect", this.gameObject);
			gameObject.SetActive(false);
		}
	}
}
