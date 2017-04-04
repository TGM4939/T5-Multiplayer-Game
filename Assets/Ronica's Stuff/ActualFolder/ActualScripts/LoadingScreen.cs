using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour 
{
	/*public string[] sceneName = new string[3];
	public string previousScene, nextScene;*/

	public string str;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("loadScreen");
	}

	public IEnumerator loadScreen()
	{
		yield return new WaitForSeconds(5.0f);
		loadNextScreen ();
	}

	public void loadNextScreen()
	{
		SceneManager.LoadScene (str);
	}
}
