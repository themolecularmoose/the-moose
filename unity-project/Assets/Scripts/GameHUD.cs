using UnityEngine;
using System.Collections;

public class GameHUD : MonoBehaviour {

	public Font hudFont;
	public Font helpFont;
	public GameController gameController;
	public bool showHelp = true;
	public Texture waterTex;
	public Texture MethaneTex;
	
	void Start () {
		Invoke("HideHelp",5);
	}
	
	void Update () {
		if(Input.GetKeyDown("h")){
			if(!showHelp){
				showHelp = true;
				Invoke("HideHelp",5);
			}
		}
	}
	
	void OnGUI () {
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
	
	public string getTimeRemainingStr( int totalSeconds ){
		int minutes = totalSeconds/60;
		int seconds = totalSeconds - 60 * minutes;
		return minutes + ":" + seconds.ToString("00"); 
	}
	
	public string getCollectProgressStr( int collectRemaining, int totalCollects ){
		return collectRemaining.ToString("00") + "/" + totalCollects;
	}
	
	void HideHelp(){
		showHelp = false;	
	}
	
	void DrawHelpMessage(){
		string message = "MISSION:\nNavigate your ship in sub-atomic space to collect all the water and methane molecules before time runs out.";
		GUIStyle helpMessageStyle = GUI.skin.GetStyle("Box");
		helpMessageStyle.wordWrap = true; 
		helpMessageStyle.alignment = TextAnchor.UpperLeft;
		helpMessageStyle.fontSize = 18;
		helpMessageStyle.font = helpFont;
		helpMessageStyle.normal.textColor = Color.yellow;
		GUI.Label(new Rect (Screen.width - 250, Screen.height - 200, 200, 200), message, helpMessageStyle);
	}
	
	void DrawTimer( string time ){
		GUIStyle centeredTimerStyle = GUI.skin.GetStyle("Label");
		centeredTimerStyle.alignment = TextAnchor.UpperCenter;
		centeredTimerStyle.fontSize = 48;
		centeredTimerStyle.font = hudFont;
		centeredTimerStyle.normal.textColor = Color.yellow;
		// centered and at top of screen
		GUI.Label(new Rect (Screen.width/2-100, 25, 200, 100), time, centeredTimerStyle);
	}
	
	void DrawCollectablesCounter( string counter ){
		GUIStyle topLeftStyle = GUI.skin.GetStyle("Label");
		topLeftStyle.alignment = TextAnchor.UpperLeft;
		topLeftStyle.fontSize = 48;
		topLeftStyle.font = hudFont;
		topLeftStyle.normal.textColor = Color.yellow;
		//top left
		GUI.Label (new Rect (50, 25, 200, 100), counter, topLeftStyle);
	}
	
	void DrawScore( string score ){
		GUIStyle topRightStyle = GUI.skin.GetStyle("Label");
		topRightStyle.alignment = TextAnchor.UpperRight;
		topRightStyle.fontSize = 48;
		topRightStyle.font = hudFont;
		topRightStyle.normal.textColor = Color.yellow;
		//top right
		GUI.Label (new Rect (Screen.width - 250, 25, 200, 100), score, topRightStyle);
	}
	
	void DrawWater( string water ){
		GUIStyle bottomLeftStyle = GUI.skin.GetStyle("Label");
		bottomLeftStyle.alignment = TextAnchor.LowerLeft;
		bottomLeftStyle.fontSize = 32;
		bottomLeftStyle.font = hudFont;
		bottomLeftStyle.normal.textColor = Color.yellow;

		GUI.DrawTexture(new Rect(50,Screen.height - 70,60,60), waterTex, ScaleMode.ScaleToFit, true, 1.0f);
		GUI.Label(new Rect (120, Screen.height - 200, 200, 200), water, bottomLeftStyle);
	}
	
	void DrawMethane( string methane ){
		GUIStyle bottomLeftStyle = GUI.skin.GetStyle("Label");
		bottomLeftStyle.alignment = TextAnchor.LowerLeft;
		bottomLeftStyle.fontSize = 32;
		bottomLeftStyle.font = hudFont;
		bottomLeftStyle.normal.textColor = Color.yellow;
		
		GUI.DrawTexture(new Rect(50,Screen.height - 170,60,60), MethaneTex, ScaleMode.ScaleToFit, true, 1.0f);
		GUI.Label(new Rect (120, Screen.height - 300, 200, 200), methane, bottomLeftStyle);
	}
}