#pragma strict

var xRotation = 20;
var yRotation = 0;
var zRotation = 50;


function Start () {

}

function Update () {
	transform.Rotate (xRotation*Time.deltaTime,yRotation*Time.deltaTime,zRotation*Time.deltaTime);
}