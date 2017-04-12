using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorSc : MonoBehaviour {

	[SerializeField] private GameObject aiPref;
	private GameObject aiInstance;

	// Use this for initialization
	void Start () {
		aiInstance = GameObject.Instantiate(aiPref, transform.position,transform.rotation);
	}

	// Update is called once per frame
	void Update () {

		//float Timer = Random.Range(0f, 3f);
		if (aiInstance == null)
		{
			//aiInstance = GameObject.Instantiate(aiPref, transform.position, transform.rotation);
			//Invoke("Instantiation", Timer);
			Instantiation();
		}
		//if (aiPref == null)
		//{
		//    Instantiate(aiPref) as GameObject
		//}

	}
	private void Instantiation()
	{
		aiInstance = GameObject.Instantiate(aiPref, transform.position, transform.rotation);
	}
}