using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	public int rotationSpeed;

	// Update is called once per frame
	void Update () 
	{
		// To add rotation to the player
		transform.Rotate (new Vector3 (0,rotationSpeed,0) * Time.deltaTime);
	}
}
