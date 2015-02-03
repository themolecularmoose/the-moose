using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour
{
	private GameController gameController ;
	GameControllerObject gameObject;

	// Use this for initialization
	void Start ()
	{
		gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent(GameController);
		}else{
			Debug.Log("Can't find GameController script!");
		}
	}
	
	// Update is called once per frame
	void Update ()
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

