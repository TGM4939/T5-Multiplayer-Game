using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour 
{
	public Transform[] SpawnPoints;
	private string Character;

	// Use this for initialization
	void Start () 
	{
		Character = PlayerPrefs.GetString ("Character");
		PhotonNetwork.Instantiate (Character, SpawnPoints[PhotonNetwork.player.ID - 1].position, Quaternion.identity, 0);
	}
}
