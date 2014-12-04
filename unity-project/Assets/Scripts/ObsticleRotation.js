#pragma strict

var xRotation = 5;
var yRotation = 0;
var zRotation = 20;

function Start () {

}

function Update () {
	transform.Rotate (xRotation*Time.deltaTime,yRotation*Time.deltaTime,zRotation*Time.deltaTime);
}