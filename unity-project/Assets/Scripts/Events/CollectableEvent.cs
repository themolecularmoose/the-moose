using UnityEngine;
using System.Collections;

public class CollectableEvent : GameEvent{

	public GameObject collectable;
	public bool decollect;
	public CollectableEvent(GameObject collectable, bool a_decollect = false) : base(a_decollect ? "OnDecollect" : "OnCollect") 
	{
		this.collectable = collectable;
		this.decollect = a_decollect;
	}
}
