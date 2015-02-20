using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int score;
	private int count;
	private float startTime;
	private float levelTime;
	public GameObject homebase;
	private float timeLimit;
	private bool endLevel;
	private bool winState; //true is win false is lose
	private int beamEnergy;
	private int collected;
	private int waterCount;
	private int methaneCount;
	public ArrayList collected_collectables;
	private float health;
	private bool tractorBeam;

	public GameObject checkpoint;

	public ArrayList all_collectables;

	private StateObj state;

	// Use this for initialization
	void Start () {


		GameObject collectablesObject = GameObject.FindWithTag ("Collectables");

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Water");
		all_collectables = new ArrayList ();

		for (int i=0; i<objs.Length; i++) {
			all_collectables.Add(objs[i]);
		}

		state = new StateObj ();
		endLevel = false;
		winState = false;
		timeLimit = 60 * 3;

		//hide cursor
		Screen.lockCursor = true;
		Screen.showCursor = false;
		
		if(collectablesObject != null)
		{
			count = collectablesObject.transform.childCount;
		}
		startTime = Time.time;
		
		score = 0;
		collected_collectables = new ArrayList();
		beamEnergy = 100;
		waterCount = 0;
		methaneCount = 0;
		collected = 0;
		tractorBeam = false;
		waterCount = 0;
		methaneCount = 0;
		health = 100;
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

	public void AddScore(int scoreVal) 
	{
		score += scoreVal;
	}

	public void SetCheckpoint(GameObject cp){
		Debug.Log ("Setting checkpoint");
		/*
		 * NEED TO ADAPT TO NEW ShipBehaviour, LevelManager, GameManager structure.
		 */
		this.state.SaveState(collected_collectables,score,collected,waterCount,methaneCount,beamEnergy);
		collected_collectables.Clear ();
		this.checkpoint = cp;
	}

	public void RespawnPlayer(GameObject player){
		if (this.checkpoint != null) {
			player.transform.position = this.checkpoint.transform.position;

			this.score = this.state.getScore();
			this.collected = this.state.getCollected();
			this.waterCount = this.state.getWatercount();
			this.methaneCount = this.state.getMethanecount();
			this.beamEnergy = this.state.getBeamenergy();

			collected_collectables.Clear ();
			foreach(GameObject molecule in this.state.getCollectables()){
				all_collectables.Remove (molecule);
			}
			foreach(GameObject obj in all_collectables){
				//Instantiate (obj);
				obj.SetActive(true);
			}
			//Debug.Log("Score " + this.score + " count " + this.count + " water " + 
			//          this.waterCount + " meth " + this.methaneCount + " time " + this.startTime); 
		}
	}

	public void CollectedIncrease(string type)
	{
		collected++;
		beamEnergy -= 10;
		if(type == "Water")
		{
			waterCount++;
		}
		else
		{
			methaneCount++;
		}
	}
	
	public int GetCount()
	{
		return count;
	}
	
	public void TimerCountDown()
	{
		timeLimit -= (Time.deltaTime);
	}
	
	public void EndLevel()
	{
		levelTime = Time.time - startTime;
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

	public void CollectCollectable(GameObject collectable){
		this.collected_collectables.Add(collectable);
	}

	public int GetScore()
	{
		return score;
	}
	
	public int GetTime()
	{
		return (int) timeLimit;
	}
	
	public int GetCollected()
	{
		return collected;
	}
	
	public int GetWater()
	{
		return waterCount;
	}
	
	public int GetMethane()
	{
		return methaneCount;
	}
	
	public bool isBeamOn()
	{
		return tractorBeam;
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

	public void DecreaseHealth(float damage) {
		health -= damage;
	}

	public float GetHealth() {
		return health;
	}

	public void OnDeath() {
		EndLevel (); // TODO: replace with checkpoint loading
	}

	public void OnDamage(float damage) {
		DecreaseHealth(damage);
	}
}
