using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	private int win = 0;
	private int score = 0;
	private string level = "";
	private LevelLoader loader;

	// Use this for initialization
	void Start (){
		loader = GameObject.Find ("Utilities").GetComponent<LevelLoader> ();
		//get our score from playerprefs
		level = PlayerPrefs.GetString ("Level");
		score = PlayerPrefs.GetInt("Score");
		win = PlayerPrefs.GetInt("Win");
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}

	void OnGUI () {
		// Make a background box
		Rect rect = new Rect(0, 0, Screen.width, Screen.height);
		GUI.Box (rect, "");
		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = 48;
		centeredStyle.normal.textColor = Color.yellow;
		
		// centered and at top of screen
		GUI.Label (new Rect (rect.center[0]-100, rect.center[1]-200, 200, 100), "Final Score");
		GUI.Label (new Rect (rect.center[0]-100, rect.center[1]-80, 200, 100), score.ToString());

		string victory = "";
		if(win == 1) {
			victory = "Winner!";
		} else {
			victory = "Loser";
		}
		GUI.Label (new Rect (rect.center[0]-100, rect.center[1]+100, 200, 100), victory);
		if (GUI.Button (new Rect (rect.center[0]-40,rect.center[1],80,20), "Retry")) {
			loader.LoadLevel (level);
		}
		
		// Make the second button.
		if (GUI.Button (new Rect (rect.center[0] -40,rect.center[1] + 40,80,20), "Quit")) {
			loader.LoadLevel("start_menu");
		} 
	}
}

