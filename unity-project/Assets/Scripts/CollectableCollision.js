#pragma strict

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag == "Player"){
		Debug.Log("Hit by player");
    	Destroy(this.gameObject);
    	//Destroy(this);
    	collision.gameObject.SendMessage("HitCollectable");
    }
}