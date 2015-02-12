using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int score;
	private int count;
	private int collected;
	private int beamEnergy;
	private float startTime;
	private float levelTime;
	public GameObject homebase;
	private float timeLimit;
	private bool endLevel;
	private bool winState; //true is win false is lose
	private bool tractorBeam;
	private int waterCount;
	private int methaneCount;

	public GameObject checkpoint;
	private StateObj state;

	// Use this for initialization
	void Start () {

		GameObject collectablesObject = GameObject.FindWithTag ("Collectables");
		state = new StateObj ();
		RefillEnergy();
		collected = 0;
		endLevel = false;
		winState = false;
		tractorBeam = false;
		timeLimit = 60 * 3;
		waterCount = 0;
		methaneCount = 0;
		
		//hide cursor
		Screen.lockCursor = true;
		Screen.showCursor = false;
		
		if(collectablesObject != null)
		{
			count = collectablesObject.transform.childCount;
		}
		startTime = Time.time;
		
		score = 0;
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
	
	void FixedUpdate () {
		if(Input.GetButton("Tractor Beam"))
		{
			beamState(true);
		}
		else
		{
			beamState(false);
		}
	}

	public void AddScore(int scoreVal) 
	{
		score += scoreVal;
	}

	public void SetCheckpoint(GameObject cp){
		Debug.Log ("Setting checkpoint");
		//this.state.SaveState(score,count,waterCount,methaneCount,startTime);
		this.checkpoint = cp;
	}

	public void RespawnPlayer(GameObject player){
		if (this.checkpoint != null) {
			Debug.Log ("Relocating player");
			player.transform.position = this.checkpoint.transform.position;
			//this.score = this.state.getScore();
			//this.count = this.state.getCount();
			//this.waterCount = this.state.getWatercount();
			//this.methaneCount = this.state.getMethanecount();
			//this.startTime = this.state.getStarttime();

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
	
	public void beamState(bool state)
	{
		if(beamEnergy <= 0)
		{
			tractorBeam = false;
		}
		else
		{
			tractorBeam = state;
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
	
	public void RefillEnergy()
	{
		beamEnergy = 100;
		waterCount = 0;
		methaneCount = 0;
	}
}
