using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : Photon.PunBehaviour
{
	public float xDist;
	public float yDist;
	public float zDist;
	private Vector3 CamPos;
	private GameObject Camera;
	public float cameraYLookDisp, cameraXLookDisp;
	private Vector3 cameraLookAt;

	void Start () {
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	// Update is called once per frame
	void Update ()
	{
		if (!photonView.isMine)
		{
			return;
		}
		CamPos = new Vector3(xDist + gameObject.transform.position.x, yDist + gameObject.transform.position.y, zDist + gameObject.transform.position.z);
		cameraLookAt = new Vector3(cameraXLookDisp, cameraYLookDisp, 0) + gameObject.transform.position;
		Camera.transform.position = CamPos;
		Camera.transform.LookAt(cameraLookAt);
	}
}