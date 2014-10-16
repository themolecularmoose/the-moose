#pragma strict

function start(){
}

function update(){
}

function OnMouseUp(){
	testPost();
}

function testPost(){
	var form = new WWWForm();
	var url = "http://requestb.in/13nlt401";
	form.AddField( "game", "MyGameName" );
	form.AddField( "score", "500" );
	
	var response = new WWW( url, form );
	yield response;
	
	if( response.error ){
		print( "Error submitting form: " + response.error );
		return;
	}
}
