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
		if (levelToLoad) {
			levelToLoad = false;
			Application.LoadLevel(levelName);
		}
	}

	public void LoadLevel( string level ) { 
		if (level.Contains ("-")) {
			LoadWithLoadingScreen (level);
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
