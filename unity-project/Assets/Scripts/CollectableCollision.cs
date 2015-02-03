using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour
{
	private GameController gameController ;
	private GameObject gameControllerObject;

	// Use this for initialization
	void Start ()
	{
		gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			Debug.Log("Game controller created");
			gameController = gameControllerObject.GetComponent("GameController") as GameController;
		}else{
			Debug.Log("Can't find GameController script!");
		}
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player" && gameController.isBeamOn()){
			Debug.Log("Hit by player");
			Destroy(this.gameObject);
			
			gameController.AddScore(100);
			gameController.CollectedIncrease(this.gameObject.tag);
			//Destroy(this);
			collision.gameObject.SendMessage("HitCollectable");
		}
	}
}

