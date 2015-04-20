using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {
	public bool m_respawn;
	private bool canBeam = true;
	private EventPublisher eventPublisher;

	//need to have Start() so we can disable this.
	void Start()
	{
		if (GameObject.Find ("Level") != null) {
			eventPublisher = GameObject.Find ("Level").GetComponent<EventPublisher> ();
		} else { 
			Debug.Log ("No level game object in scene: " + Application.loadedLevelName);
		}
	}

	void OnCollisionEnter(Collision collision) {
		//we HAVE to check if enabled, I think enabled controls updating alone
		if(enabled && collision.gameObject.tag == "Player") {
			eventPublisher.publish ( new CollectableEvent(gameObject));
			gameObject.SetActive(false);
			if(m_respawn)
				Invoke("Reactivate", 1);
		}
	}

	void Reactivate()
	{
		gameObject.SetActive(true);
		eventPublisher.publish ( new CollectableEvent(gameObject, true));
	}

	public bool getBeam() 
	{
		return canBeam;
	}
}
