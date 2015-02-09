using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour
{

	private GameController gameController;
	private int count;
	private int items;

	// Use this for initialization
	void Start ()
	{
		items = 0;
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		GameObject collectablesObject = GameObject.FindWithTag ("Collectables");
		if(collectablesObject != null){
			count = collectablesObject.transform.childCount;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.tag == "Player"){
			gameController.RefillEnergy();
			items = count - gameController.GetCollected();
			if(items == 0)
			{
				gameController.ChangeWinState();
				gameController.EndLevel();
			}
		}
	}
	void OnCollisionEnter(Collision collision){
		collision.gameObject.SendMessage("HitObject");
	}
}

