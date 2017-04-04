using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	private GameObject optionsPanel, creditsPanel, helpPanel;

	void Awake () 
	{
		optionsPanel = GameObject.Find("OptionsPanel");
		creditsPanel = GameObject.Find("CreditsPanel");
		helpPanel = GameObject.Find("HelpPanel");

		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		helpPanel.SetActive(false);


	}

	public void Options()
	{
		creditsPanel.SetActive(false);
		helpPanel.SetActive(false);
		optionsPanel.SetActive(true);


	}

	public void Credits()
	{
		optionsPanel.SetActive(false);
		helpPanel.SetActive(false);
		creditsPanel.SetActive(true);
	}

	public void Help()
	{
		optionsPanel.SetActive(false);
		creditsPanel.SetActive(false);
		helpPanel.SetActive(true);

	}

	public void PlayGame(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

		#else
		Application.Quit();

		#endif
	}

}
