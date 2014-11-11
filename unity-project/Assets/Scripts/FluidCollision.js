#pragma strict

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag == "Player"){
    	collision.gameObject.SendMessage("HitFluid");
    }
}