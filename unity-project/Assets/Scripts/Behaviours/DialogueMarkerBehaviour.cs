using UnityEngine;
using System.Collections;

public class DialogueMarkerBehaviour : MonoBehaviour {
	public Font m_font;
	[Multiline]
	public string m_text = "Default Text.\nShould make room for new text.\nLiteral Room.\nLike 4 lines.";
	bool m_on;
	float m_spinRate;
	GUIContent m_content;
	CartoonBehaviour m_behaviour;
	private bool paused = false;
	private bool displayMenu = false;

	void OnPause( PauseEvent pe ) {
		paused = !paused;
		displayMenu = (paused && pe.displayMenu);
	}
	
	// Use this for initialization
	void Start () {
		m_behaviour = GetComponent<CartoonBehaviour>();
		m_spinRate = m_behaviour.m_spinRate;
		m_content = new GUIContent(m_text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		if(m_on && !displayMenu)
		{
			drawDialogue();
		}
	}
	
	void drawDialogue()
	{
		float paddingX = 100;
		float paddingY = 100;
		float x = paddingX;
		float width = Screen.width - paddingX * 2;
		
		GUIStyle helpMessageStyle = GUI.skin.GetStyle("Box");
		helpMessageStyle.wordWrap = true; 
		helpMessageStyle.alignment = TextAnchor.MiddleCenter;
		helpMessageStyle.fontSize = 18;
		helpMessageStyle.font = m_font;
		helpMessageStyle.normal.textColor = Color.yellow;
		float height = helpMessageStyle.CalcHeight(m_content, width);
		float y = Screen.height - paddingY - height;
		
		GUI.Box (new Rect (x, y, width, height), m_content, helpMessageStyle);
	}
	
	void turnOn()
	{
		m_on = true;
		m_behaviour.m_spinRate = m_spinRate * 6;
	}
	void turnOff()
	{
		m_on = false;
		m_behaviour.m_spinRate = m_spinRate;
	}
	
	void OnTriggerEnter(Collider a_other)
	{
		if (a_other.gameObject.tag != "Player")
			return;
		turnOn ();
	}
	
	void OnTriggerExit(Collider a_other)
	{
		if (a_other.gameObject.tag != "Player")
			return;
		turnOff ();
		Destroy (this.gameObject);
	}
}
