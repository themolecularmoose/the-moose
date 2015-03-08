using UnityEngine;
using System.Collections;

public class EventPublisher : MonoBehaviour {


	// Update is called once per frame
	void Update () {
	
	}

	// TODO: make even stack if reflection performance becomes an issue
	public void publish(GameEvent gameEvent) {
		BroadcastMessage(gameEvent.name, gameEvent, SendMessageOptions.DontRequireReceiver);
	}
}
