using UnityEngine;
using System.Collections;

public class DamageEvent : GameEvent{

	public float damage;
	public float preHealth;
	public float postHealth;
	public float maxHealth;

	public DamageEvent(float damage, float preHealth, float maxHealth) : base("OnDamage") 
	{
		this.damage = damage;
		this.preHealth = preHealth;
		this.postHealth = preHealth - damage;
		this.maxHealth = maxHealth;
	}
}
