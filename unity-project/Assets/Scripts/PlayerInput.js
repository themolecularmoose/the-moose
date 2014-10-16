#pragma strict

//public variables for how fast the ship moves forward/backward and rotates
public var speed : int;
public var rotSpeed : int;

//movement is not currently final
function FixedUpdate()
{
	//get value for when the user presses a horizontal key
	var h : float = Input.GetAxis("Horizontal");
	
	//create a vector3 value for when our ship rotates
	var rotate : Vector3 = new Vector3(0.0f, h, 0.0f);
	//this is the current rotation of the ship
	var rot : Vector3 = transform.rotation.eulerAngles;
	
	
	//Time.deltaTime makes sure our speed is consistant by making the object move
	//in relation to time and not by frame
	
	//if the forward button is pressed, move forward at the desired speed.
	if(Input.GetButton("Forward"))
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
	//back button pressed, move backward at the desired speed.
	if(Input.GetButton("Back"))
	{
		transform.position -= transform.forward * speed * Time.deltaTime;
	}
	
	//Update the ships rotation 
	transform.rotation.eulerAngles = rot + rotate * rotSpeed * Time.deltaTime;
}