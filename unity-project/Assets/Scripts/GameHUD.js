#pragma strict

var hudFont : Font;
var gameController : GameController;


function Start () {
}

function Update () {
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
}

function getTimeRemainingStr( totalSeconds : int ){
	var minutes : int = totalSeconds/60.0;
	var seconds : int = totalSeconds - 60 * minutes;
	return minutes + ":" + seconds.ToString("00"); 
}

function getCollectProgressStr( collectRemaining : int, totalCollects : int ){
	return collectRemaining.ToString("00") + "/" + totalCollects;
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
	
	GUI.Label(Rect (50, Screen.height - 200, 200, 200), water, bottomLeftStyle);
}

function DrawMethane( methane : String ){
	var bottomLeftStyle = GUI.skin.GetStyle("Label");
	bottomLeftStyle.alignment = TextAnchor.LowerLeft;
	bottomLeftStyle.fontSize = 32;
	bottomLeftStyle.font = hudFont;
	bottomLeftStyle.normal.textColor = Color.yellow;
	
	GUI.Label(Rect (50, Screen.height - 250, 200, 200), methane, bottomLeftStyle);
}