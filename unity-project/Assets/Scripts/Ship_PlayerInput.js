#pragma strict

//public variables for how fast the ship moves forward/backward and rotates
var speed_max : float = 150.0; //max speed
var speed_min : float = 5.0; //min speed 
var speed_accel : float = 0.15; //acceleration on forward
var speed_brake : float = 0.05; //deceleration on back
var speed_decel : float = 0.002; //passive deceleration


var strafe_max : float = 30; //strafe max speed
var strafe_accel : float = 0.05; //strafe acceleration
var strafe_decel : float = 0.05; //strafe passive decelleration
var speed_scale : float = 0.1; //general speed scalar so numbers are reasonable

var rotate_speed : int = 120; //base turn sensitivity
// min rotate = rotate_speed - rotate_dim * speed_min / speed_max
// max rotate = rotate_speed - rotate_dim
var rotate_dim : float = 0.3; 

var horiz_sens : float = 0.75; //left right sensitivity
var vert_sens : float = 1.0; //up down sensitivity
var spin_sens : float = 0.75; //clockwise/counterclockwise rotation that is mixed with left right
var horiz_invert : int = 1; //negative or positive sensitivy inversion 
var vert_invert : int = -1; //negative or positive sensitivy inversion
var spin_invert : int = 1; //negative or positive sensitivy inversion

private var speed : float = speed_min;
private var strafe : float = 0;


//movement is not currently final
function FixedUpdate()
{		
	//if the forward button is pressed, move forward at the desired speed.
	if(Input.GetButton("Forward"))
	{
		speed += (speed_max - speed) * speed_accel;
	}
	//back button pressed, move backward at the desired speed.
	if(Input.GetButton("Back"))
	{	
		speed -= (speed - speed_min) * speed_brake;
	}
	
	if(!Input.GetButton("Forward") && !Input.GetButton("Back")){
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
	
	
	//get mouse input
	var x : float = Input.GetAxis("Mouse X");
	var y : float = Input.GetAxis("Mouse Y");
	
	//create a vector3 value for when our ship rotates
	var new_rotate : Vector3 = new Vector3( vert_invert * vert_sens * y, horiz_invert * horiz_sens * x, spin_invert * spin_sens * x);
	
	//postion to rotate to * ( calculated speed to rotate at ) * ( scaled by fixed delta time )
	transform.Rotate(new_rotate * (rotate_speed - (rotate_dim * speed / speed_max)) * Time.fixedDeltaTime); 

}

function HitCollectable(){
	Debug.Log("Hit collectable");
}

function HitFluid(){
	speed = .1;
	Debug.Log("Hit fluid");
}
