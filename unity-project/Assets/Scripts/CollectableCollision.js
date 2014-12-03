#pragma strict

private var gameController : GameController;

function Start()
{
	var gameControllerObject : GameObject = GameObject.FindWithTag("GameController");
	if(gameControllerObject != null){
		gameController = gameControllerObject.GetComponent(GameController);
	}
	if(gameControllerObject == null){
		Debug.Log("Can't fint 'GameController' script!");
	}
		
}

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag == "Player" && gameController.isBeamOn()){
		Debug.Log("Hit by player");
    	Destroy(this.gameObject);
    	
    	gameController.AddScore(100);
    	gameController.CollectedIncrease(this.gameObject.tag);
    	//Destroy(this);
    	collision.gameObject.SendMessage("HitCollectable");
    }
    
}