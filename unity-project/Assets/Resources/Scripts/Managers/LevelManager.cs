using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LevelManager : MonoBehaviour {
	// Vars set from unity editor
	public float timeLimit;
	public Vector3 checkpoint;

	private Dictionary<string, List<GameObject>> collected;
	private Dictionary<string, List<GameObject>> collectables;
	private bool endLevel;
	private bool winState; //true is win false is lose

	private StateObj state;

	private int score;

	private ShipBehaviour ship;

	public GameObject guiGets; 
	public GUIManager GUIMan; // Gui manager script.


	void OnEnable () 
	{
		// Setup level vars
		collectables = TagLookupTable (GetCollectables ());
		collected = new Dictionary<string, List<GameObject>>();
		endLevel = false;
		winState = false;
		score = 0;
		state = new StateObj ();
	}

	// Use this for initialization
	void Start () 
	{
		//hide cursor
		Screen.lockCursor = true;
		Screen.showCursor = false;

		ship = GameObject.Find("Player").GetComponent<ShipBehaviour>();
		SetCheckpoint(ship.transform.position);

		GUIMan = guiGets.GetComponent<GUIManager>();
	
	}

	private ArrayList GetCollectables()
	{
		GameObject collectablesObject = GameObject.Find ("Collectables");
		ArrayList tmpList = new ArrayList (); 
		foreach (Transform child in collectablesObject.transform) 
		{
			tmpList.Add(child.gameObject);
		}
		return tmpList;
	}

	private Dictionary<string, List<GameObject>> TagLookupTable(ArrayList list) 
	{
		Dictionary<string, List<GameObject>> tmpDic = new Dictionary<string, List<GameObject>>();
		foreach (GameObject tmpObj in list) 
		{
			if(!tmpDic.ContainsKey(tmpObj.tag)){
				tmpDic.Add (tmpObj.tag, new List<GameObject>());
			}
			tmpDic[tmpObj.tag].Add (tmpObj);
		}
		return tmpDic;
	}

	private ArrayList Flatten(Dictionary<string, List<GameObject>> table) 
	{
		ArrayList tmp = new ArrayList();
		foreach(KeyValuePair<string, List<GameObject>> entry in table) {
			tmp.AddRange(entry.Value);
		}
		return tmp;
	}

	public void OnDeath() 
	{
		RespawnPlayer (ship.gameObject);
	}

	public void OnDamage(float damage) 
	{
		// NOOP
	}

	public void UpdateGUIBars(Vector4 res)
	{
		GUIMan.UpdateGUI(res);
		Debug.Log ("Passing it along");
	}

	public void OnCollect(GameObject collectable) {
		CollectCollectable (collectable);
	}

	public void TimerCountDown()
	{
		timeLimit -= (Time.deltaTime);
	}

	// Update is called once per frame
	void Update () 
	{
		if(endLevel == false)
		{
			if(timeLimit <= 0)
			{
				//Lose state
				EndLevel();
			}
			else
			{
				TimerCountDown();
			}
		}
	}

	public void RespawnPlayer(GameObject player)
	{
		this.score = this.state.getScore();
		ArrayList collectedList = this.state.getCollected();
		List<GameObject> saveCollected = collectedList.Cast<GameObject>().ToList();
		List<GameObject> curCollected = Flatten(this.collected).Cast<GameObject>().ToList();
		IEnumerable<GameObject> enables = curCollected.Except (saveCollected);
		ship.Health = this.state.getHealth();
		ship.BeamEnergy = this.state.getBeamenergy();
		foreach(GameObject obj in enables){
			obj.SetActive(true);
		}
		this.collected = TagLookupTable(collectedList);
		player.transform.position = this.checkpoint;
		player.rigidbody.velocity = Vector3.zero;
		player.rigidbody.angularVelocity = Vector3.zero;
	}

	public void SetCheckpoint(Vector3 checkpoint)
	{
		this.state.SaveState(score,Flatten(collected),ship.BeamEnergy, ship.Health);
		this.checkpoint = checkpoint;
	}

	public void EndLevel()
	{
		endLevel = true;
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetString ("Level" ,Application.loadedLevelName);
		if(winState)
		{
			PlayerPrefs.SetInt ("Win", 1);
			Application.LoadLevel("game_over");
		}
		else
		{
			PlayerPrefs.SetInt ("Win", 0);
			Application.LoadLevel("game_over");
		}
		
	}

	public void ChangeWinState()
	{
		if(winState)
		{
			winState = false;
		}
		else
		{
			winState = true;
		}
	}

	public void CollectCollectable(GameObject collectable){
		if(!this.collected.ContainsKey(collectable.tag)) {
			this.collected.Add(collectable.tag, new List<GameObject>());
		}
		this.collected[collectable.tag].Add (collectable);
	}

	public ArrayList GetCollectedByTag(string tag) 
	{
		if(collected.ContainsKey(tag)){
			return new ArrayList(collected [tag]);
		} else {
			return new ArrayList();
		}
	}

	public int Score
	{
		get{ return score;}
	}
	
	public int TimeLimit
	{
		get{ return (int)timeLimit;}
	}
	
	public ArrayList Collected
	{
		get{
			return Flatten (this.collected);
		}
	}

	public ArrayList Collectables
	{
		get{
			return Flatten (this.collectables);
		}
	}
}
