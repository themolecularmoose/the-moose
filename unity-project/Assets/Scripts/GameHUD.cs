using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHUD : MonoBehaviour {

	public Font hudFont;
	public Texture WaterTex;
	public Texture MethaneTex;
	public Texture box;
	public LevelManager level;
	public LevelLoader loader;
	public EventPublisher eventPublisher;
	private MouseLook xaxis;
	private MouseLook yaxis;
	private float sens = 15.0f;

	public string[] collectableMolecules = {"Water", "Methane"};
	public Dictionary<string, Texture> moleculeTextures;
	public Dictionary<string, Rect> moleculeTextureRects;
	public Dictionary<string, Rect> moleculeLabelRects;

	private bool paused = false;
	private bool menupaused = false;

	void OnPause( PauseEvent pe ){
		paused = !paused;
		menupaused = (pe.displayMenu && paused) ? true : false;
	}

	void Start () {
		moleculeLabelRects = new Dictionary<string, Rect> {
			{"Water", new Rect (120, Screen.height - 200, 200, 200)},
		    {"Methane", new Rect (120, Screen.height - 300, 200, 200)}
		};
		moleculeTextureRects = new Dictionary<string, Rect> {
			{"Water", new Rect(50,Screen.height - 70,60,60)},
			{"Methane", new Rect(50,Screen.height - 170,60,60)}
		};
		moleculeTextures = new Dictionary<string, Texture> {
			{"Water", WaterTex},
			{"Methane", MethaneTex}
		};
		if (GameObject.Find ("Utilities") != null) {
			loader = GameObject.Find ("Utilities").GetComponent<LevelLoader> ();
		} else { 
			Debug.Log ("No loader game object in scene: " + Application.loadedLevelName);
		}
		if (GameObject.Find ("Level") != null) {
			eventPublisher = GameObject.Find ("Level").GetComponent<EventPublisher> ();
		} else { 
			Debug.Log ("No level game object in scene: " + Application.loadedLevelName);
		}
		xaxis = GameObject.Find ("Player").GetComponent<MouseLook> ();
		yaxis = GameObject.Find ("Attachments").GetComponent<MouseLook> ();
	}
	
	void Update () {
	}
	
	void OnGUI () {
		DrawCollectablesCounter(
			getCollectProgressStr(level.Collected.Count, level.Collectables.Count));
		DrawScore( level.Score.ToString() );

		foreach (string molecule in collectableMolecules) 
		{
			ArrayList molecules = level.GetCollectedByTag (molecule);
			if (molecules.Count != 0) 
			{
				DrawMolecule (molecule, molecules.Count);
			}
		}
		if (menupaused) {
			drawPauseMenu();
		}
	}

	private void drawPauseMenu(){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), box);
		GUIStyle topCenterStyle = GUI.skin.GetStyle("Label");
		topCenterStyle.alignment = TextAnchor.UpperCenter;
		topCenterStyle.fontSize = 48;
		topCenterStyle.font = hudFont;
		topCenterStyle.normal.textColor = Color.yellow;
		//top center
		GUI.Label (new Rect (Screen.width / 2 - 100, 25, 200, 100), "PAUSED", topCenterStyle);
		GUIStyle bottomCenterStyle = GUI.skin.GetStyle("Label");
		bottomCenterStyle.alignment = TextAnchor.MiddleCenter;
		bottomCenterStyle.fontSize = 36;
		bottomCenterStyle.font = hudFont;
		bottomCenterStyle.normal.textColor = Color.white;
		bottomCenterStyle.hover.textColor = Color.yellow;
		if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 6 * 5 - 50, 300, 100), "MAIN MENU", bottomCenterStyle)){
			if( loader != null ){
				eventPublisher.publish ( new PauseEvent(true) );
				eventPublisher.publish ( new ShowMouseEvent(true) );
				loader.LoadLevel("start_menu");
			} else {
				Debug.Log ("No level loader found in scene: " + Application.loadedLevelName);
			}
		}
		if ( GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 6 * 4 - 50, 300, 100), "LEVEL SELECT", bottomCenterStyle) ){
			if( loader != null ){
				eventPublisher.publish ( new PauseEvent(true) );
				eventPublisher.publish ( new ShowMouseEvent(true) );
				loader.LoadLevel("level_select");
			} else {
				Debug.Log ("No level loader found in scene: " + Application.loadedLevelName);
			}
		}
		GUI.Label (new Rect (0, Screen.height / 6 * 2 - 50, Screen.width, 100), "MOUSE SENSITIVITY", bottomCenterStyle);
		Rect sliderBoxOutside = new Rect (Screen.width * 1 / 4, Screen.height * 5 / 12, Screen.width / 2, 50);
		sens = GUI.HorizontalSlider (sliderBoxOutside, sens, 0.0F, 100.0F);
		GUIStyle sliderStyle = GUI.skin.GetStyle("Label");
		sliderStyle.alignment = TextAnchor.MiddleCenter;
		sliderStyle.fontSize = 24;
		sliderStyle.font = hudFont;
		sliderStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width * (sens + 50) / 200 - 30, Screen.height * 5 / 12 + 5, 60, 40),""+(int)(sens*10), sliderStyle);
		xaxis.sensitivityX = sens;
		yaxis.sensitivityY = sens;
	}
	
	public string getCollectProgressStr( int collectRemaining, int totalCollects ){
		return collectRemaining.ToString("00") + "/" + totalCollects;
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

	void DrawMolecule(string molecule, int count){
		string message = "x " + count.ToString ();
		GUIStyle bottomLeftStyle = GUI.skin.GetStyle("Label");
		bottomLeftStyle.alignment = TextAnchor.LowerLeft;
		bottomLeftStyle.fontSize = 32;
		bottomLeftStyle.font = hudFont;
		bottomLeftStyle.normal.textColor = Color.yellow;
		GUI.DrawTexture(moleculeTextureRects[molecule], moleculeTextures[molecule], ScaleMode.ScaleToFit, true, 1.0f);
		GUI.Label(moleculeLabelRects[molecule], message, bottomLeftStyle);
	}
}