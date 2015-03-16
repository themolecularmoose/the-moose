using UnityEngine;
using System.Collections;

public class CollectableEvent : GameEvent{

	public int number; 
	public CollectableEvent(int num) : base("OnDamage") 
	{
		this.number = num;
	}
}
