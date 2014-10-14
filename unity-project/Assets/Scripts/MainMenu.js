#pragma strict

private var original_color : Color;
var highlight_color : Color;

function Start () {
}

function OnMouseEnter() {
	//this doesn't work in start or update for some reason
	original_color = guiText.material.color;
	//change text to different color
	guiText.material.color = highlight_color;
}

function OnMouseExit() {
	//change text back to original color
	if(original_color.a > 0){
		guiText.material.color = original_color;
	}
}

function OnMouseUp() {
	//switch to main_game scene
	Application.LoadLevel("main_game");
}


function Update () {
	//Exit game if escape is pressed in menu
	if(Input.GetKey(KeyCode.Escape)){
		Application.Quit();
	}
}