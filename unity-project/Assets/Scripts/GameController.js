#pragma strict

private var score : int;
private var count : int;
private var startTime : float;
private var levelTime : float;
public var base : GameObject;

function Start () 
{
	var collectablesObject : GameObject = GameObject.FindWithTag("Collectables");
	Screen.showCursor = false;
	
	if(collectablesObject != null)
	{
		count = collectablesObject.transform.childCount;
	}
	startTime = Time.time;
	
	score = 0;
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