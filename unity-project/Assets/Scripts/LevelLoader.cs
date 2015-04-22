using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	
	bool levelToLoad = false;
	string levelName;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (levelToLoad && Application.loadedLevelName == "loading_screen") {
			levelToLoad = false;
			Application.LoadLevel(levelName);
		}
	}
	
	public void LoadLevel( string level ) { 
		// use loading screen for level_1-0, 1-1 only
		if (level.Contains ("-")) {
			LoadWithLoadingScreen (level);
		} else if (level.Contains ("start")) {
			Application.LoadLevel (level);
		} else {
			Application.LoadLevel (level);
		}
		
	}
	
	private void LoadWithLoadingScreen( string level ){
		this.levelName = level;
		this.levelToLoad = true;
		Application.LoadLevel ("loading_screen");
	}
}
