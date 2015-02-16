using UnityEngine;
using System.Collections;

public class FluidCollision : MonoBehaviour
{
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Player"){
			collision.gameObject.SendMessage("HitFluid");
		}
	}
}

