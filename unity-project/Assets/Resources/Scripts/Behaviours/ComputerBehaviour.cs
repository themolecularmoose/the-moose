using UnityEngine;
using System.Collections;

public class ComputerBehaviour : MonoBehaviour {

	bool showLevelMessage = false;

	// Use this for initialization
	void Start () {
	}
	
	void Update(){
		if (showLevelMessage) {
			if (Input.GetKeyDown ("space")) {
					Application.LoadLevel ("main_game");
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			showLevelMessage = true;
		}
	}

	void OnCollisionExit(Collision collision){
		showLevelMessage = false;
	}


	void OnGUI (){
		if (showLevelMessage) {
			DrawLevelMessage();		
		}
	}

	void DrawLevelMessage(){
		string message = "Welcome to your training mission. Press SPACE to start Level 1";		

		GUIStyle levelMessageStyle = GUI.skin.GetStyle("Box");
		levelMessageStyle.wordWrap = true; 
		levelMessageStyle.alignment = TextAnchor.MiddleCenter;
		levelMessageStyle.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		levelMessageStyle.fontSize = 30;

		levelMessageStyle.normal.textColor = Color.blue;
		GUI.Label(new Rect (Screen.width/2, Screen.height/2, 200,200), message, levelMessageStyle);
	}
}
