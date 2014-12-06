#pragma strict

public var gameController: GameController;

function Start()
{
	rigidbody.freezeRotation = true;
	var gameControllerObject : GameObject = GameObject.FindWithTag("GameController");
	if(gameControllerObject != null){
		gameController = gameControllerObject.GetComponent(GameController);
	}
	if(gameControllerObject == null){
		Debug.Log("Can't find 'GameController' script!");
	}
		
}

function Update () {

}

function FixedUpdate () {
	if(Input.GetButton("Tractor Beam"))
	{
		gameController.beamState(true);
	}
	else
	{
		gameController.beamState(false);
	}
}

function HitCollectable(){
	audio.Play();
 	Debug.Log("Hit collectable");
 }
 
function HitFluid(){
 	Debug.Log("Hit fluid");
}

function HitObject()
{
	Debug.Log("Hit base!");
	WaitForSeconds(1);
	Debug.Log("Good to go");
}
