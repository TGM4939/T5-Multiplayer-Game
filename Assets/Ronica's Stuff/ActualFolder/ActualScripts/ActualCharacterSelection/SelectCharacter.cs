using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
	// Array that contains the characters
	public GameObject[] characterArray = new GameObject[2];
	public GameObject[] ActiveCharScene = new GameObject[2];

	// Array for the buttons
	public GameObject[] characterBtn = new GameObject[2];

	public int index;

	// To get the transform values of the spawn point gameobject.
	public Transform spawnPoint;

	// To check if a character is active in the scene.
	bool charIsActive;

	// Stores the character gameobject active on the platform.
	public static GameObject playerSelected;
	public static GameObject passChar;

	GameObject activeChar;
	Vector3 oldRotation;

	// Use this for initialization
	void Start () 
	{
		// Setting bool value to false, as there is no active character.
		charIsActive = false;
	}

	public void SelectPlayer()
	{
		// If a character is in scene
		if(charIsActive)
		{
			
			activeChar = GameObject.FindGameObjectWithTag ("Character");
			Destroy (activeChar);
			charIsActive = false;

			oldRotation = activeChar.transform.eulerAngles;
		}

		// Getting the name of the gameobject currently selected in the Event System.
		string btnName = EventSystem.current.currentSelectedGameObject.transform.name;

		// Checking if the button clicked, its string name exists in characterBtn array.
		for(int i=0;i<characterBtn.Length;i++)
		{
			// If the name match then pass the index value.
			if(btnName == characterBtn[i].name)
			{
				SpawnCharacter (i);
			}
		}
	}

	// To spawn the character on the platform
	public void SpawnCharacter(int indexValue)
	{
		index = indexValue;
		// Instantiate character and setting bool value to true.
		Instantiate (characterArray [indexValue], spawnPoint.position, Quaternion.Euler(oldRotation));
		charIsActive = true;
		playerSelected = characterArray [indexValue];
		passChar = ActiveCharScene [indexValue];
		playerSelected.transform.eulerAngles = oldRotation;
	}

	// To go to a scene.
	public void LoadScene(string sceneName)
	{
		if (charIsActive)
		{
			SceneManager.LoadScene (sceneName);

		}
	}
}


// HELP FROM LINK : http://answers.unity3d.com/questions/1215992/ui-button-onclick-function-how-to-get-name-of-butt.html