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
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), box);
			GUIStyle topCenterStyle = GUI.skin.GetStyle("Label");
			topCenterStyle.alignment = TextAnchor.UpperCenter;
			topCenterStyle.fontSize = 48;
			topCenterStyle.font = hudFont;
			topCenterStyle.normal.textColor = Color.yellow;
			//top center
			GUI.Label (new Rect (Screen.width / 2 - 100, 25, 200, 100), "PAUSED", topCenterStyle);
			GUIStyle bottomCenterStyle = GUI.skin.GetStyle("Label");
			GUIContent content = new GUIContent();
			bottomCenterStyle.alignment = TextAnchor.MiddleCenter;
			bottomCenterStyle.fontSize = 48;
			bottomCenterStyle.font = hudFont;
			bottomCenterStyle.normal.textColor = Color.white;
			bottomCenterStyle.hover.textColor = Color.yellow;
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 3 * 2 - 50, 300, 100), "EXIT GAME", bottomCenterStyle)){
				if( loader != null ){
					eventPublisher.publish ( new PauseEvent(true, true) );
					loader.LoadLevel("start_menu");
				} else {
					Debug.Log ("No level loader found in scene: " + Application.loadedLevelName);
				}
			}
		}
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