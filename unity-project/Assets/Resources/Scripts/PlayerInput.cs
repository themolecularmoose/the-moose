using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	
	//public floatiables for how fast the ship moves forward/backward and rotates
	static   float speed_max  = 150.0F; //max speed
	static   float speed_min  = 5.0F; //min speed 

	float speed_accel  = 0.15F; //acceleration on forward
	float speed_brake  = 0.05F; //deceleration on back
	float speed_decel  = 0.002F; //passive deceleration
	
	
	float strafe_max  = 30F; //strafe max speed
	float strafe_accel  = 0.05F; //strafe acceleration
	float strafe_decel  = 0.05F; //strafe passive decelleration
	float speed_scale  = 0.1F; //general speed scalar so numbers are reasonable
	
	int rotate_speed = 120; //base turn sensitivity
	// min rotate = rotate_speed - rotate_dim * speed_min / speed_max
	// max rotate = rotatea_speed - rotate_dim
	float rotate_dim  = 0.3F; 
	
	float horiz_sens  = 0.75F; //left right sensitivity
	float vert_sens  = 1.0F; //up down sensitivity
	float spin_sens  = 0.75F; //clockwise/counterclockwise rotation that is mixed with left right

	int horiz_invert = 1; //negative or positive sensitivy inversion 
	int vert_invert = -1; //negative or positive sensitivy inversion
	int spin_invert = 1; //negative or positive sensitivy inversion
	
	private   float speed  = speed_min;
	private   float strafe  = 0F;
	
	
	//movement is not currently final
	void FixedUpdate()
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
		float x  = Input.GetAxis("Mouse X");
		float y  = Input.GetAxis("Mouse Y");
		
		//create a vector3 value for when our ship rotates
		Vector3 new_rotate = new Vector3( vert_invert * vert_sens * y, horiz_invert * horiz_sens * x, spin_invert * spin_sens * x);
		
		//postion to rotate to * ( calculated speed to rotate at ) * ( scaled by fixed delta time )
		transform.Rotate(new_rotate * (rotate_speed - (rotate_dim * speed / speed_max)) * Time.fixedDeltaTime); 
		
	}
}

