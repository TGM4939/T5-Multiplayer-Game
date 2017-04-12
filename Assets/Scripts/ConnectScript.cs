using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectScript : Photon.PunBehaviour
{
	const string VERSION = "v0.0.1";
	public string RoomName = "Room";
	public Text NetworkStatus;
	public Button PlayButton;
	public Animator Animator;
	//bool Ready;

	// Use this for initialization
	void Start () 
	{
		//Ready = false;
		PhotonNetwork.ConnectUsingSettings (VERSION);
		PhotonNetwork.automaticallySyncScene = true;
		PlayButton.interactable = false;
		Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!PhotonNetwork.connected) 
		{
			NetworkStatus.text = "Connecting...";
			NetworkStatus.color = Color.red;
			PlayButton.interactable = false;
		}
		else
		{
			NetworkStatus.text = "Ready!";
			NetworkStatus.color = Color.black;
			PlayButton.interactable = true;
		}
	}

	public void ConnectToNetwork()
	{
		//GetCharacter ();

		//Animator.SetBool("Scene", false); //Will make the Scene Disappear..
	}

	public void OnJoinedRoom()
	{
		//PhotonNetwork.LoadLevel ("Multiplayer");
	}

	public void GetCharacter()
	{
		foreach (GameObject obj in FindObjectsOfType<GameObject>()) 
		{
			if (obj.name == "Fox(Clone)")
			{
				PlayerPrefs.SetString ("Character", "Fox");
			}
			else if (obj.name == "Wolf(Clone)")
			{
				PlayerPrefs.SetString ("Character", "Wolf");
			}
		}
	}
		
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		if (PhotonNetwork.playerList.Length == 2 && PhotonNetwork.isMasterClient) 
		{
			GetCharacter ();

			Animator.SetBool("Scene", false); //Will make the Scene Disappear..
			PhotonNetwork.LoadLevel ("Multiplayer");
		}
	}

	public void ConnectToRoom()
	{
		if (PhotonNetwork.connected) 
		{
			Debug.Log ("Connecting to a room");
			RoomOptions roomOptions = new RoomOptions () { IsVisible = false, MaxPlayers = 2 };
			PhotonNetwork.JoinOrCreateRoom (RoomName, roomOptions, TypedLobby.Default);
		}
	}
}
