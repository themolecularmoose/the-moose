using UnityEngine;
using System.Collections;

public class CollectableEvent : GameEvent{

	public GameObject collectable; 
	public CollectableEvent(GameObject collectable) : base("OnCollect") 
	{
		this.collectable = collectable;
	}
}
