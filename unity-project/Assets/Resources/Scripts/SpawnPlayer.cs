using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour
{
	public GameObject player;
	// Use this for initialization
	void Start ()
	{
		Instantiate(player, this.transform.position, this.transform.rotation);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

