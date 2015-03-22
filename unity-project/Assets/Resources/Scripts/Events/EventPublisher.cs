using UnityEngine;
using System.Collections;

public class EventPublisher : MonoBehaviour {


	// Update is called once per frame
	void Update () {
	
	}

	// TODO: make even stack if reflection performance becomes an issue
	public void publish(GameEvent gameEvent) {
		//send message to children of this gameObject (i.e. Level prefab)
		BroadcastMessage(gameEvent.name, gameEvent, SendMessageOptions.DontRequireReceiver);
	}
}
