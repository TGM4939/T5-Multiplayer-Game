using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour 
{
	public string str;

	// Use this for initialization
	void Start () 
	{
		//StartCoroutine ("loadScreen");
	}

	public IEnumerator loadScreen()
	{
		yield return new WaitForSeconds(5.0f);
		loadNextScreen (str);
	}

	public void loadNextScreen(string a_scene)
	{
		SceneManager.LoadScene(a_scene);
	}
}
