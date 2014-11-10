#pragma strict

import SimpleJSON;
import System.Security.Cryptography; //HMACSHA256
import System.IO; //StreamReader
import System.Text; //UTF8Encoding  
import System.Net; //HTTPWebRequest

var serverUrl = "http://obscure-temple-1449.herokuapp.com/api/student";
var username = "hmac1";
private var privatekey : String = "1424a6f0-603f-11e4-9803-0800200c9a66";
private var utf8encode : UTF8Encoding = new UTF8Encoding();
private var asciiencode : ASCIIEncoding = new ASCIIEncoding();
private var hmac : HMACSHA256 = HMACSHA256( utf8encode.GetBytes( privatekey ) );

function start(){
}

function update(){
}

function OnMouseUp(){
	postUser(username,"foo","bar","foo@bar.com");
}

function postUser( username:String, firstname:String, lastname:String, email:String ){
	var json : JSONClass = new JSONClass();
	json["username"] = username;
	json["firstName"] = firstname;
	json["lastName"] = lastname;
	json["email"] = email;
	postJson(json);
}

function postJson( json : JSONClass ){
	json["access"] = "unity";
	var unixTimestamp : int = (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;
	json["expires"] = new JSONData(unixTimestamp);
	var utf8json = utf8encode.GetBytes( json.ToSpacelessJSON() );
	_postJson(utf8json);
}

function getXSignature( utf8json : byte[] ){
	return System.Convert.ToBase64String( hmac.ComputeHash( utf8json ));
}

function _postJson( utf8json : byte[] ){
	var request : WebRequest = WebRequest.Create(serverUrl);
	request.Method = "POST";
	request.ContentType = "application/json";
	request.ContentLength = utf8json.Length;
	request.Headers.Add("X-signature", getXSignature(utf8json));
	request.GetRequestStream().Write(utf8json, 0, utf8json.Length);
	request.GetRequestStream().Close();
	
	//downcast to get status description
	var resp : HttpWebResponse = request.GetResponse();
	print( "Response status code: " + resp.StatusDescription );
	if( resp.StatusCode == HttpStatusCode.OK ){
		var stream : Stream = resp.GetResponseStream();
		var reader : StreamReader = new StreamReader(stream);
		var stringResponse : String =  reader.ReadToEnd();
		if( stringResponse.length > 0 ){
			var jsonResponse : JSONNode = JSON.Parse( stringResponse );
			if( jsonResponse ){
				print( "Response json: " + jsonResponse.ToString() );
			}
		}
		reader.Close();
	}
	resp.Close();
}


