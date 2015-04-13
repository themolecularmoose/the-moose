using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ShipAudio : MonoBehaviour {
	
	public AudioSource damage;
	public AudioSource boost;
	
	public void PlayDamage()
	{
		damage.Play ();
	}
	
	public void PlayBoost()
	{
		boost.Play();
	}
}
