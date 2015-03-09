//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using NUnit.Framework;
using System;
using System.Net;
using System.IO;
using System.Runtime.Serialization;

namespace AssemblyCSharp
{
	[TestFixture()]
	public class ConnectionUtilityTest
	{
		[Test()]
		public void TestTimestampGeneration()
		{
			ServerRequest jc = new ServerRequest ();
			//Timestamp is later than Dec 31, 2014
			Assert.IsTrue ( Int32.Parse (jc.expires) > 1424000000);
		}

		//Get and Post tests are commented as they require endpoints and have no asserts
		//[Test()]
		public void testGetToServer()
		{
			ConnectionUtility cu = new ConnectionUtility ();
			ServerRequest jc = new ServerRequest ();
			String path = "/api/student/";
			HttpWebResponse resp = cu.Get (path, jc);
			StreamReader sr = new StreamReader (resp.GetResponseStream ());
			String stringResp = sr.ReadToEnd ();
			Console.WriteLine (stringResp);
		}

		//[Test()]
		public void testGetToRequestBin()
		{
			ConnectionUtility cu = new ConnectionUtility ("http://requestb.in/w2mernw2");
			ServerRequest jc = new ServerRequest ();
			HttpWebResponse resp = cu.Get ("", jc);
			StreamReader sr = new StreamReader (resp.GetResponseStream ());
			String stringResp = sr.ReadToEnd ();
			Console.WriteLine (stringResp);
		}

		//[Test()]
		public void testPostToRequestBin()
		{
			ConnectionUtility cu = new ConnectionUtility ("http://requestb.in/w2mernw2");
			ServerRequest jc = new ServerRequest ();
			HttpWebResponse resp = cu.Post ("", jc);
			StreamReader sr = new StreamReader (resp.GetResponseStream ());
			String stringResp = sr.ReadToEnd ();
			Console.WriteLine (stringResp);
		}

		[Test()]
		public void testDeserialize()
		{
			String validJson = "{\"success\":true,\"result\":[{\"username\":\"ven\",\"first_name\":\"John\",\"last_name\":\"Same\"," + 
				"\"email\":\"tt@gmail.com\"},{\"username\":\"ted\",\"first_name\":\"Billy\",\"last_name\":\"Woot\",\"email\":\"jah@gmail.com\"}," + 
					"{\"username\":\"foobar\",\"first_name\":\"foo\",\"last_name\":\"bar\",\"email\":\"foo@bar.com\"},{\"username\":\"testuser1\"," + 
					"\"first_name\":\"foo\",\"last_name\":\"bar\",\"email\":\"foo@bar.com\"},{\"username\":\"testuser2\",\"first_name\":\"foo\"," + 
					"\"last_name\":\"bar\",\"email\":\"foo@bar.com\"},{\"username\":\"Johny\",\"first_name\":\"John\",\"last_name\":\"Doe\",\"email\"" + 
					":\"john@gmail.com\"},{\"username\":\"hwhw\",\"first_name\":\"john\",\"last_name\":\"Carl\",\"email\":\"jogn@carl.com\"},{\"username\"" + 
					":\"hmac1\",\"first_name\":\"foo\",\"last_name\":\"bar\",\"email\":\"foo@bar.com\"},{\"username\":\"hmac2\",\"first_name\":\"foo\"" + 
					",\"last_name\":\"bar\",\"email\":\"foo@bar.com\"}],\"message\":\"yep\"}";
			ServerResponse sr = ConnectionUtility.JsonDeserialize<ServerResponse> (validJson);
			Assert.IsTrue (sr.success == true);
			Assert.IsTrue (sr.userList.Length == 9);
			Assert.IsTrue (sr.message == "yep");
		}
	}
}

