#pragma strict

//public variables for how fast the ship moves forward/backward and rotates
var speed_max : float = 100.0;
var speed_min : float = 5.0;
var speed_accel : float = 0.15;
var speed_decel : float = 0.05;

var strafe_max : float = 30;
var strafe_accel : float = 0.05;
var strafe_decel : float = 0.05;
var speed_scale : float = 0.1;

var rotate_speed : int = 200;
var rotate_dim : float = 0.3;

var horiz_sens : float = 0.75;
var horiz_invert : int = 1;
var vert_sens : float = 1.0;
var vert_invert : int = -1;

private var speed : float = speed_min;
private var strafe : float = 0;


//movement is not currently final
function FixedUpdate()
{
	//get value for when the user presses a horizontal key
	var x : float = Input.GetAxis("Mouse X")*(horiz_sens * horiz_invert);
	var y : float = Input.GetAxis("Mouse Y")*(vert_sens * vert_invert);
		
	//if the forward button is pressed, move forward at the desired speed.
	if(Input.GetButton("Forward"))
	{
		speed += (speed_max - speed) * speed_accel;
	}
	//back button pressed, move backward at the desired speed.
	if(Input.GetButton("Back"))
	{	
		speed -= (speed - speed_min) * speed_decel;
	}
	transform.position += transform.forward * speed * speed_scale * Time.fixedDeltaTime;

	if(Input.GetButton("Right"))
	{
		strafe += (strafe_max - strafe) * strafe_accel;
	}
	if(Input.GetButton("Left"))
	{
		strafe += (-strafe - strafe_max) * strafe_accel;
	}
	if(!Input.GetButton("Right") && !Input.GetButton("Left"))
	{
		strafe -= strafe * strafe_decel;		
	}
	transform.position += transform.right * strafe * speed_scale * Time.fixedDeltaTime;
	
	
	//create a vector3 value for when our ship rotates
	var new_rotate : Vector3 = new Vector3( y, x, x);
	
	transform.Rotate(new_rotate * (rotate_speed - (rotate_dim * speed / speed_max)) * Time.fixedDeltaTime); 

}

function HitCollectable(){
	Debug.Log("Hit collectable");
}

function HitFluid(){
	speed = .1;
	Debug.Log("Hit fluid");
}
