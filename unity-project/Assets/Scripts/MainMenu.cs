using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private Color original_color;
	public Color highlight_color = Color.yellow;
	
	void OnMouseEnter() {
		//this doesn't work in start or update for some reason
		original_color = guiText.material.color;
		//change text to different color
		guiText.material.color = highlight_color;
	}
	
	void OnMouseExit() {
		//change text back to original color
		if(original_color.a > 0){
			guiText.material.color = original_color;
		}
	}
	
	void OnMouseUp() {
		//switch to main_game scene
		LevelLoader loader = GameObject.Find ("Utilities").GetComponent<LevelLoader> ();
		loader.LoadLevel("level_select");
	}
	
	
	void Update () {
		//Exit game if escape is pressed in menu
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
