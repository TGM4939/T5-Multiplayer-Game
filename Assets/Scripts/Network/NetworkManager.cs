using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
	
	public string PlayerPrefabName = "golem";
	public Transform SpawnPoint;

	// Use this for initialization
	void Start () 
	{
		PhotonNetwork.Instantiate (PlayerPrefabName, SpawnPoint.position, SpawnPoint.rotation, 0);
	}
}
