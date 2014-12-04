#pragma strict

//public variables for how fast the ship moves forward/backward and rotates
public var speed : int;
public var rotSpeed : int;
public var gameController: GameController;
public var moveBack : boolean;

function Start()
{
	moveBack = false;
	rigidbody.freezeRotation = true;
	var gameControllerObject : GameObject = GameObject.FindWithTag("GameController");
	if(gameControllerObject != null){
		gameController = gameControllerObject.GetComponent(GameController);
	}
	if(gameControllerObject == null){
		Debug.Log("Can't find 'GameController' script!");
	}
		
}

//movement is not currently final
function FixedUpdate()
{
	//get value for when the user presses a horizontal key
	var x : float = Input.GetAxis("Mouse X");
	var y : float = Input.GetAxis("Mouse Y")*(-0.5);
	
	//create a vector3 value for when our ship rotates
	var rotate : Vector3 = new Vector3(y, x, 0.0f);
	
	//this is the current rotation of the ship
	var rot : Vector3 = transform.rotation.eulerAngles;
	
	//Time.deltaTime makes sure our speed is consistant by making the object move
	//in relation to time and not by frame
	
	//Keeps the player moving in a forward direction
	transform.position += transform.forward * speed * Time.deltaTime * .2;
	
	//if the forward button is pressed, move forward at the desired speed.
	if(Input.GetButton("Forward") && !moveBack)
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
	//back button pressed, move backward at the desired speed.
	if(Input.GetButton("Back") && !moveBack)
	{
		transform.position -= transform.forward * speed * Time.deltaTime;
	}
	
	if(moveBack)
	{
		transform.position -= transform.forward * speed * Time.deltaTime;
	}
	
	//Update the ships rotation 
	transform.rotation.eulerAngles = rot + rotate * rotSpeed * Time.deltaTime;
	
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
	Debug.Log("Hit collectable");
}

function HitFluid(){
	speed = .1;
	Debug.Log("Hit fluid");
}