using UnityEngine;
using System.Collections;

public class BoundaryCollision : MonoBehaviour
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
		if(collision.gameObject.tag == "Player"){
			Debug.Log("Collision detected");
			gameController.RespawnPlayer(collision.gameObject);
			//Destroy(this);
		}
	}
}

