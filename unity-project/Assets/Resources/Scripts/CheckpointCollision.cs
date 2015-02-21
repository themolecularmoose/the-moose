using UnityEngine;
using System.Collections;

public class CheckpointCollision : MonoBehaviour
{
	public LevelManager level;
	
	// Use this for initialization
	void Start ()
	{
		level = GameObject.Find("Level").GetComponent<LevelManager>();
		
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player"){
			level.SetCheckpoint(this.gameObject.transform.position);
			Destroy (this.gameObject);
		}
	}
}