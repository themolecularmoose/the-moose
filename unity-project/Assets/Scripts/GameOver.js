#pragma strict

var score = 0;
var level = '';
var win = 0;
function Start () {
	//get our score from playerprefs
	level = PlayerPrefs.GetString ("Level");
	score = PlayerPrefs.GetInt("Score");
	win = PlayerPrefs.GetInt("Win");
	Screen.showCursor = true;
	Screen.lockCursor = false;
}

function Update () {

}

function OnGUI () {
    // Make a background box
    var rect = Rect(0, 0, Screen.width, Screen.height);
    GUI.Box (rect, "");
    var centeredStyle = GUI.skin.GetStyle("Label");
	   centeredStyle.alignment = TextAnchor.UpperCenter;
	   centeredStyle.fontSize = 48;
	   centeredStyle.normal.textColor = Color.yellow;

   	// centered and at top of screen
   	GUI.Label (Rect (rect.center[0]-100, rect.center[1]-200, 200, 100), "Final Score");
   	GUI.Label (Rect (rect.center[0]-100, rect.center[1]-80, 200, 100), score.ToString());

	var victory = '';
	if(win) {
		victory = "Winner!";
	} else {
		victory = "Loser";
	}
	GUI.Label (Rect (rect.center[0]-100, rect.center[1]+100, 200, 100), victory);
    if (GUI.Button (Rect (rect.center[0]-40,rect.center[1],80,20), "Retry")) {
        Application.LoadLevel (level);
    }

    // Make the second button.
    if (GUI.Button (Rect (rect.center[0] -40,rect.center[1] + 40,80,20), "Quit")) {
        Application.LoadLevel('start_menu');
    } 
}