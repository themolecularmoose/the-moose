using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ShipAudio : MonoBehaviour {
	
	public AudioSource damage;
	public AudioSource boost;
	public AudioSource ship;
	public AudioClip explode;
	public AudioClip shoot;

	public void PlayDamage()
	{
		damage.Play ();
	}
	
	public void PlayBoost()
	{
		boost.Play();
	}

	public void PlayBuster ()
	{
		ship.PlayOneShot (shoot);
	}

	public void IncVol()
	{
		ship.volume = 0.6f;
	}

	public void DecVol()
	{
		ship.volume = 0.2f;
	}

	public void ToggleEngine(bool death)
	{
		if (death)
		{
			ship.Stop ();
			damage.PlayOneShot (explode);
		}
		else
		{
			ship.Play ();
		}
	}
}
