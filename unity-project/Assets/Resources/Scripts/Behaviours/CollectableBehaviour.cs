using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			gameObject.SendMessageUpwards("OnCollect", this.gameObject);
			gameObject.SetActive(false);
		}
	}
}
