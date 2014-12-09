#pragma strict

var hudFont : Font;
var helpFont : Font;
var gameController : GameController;
var showHelp = true;
public var waterTex : Texture;
public var MethaneTex : Texture;

function Start () {
	Invoke("HideHelp",5);
}

function Update () {
	if(Input.GetKeyDown("h")){
		if(!showHelp){
			showHelp = true;
			Invoke("HideHelp",5);
		}
	}
}

function OnGUI () {
  	DrawTimer(getTimeRemainingStr(gameController.GetTime()));
  	DrawCollectablesCounter(
  		getCollectProgressStr(gameController.GetCollected(),gameController.GetCount()));
  	DrawScore( gameController.GetScore().ToString() );

  	if(gameController.GetWater() != 0)
  		DrawWater( "x " + gameController.GetWater().ToString() );
  		
  	if(gameController.GetMethane() != 0)
  		DrawMethane("x " + gameController.GetMethane().ToString() );

 	if(showHelp){
 		DrawHelpMessage();
 	}
}

function getTimeRemainingStr( totalSeconds : int ){
	var minutes : int = totalSeconds/60.0;
	var seconds : int = totalSeconds - 60 * minutes;
	return minutes + ":" + seconds.ToString("00"); 
}

function getCollectProgressStr( collectRemaining : int, totalCollects : int ){
	return collectRemaining.ToString("00") + "/" + totalCollects;
}

function HideHelp(){
	showHelp = false;	
}

function DrawHelpMessage(){
	var message = "MISSION:\nNavigate your ship in sub-atomic space to collect all the water and methane molecules before time runs out.";
	var helpMessageStyle = GUI.skin.GetStyle("Box");
	helpMessageStyle.wordWrap = true; 
	helpMessageStyle.alignment = TextAnchor.UpperLeft;
	helpMessageStyle.fontSize = 18;
	helpMessageStyle.font = helpFont;
	helpMessageStyle.normal.textColor = Color.yellow;
	GUI.Label(Rect (Screen.width - 250, Screen.height - 200, 200, 200), message, helpMessageStyle);
}

function DrawTimer( time : String ){
	var centeredTimerStyle = GUI.skin.GetStyle("Label");
    centeredTimerStyle.alignment = TextAnchor.UpperCenter;
    centeredTimerStyle.fontSize = 48;
    centeredTimerStyle.font = hudFont;
    centeredTimerStyle.normal.textColor = Color.yellow;
    // centered and at top of screen
    GUI.Label (Rect (Screen.width/2-100, 25, 200, 100), time, centeredTimerStyle);
}

function DrawCollectablesCounter( counter : String ){
	var topLeftStyle = GUI.skin.GetStyle("Label");
	topLeftStyle.alignment = TextAnchor.UpperLeft;
	topLeftStyle.fontSize = 48;
	topLeftStyle.font = hudFont;
	topLeftStyle.normal.textColor = Color.yellow;
	//top left
	GUI.Label (Rect (50, 25, 200, 100), counter, topLeftStyle);
}

function DrawScore( score : String ){
	var topRightStyle = GUI.skin.GetStyle("Label");
	topRightStyle.alignment = TextAnchor.UpperRight;
	topRightStyle.fontSize = 48;
	topRightStyle.font = hudFont;
	topRightStyle.normal.textColor = Color.yellow;
	//top right
	GUI.Label (Rect (Screen.width - 250, 25, 200, 100), score, topRightStyle);
}

function DrawWater( water : String ){
	var bottomLeftStyle = GUI.skin.GetStyle("Label");
	bottomLeftStyle.alignment = TextAnchor.LowerLeft;
	bottomLeftStyle.fontSize = 32;
	bottomLeftStyle.font = hudFont;
	bottomLeftStyle.normal.textColor = Color.yellow;
	
	GUI.DrawTexture(Rect(50,Screen.height - 70,60,60), waterTex, ScaleMode.ScaleToFit, true, 1.0f);
	GUI.Label(Rect (120, Screen.height - 200, 200, 200), water, bottomLeftStyle);
}

function DrawMethane( methane : String ){
	var bottomLeftStyle = GUI.skin.GetStyle("Label");
	bottomLeftStyle.alignment = TextAnchor.LowerLeft;
	bottomLeftStyle.fontSize = 32;
	bottomLeftStyle.font = hudFont;
	bottomLeftStyle.normal.textColor = Color.yellow;
	
	GUI.DrawTexture(Rect(50,Screen.height - 170,60,60), MethaneTex, ScaleMode.ScaleToFit, true, 1.0f);
	GUI.Label(Rect (120, Screen.height - 300, 200, 200), methane, bottomLeftStyle);
}
