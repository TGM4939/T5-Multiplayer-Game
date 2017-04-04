using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	private Animator m_animator;

	//private GameObject optionsPanel, creditsPanel, helpPanel;

	void Awake () 
	{
		m_animator = GetComponent<Animator>();

	//	optionsPanel = GameObject.Find("OptionsPanel");
	//	creditsPanel = GameObject.Find("CreditsPanel");
	//	helpPanel = GameObject.Find("HelpPanel");

	//	optionsPanel.SetActive(false);
	//	creditsPanel.SetActive(false);
	//	helpPanel.SetActive(false);


	}

	//public void Options()
	//{
	//	creditsPanel.SetActive(false);
	//	helpPanel.SetActive(false);
	//	optionsPanel.SetActive(true);


	//}

	//public void Credits()
	//{
	//	optionsPanel.SetActive(false);
	//	helpPanel.SetActive(false);
	//	creditsPanel.SetActive(true);
	//}

	//public void Help()
	//{
	//	optionsPanel.SetActive(false);
	//	creditsPanel.SetActive(false);
	//	helpPanel.SetActive(true);

	//}

	public void PlayGame(string sceneName)
	{
		m_animator.SetBool("Scene", false);
		StartCoroutine(NextScene(sceneName));
	}

	private IEnumerator NextScene(string a_scene)
	{
		yield return new WaitForSeconds(5);

		SceneManager.LoadScene(a_scene);
	}

	public void Quit()
	{
		m_animator.SetBool("Scene", false);

		StartCoroutine(QuitApplication());

	}

	private IEnumerator QuitApplication()
	{
		yield return new WaitForSeconds(5);

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

		#else
		Application.Quit();

		#endif
	}

}
