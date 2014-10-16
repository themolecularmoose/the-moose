#pragma strict

var serverUrl = "http://requestb.in/13nlt401";

function start(){
}

function update(){
}

function OnMouseUp(){
	postUser("foobar","foo","bar","foo@bar.com");
}

function postUser( username:String, firstname:String, lastname:String, email:String ){
	var jsonString = "{ username:"+username+",firstname:"+firstname+
		",lastname:"+lastname+",email:"+email+"}";
	postJson(jsonString);
}

function postJson( jsonString:String ){
	//var form = new WWWForm();
	//var headers : Hashtable = form.headers;
	//headers["Content-Type"] = "application/json";
	
	var headers = new Hashtable();
	headers.Add("Content-Type", "application/json");
	
	//headers["Content-Length"] = jsonString.Length;
	
	
	var encoding = new System.Text.UTF8Encoding();
	//form.AddBinaryData("", encoding.GetBytes(jsonString));
	
	var response = new WWW( serverUrl, encoding.GetBytes(jsonString), headers );
	yield response;
	
	if( response.error ){
		print( "Error submitting form: " + response.error );
		return;
	}
}
