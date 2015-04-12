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
			StartCoroutine( DelayedLevelLoad ( 2 , levelName ));
		}
	}

	public void LoadLevel( string level ) { 
		this.levelName = level;
		this.levelToLoad = true;
		Application.LoadLevel ("loading_screen");
	}

	IEnumerator DelayedLevelLoad( int num, string level ){
		yield return new WaitForSeconds (num);
		Application.LoadLevel (level);
	}
}
