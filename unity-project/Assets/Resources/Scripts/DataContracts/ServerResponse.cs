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
using System.Runtime.Serialization;

//This is mostly proof of concept since server data will be different

[DataContract]
public class ServerResponse
{
	[DataMember]
	public Boolean success;

	[DataMember]
	public String message;

	[DataMember(Name="result")]
	public ServerUser[] userList;

}

[DataContract]
public class ServerUser
{
	[DataMember]
	public String username;

	[DataMember(Name="first_name")]
	public String firstName;

	[DataMember(Name="last_name")]
	public String lastName;

	[DataMember]
	public String email;
	
}
