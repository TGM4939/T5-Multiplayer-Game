using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// For the RestartPanel and its text.
	public GameObject restartPanel;

	SelectCharacter selectCharObj;
	//CharHealthBar charHBObj;

	public Transform playerPos;
	public GameObject playerGO;

	void Start () 
	{
		//GameObject.DontDestroyOnLoad (GetComponent<SelectCharacter> ().passChar);
		restartPanel.SetActive (false);
		//charHBObj = GetComponent<CharHealthBar> ();

		GameObject newChar = (GameObject) Instantiate (SelectCharacter.passChar, playerPos.position, Quaternion.identity);
		newChar.transform.parent = gameObject.transform;

		/*	playerGO = GetComponent <SelectCharacter> ().passChar;

		GameObject newChar = (GameObject) Instantiate (playerGO, playerPos.position, Quaternion.identity);
		newChar.transform.parent = gameObject.transform;
		Debug.Log ("Passing Char: " + selectCharObj.passChar);*/
	}
		
	// Update is called once per frame
	void Update () 
	{
		//if (charHBObj.currentHealth <= 0) 
		//{
		//	restartPanel.SetActive (true);
		//} 
	}
}
