#pragma strict

private var gameController : GameController;
var collectSound : AudioClip;

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
	audio.PlayOneShot(collectSound);
	if(collision.gameObject.tag == "Player" && gameController.isBeamOn()){
		Debug.Log("Hit by player on collision enter");
    	Destroy(this.gameObject);
    	gameController.AddScore(100);
    	gameController.CollectedIncrease(this.gameObject.tag);
    	//Destroy(this);
    	collision.gameObject.SendMessage("HitCollectable");
    }
    
}