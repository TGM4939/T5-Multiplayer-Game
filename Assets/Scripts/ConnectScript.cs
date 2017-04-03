using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectScript : MonoBehaviour {

	const string VERSION = "v0.0.1";
	public string RoomName = "Room";
	public Text NetworkStatus;
	public Button PlayButton;

	// Use this for initialization
	void Start () 
	{
		PhotonNetwork.ConnectUsingSettings (VERSION);
		PhotonNetwork.automaticallySyncScene = true;
		PlayButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!PhotonNetwork.connected) 
		{
			NetworkStatus.text = "Connecting";
			NetworkStatus.color = Color.red;
			PlayButton.interactable = false;
		}
		else 
		{
			NetworkStatus.text = "Ready";
			NetworkStatus.color = Color.green;
			PlayButton.interactable = true;
		}
	}

	public void ConnectToNetwork()
	{
		if (PhotonNetwork.connected) 
		{
			RoomOptions roomOptions = new RoomOptions () { IsVisible = false, MaxPlayers = 2 };
			PhotonNetwork.JoinOrCreateRoom (RoomName, roomOptions, TypedLobby.Default);
		}
	}

	public void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel ("Multiplayer");
	}
}
