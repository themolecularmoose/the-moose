using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour
{
	public GameController gameController ;
	private GameObject gameControllerObject;

	// Use this for initialization
	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player" && gameController.isBeamOn()){
			Debug.Log("Hit by player");
			gameController.CollectCollectable(this.gameObject);
			//Destroy(this.gameObject);
			this.gameObject.SetActive(false);
			gameController.AddScore(100);
			gameController.CollectedIncrease(this.gameObject.tag);

			//Destroy(this);
		}
	}
}

