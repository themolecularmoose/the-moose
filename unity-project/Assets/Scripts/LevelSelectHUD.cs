using UnityEngine;
using System.Collections;

public class LevelSelectHUD : MonoBehaviour {
	public bool showGUI;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if (showGUI) {
			string message = "Select Level";
			GUIStyle helpMessageStyle = GUI.skin.GetStyle ("Box");
			helpMessageStyle.wordWrap = true; 
			helpMessageStyle.alignment = TextAnchor.UpperLeft;
			helpMessageStyle.fontSize = 18;
			helpMessageStyle.alignment = TextAnchor.UpperCenter;
			helpMessageStyle.normal.textColor = Color.yellow;
			GUI.Label (new Rect (200, 200, 200, 200), message, helpMessageStyle);
		}
	}

	void toggleGUI(){
		if (showGUI) {
			showGUI = false;
		} else {
			showGUI = true;
		}
	}
}
