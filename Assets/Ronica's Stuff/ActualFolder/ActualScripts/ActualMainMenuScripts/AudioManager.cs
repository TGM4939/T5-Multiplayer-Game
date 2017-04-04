using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour 
{

	public Slider musicSlider;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		audioSource.Play ();
		musicSlider.value = 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float volumeValue = musicSlider.value;
		audioSource.volume = volumeValue;

		DontDestroyOnLoad (audioSource);
	}
}
