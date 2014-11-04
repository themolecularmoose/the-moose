#pragma strict

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag == "Player"){
    	Destroy(this.gameObject);
    	collision.gameObject.SendMessage("HitCollectable");
    }
}