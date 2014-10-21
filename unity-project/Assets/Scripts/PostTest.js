#pragma strict

var serverUrl = "http://localhost:8080/api/student";

function start(){
}

function update(){
}

function OnMouseUp(){
	postUser("foobar","foo","bar","foo@bar.com");
}

function postUser( username:String, firstname:String, lastname:String, email:String ){
	//TODO use json parser or sanitize if necessary
	var jsonString = '{"username":"'+username+'","firstName":"'+firstname+
		'","lastName":"'+lastname+'","email":"'+email+'"}';
	postJson(jsonString);
}

function postJson( jsonString:String ){
	var headers = new Hashtable();
	headers.Add("Content-Type", "application/json");
	var encoding = new System.Text.UTF8Encoding();
		
	//Note: have to use www-url-encoded to use current use of WWW, using old use for app/json
	var response = new WWW( serverUrl, encoding.GetBytes(jsonString), headers );
	yield response;
	
	if( response.error ){
		print( "Error submitting form: " + response.error );
		return;
	}
}
