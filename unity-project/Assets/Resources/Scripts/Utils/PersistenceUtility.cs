//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine; 
using System.Net;

public class PersistenceUtility : MonoBehaviour
{
	public static string BASE_URL;
	public static string LOAD_URL;
	public static string SAVE_URL;
	private static ConnectionUtility cxnUtility;

	private static PersistenceUtility instance;

	//for singleton
	public static PersistenceUtility GetInstance() {
		return instance;
	}
	
	//for singleton
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
		//attempts to fetch cookie
		CookieUtility.RequestCookie ();
	    cxnUtility = new ConnectionUtility( BASE_URL );
	}

	bool LoadGame() {
		//TODO try GET
		ServerRequest sq = new ServerRequest ();
		HttpWebResponse webResp = cxnUtility.Post<ServerRequest> (LOAD_URL, sq);
		ServerResponse resp = ConnectionUtility.JsonDeserialize<ServerResponse> (webResp);
		return (resp != null) ? resp.success : false;
	}

	bool SaveGame(GameSave gs) {
		SaveGameRequest sgr = new SaveGameRequest (gs);
		HttpWebResponse webResp = cxnUtility.Post<SaveGameRequest> (SAVE_URL, sgr);
		ServerResponse resp = ConnectionUtility.JsonDeserialize<ServerResponse> (webResp);
		//TODO Log save result
		return (resp != null) ? resp.success : false;
	}

}
