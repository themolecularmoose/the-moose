using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {

	//need to have Start() so we can disable this.
	void Start()
	{
	}

	void OnCollisionEnter(Collision collision) {
		//we HAVE to check if enabled, I think enabled controls updating alone
		if(enabled && collision.gameObject.tag == "Player") {
			gameObject.SendMessageUpwards("OnCollect", this.gameObject);
			gameObject.SetActive(false);
		}
	}
}
