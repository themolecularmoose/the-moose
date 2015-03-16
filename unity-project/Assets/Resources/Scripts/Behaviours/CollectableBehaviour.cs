using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {

	private EventPublisher eventPublisher;

	//need to have Start() so we can disable this.
	void Start()
	{
		eventPublisher = GameObject.Find("Level").GetComponent<EventPublisher>();
	}

	void OnCollisionEnter(Collision collision) {
		//we HAVE to check if enabled, I think enabled controls updating alone
		if(enabled && collision.gameObject.tag == "Player") {
			eventPublisher.publish ( new CollectableEvent(gameObject));
			gameObject.SetActive(false);
		}
	}
}
