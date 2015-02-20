using UnityEngine;
using System.Collections;

public class PlayerCollison : MonoBehaviour
{
	public GameController gameController;

	// Use this for initialization
	void Start ()
	{
		rigidbody.freezeRotation = true;
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	/*void FixedUpdate () {
		if(Input.GetButton("Tractor Beam"))
		{
			gameController.beamState(true);
		}
		else
		{
			gameController.beamState(false);
		}
	}

	void HitFluid(){
		Debug.Log("Hit fluid");
	}
	
	void HitObject()
	{
		Debug.Log("Hit base!");
		wait ();
		Debug.Log("Good to go");
	}

	IEnumerator wait()
	{	
		yield return new WaitForSeconds (1);
	}*/
}

