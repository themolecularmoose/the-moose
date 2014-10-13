#pragma strict

public var speed : int;

function FixedUpdate()
{
	var h : float = Input.GetAxis("Horizontal");
	var v : float = Input.GetAxis("Vertical");
	
	var movement : Vector3 = new Vector3 (v, 0.0f, -h);
	var pos : Vector3 = transform.position; 
	
	pos = pos + movement * speed * Time.deltaTime;
	
	transform.position = pos;
}