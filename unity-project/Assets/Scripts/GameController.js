#pragma strict

private var score : int;
private var count : int;
private var collected : int;
private var startTime : float;
private var levelTime : float;
public var base : GameObject;
private var timeLimit : float;
private var endLevel : boolean;
private var winState : boolean; //true is win false is lose

function Start () 
{
	var collectablesObject : GameObject = GameObject.FindWithTag("Collectables");
	collected = 0;
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
}

function Update()
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

function AddScore(scoreVal : int) 
{
	score += scoreVal;
	Debug.Log(score);
}

function CollectedIncrease()
{
	collected++;
	if(count == collected)
	{
		//Winstate
		//winState = true;
		//EndLevel();
	}
}

function GetCount()
{
	return count;
}

function TimerCountDown()
{
	timeLimit -= (Time.deltaTime);
}

function EndLevel()
{
	levelTime = Time.time - startTime;
	endLevel = true;
	PlayerPrefs.SetInt ("Score", score);
	PlayerPrefs.SetString ("Level" ,Application.loadedLevelName);
	if(winState)
	{
		PlayerPrefs.SetInt ("Win", 1);
		Application.LoadLevel('game_over');
	}
	else
	{
		PlayerPrefs.SetInt ("Win", 0);
		Application.LoadLevel('game_over');
	}
		
}

function GetScore()
{
	return score;
}

function GetTime()
{
	return timeLimit;
}

function GetCollected()
{
	return collected;
}

function ChangeWinState()
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