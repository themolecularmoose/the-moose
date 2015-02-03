﻿using UnityEngine;
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


	// Use this for initialization
	void Start () {

		GameObject collectablesObject = GameObject.FindWithTag ("Collectables");
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

	public void AddScore(int scoreVal) 
	{
		score += scoreVal;
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
	
	public float GetTime()
	{
		return timeLimit;
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
