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
using System.Runtime.Serialization.Json; // for DataContractJsonSerializer

//This is mostly proof of concept since server data will be different

[DataContract]
public class ServerResponse
{
	[DataMember]
	internal Boolean success;

	[DataMember]
	internal String message;

	[DataMember(Name="result")]
	internal ServerUser[] userList;

}

[DataContract]
public class ServerUser
{
	[DataMember]
	internal String username;

	[DataMember(Name="first_name")]
	internal String firstName;

	[DataMember(Name="last_name")]
	internal String lastName;

	[DataMember]
	internal String email;
	
}
