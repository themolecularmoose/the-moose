using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour {
	// Vars set from unity editor
	public float timeLimit;
	public Vector3 checkpoint;

	private Dictionary<string, List<GameObject>> collected;
	private Dictionary<string, List<GameObject>> collectables;
	private float startTime; 
	private bool endLevel;
	private bool winState; //true is win false is lose

	private StateObj state;

	private int score;

	private ShipBehaviour ship;

	// Use this for initialization
	void Start () {
		ship = GameObject.Find("Player").GetComponent<ShipBehaviour>();
		collectables = GetCollectables ();
		collected = new Dictionary<string, List<GameObject>>();
		state = new StateObj ();
		endLevel = false;
		winState = false;
		startTime = Time.deltaTime;

		//hide cursor
		Screen.lockCursor = true;
		Screen.showCursor = false;
		score = 0;
	}

	private Dictionary<string, List<GameObject>> GetCollectables()
	{
		Dictionary<string, List<GameObject>> tmpDic = new Dictionary<string, List<GameObject>>();
		GameObject collectablesObject = GameObject.Find ("Collectables");
		foreach (Transform child in collectablesObject.transform) 
		{
			GameObject tmpChild = child.gameObject;
			if(!tmpDic.ContainsKey(tmpChild.tag)){
				tmpDic.Add (child.tag, new List<GameObject>());
			}
			tmpDic[tmpChild.tag].Add(tmpChild);
		}
		return tmpDic;
	}

	private Dictionary<string, List<GameObject>> TagLookupTable(ArrayList list) {
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

	private ArrayList Flatten(Dictionary<string, List<GameObject>> table) {
		ArrayList tmp = new ArrayList();
		foreach(KeyValuePair<string, List<GameObject>> entry in table) {
			tmp.AddRange(entry.Value);
		}
		return tmp;
	}

	public void OnDeath() {
		if(!RespawnPlayer (ship.gameObject)) 
			EndLevel();
	}

	public void OnDamage(float damage) {
		// NOOP
	}

	public void OnCollect(GameObject collectable) {
		CollectCollectable (collectable);
	}

	public void TimerCountDown()
	{
		timeLimit -= (Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
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

	public bool RespawnPlayer(GameObject player){
		if (this.checkpoint != null) {
			this.score = this.state.getScore();
			ArrayList collectedList = this.state.getCollected();
			this.collected = TagLookupTable(collectedList);
			ship.Health = this.state.getHealth();
			ship.BeamEnergy = this.state.getBeamenergy();

			foreach(GameObject obj in collectedList){
				obj.SetActive(true);
			}
			player.transform.position = this.checkpoint;
			return true;
		}
		return false;
	}

	public void SetCheckpoint(Vector3 checkpoint){
		this.state.SaveState(score,Flatten(collected),ship.BeamEnergy, ship.Health);
		this.checkpoint = checkpoint;
	}

	public void EndLevel()
	{
		float levelTime = Time.time - startTime;
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
