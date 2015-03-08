using UnityEngine;
using System.Collections;

public class DamageEvent : GameEvent{

	public float damage;
	public float preHealth;
	public float postHealth;
	public float maxHealth;

	public DamageEvent(string name, float damage, float preHealth, float maxHealth) : base(name) 
	{
		this.damage = damage;
		this.preHealth = preHealth;
		this.postHealth = preHealth - damage;
		this.maxHealth = maxHealth;
	}
}
