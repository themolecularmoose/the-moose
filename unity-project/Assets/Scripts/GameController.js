#pragma strict

private var score : int;
private var count : int;
private var startTime : float;
private var levelTime : float;
public var base : GameObject;
private var timeLimit : float;

function Start () 
{
	var collectablesObject : GameObject = GameObject.FindWithTag("Collectables");
	timeLimit = 12;
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
	timeLimit -= (Time.deltaTime);
	Debug.Log(timeLimit);
	if(timeLimit <= 0)
	{
		Debug.Log("Game Over, buddy");
	}
}

function AddScore(scoreVal : int) 
{
	score += scoreVal;
	Debug.Log(score);
}

function DecreaseCount()
{
	count--;
}

function GetCount()
{
	return count;
}

function EndLevel()
{
	levelTime = Time.time - startTime;
	Debug.Log("You have won this level!");
	Debug.Log("Time taken to complete this level: " + levelTime);
}