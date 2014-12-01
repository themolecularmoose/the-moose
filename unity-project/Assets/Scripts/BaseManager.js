#pragma strict

private var gameController : GameController;
private var count : int;
private var items : int;
private var levelDone : boolean;


function Start () {
	items = 0;
	levelDone = false;
	var gameControllerObject : GameObject = GameObject.FindWithTag("GameController");
	if(gameControllerObject != null){
		gameController = gameControllerObject.GetComponent(GameController);
	}
	
	if(gameControllerObject == null){
		Debug.Log("Can't fint 'GameController' script!");
	}
	var collectablesObject : GameObject = GameObject.FindWithTag("Collectables");
	
	if(collectablesObject != null)
	{
		count = collectablesObject.transform.childCount;
	}
}



function OnTriggerEnter(collision:Collider){
	if(collision.gameObject.tag == "Player"){
		items = count - gameController.GetCollected();
		Debug.Log(items);
		if(items == 0)
		{
			gameController.ChangeWinState();
			gameController.EndLevel();
		}
    }
}

function OnCollisionEnter(collision:Collision)
{
	Debug.Log("Ouch!");
	collision.gameObject.SendMessage("HitObject");
}