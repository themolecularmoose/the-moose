// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections;

public class StateObj
{
	
	private int score;
	private ArrayList collected;
	private int beamenergy;
	private float health;

	public StateObj ()
	{
		collected = new ArrayList();
	}

	public void SaveState(int score, ArrayList collected, int beamEnergy, float health)
	{
		this.score = score;
		this.collected = collected;
		this.beamenergy = beamEnergy;
		this.health = health;
	}

	public ArrayList getCollected(){
		return this.collected;
	}

	public int getScore(){
		return this.score;
	}
	public int getBeamenergy(){
		return this.beamenergy;
	}

	public float getHealth(){
		return this.health;
	}
}

