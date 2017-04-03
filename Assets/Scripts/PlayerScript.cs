using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScript : Photon.PunBehaviour {
	
	public Animator PlayerAnimator;
	public Image HealthBar;
	public float Speed, Sensitivity, Health, Damage, MaxHealth;
	private GameObject Camera;
	public GameObject Fist;
	public SphereCollider FistSphereCollider;

	// Use this for initialization
	void Start () 
	{
		MaxHealth = 10;
		Health = MaxHealth;
		Damage = 1;
		Camera = GameObject.FindGameObjectWithTag ("MainCamera");
		FistSphereCollider.enabled = false;
		Fist.GetComponent<DamageClass> ().Damage = this.Damage;
	}

	// Update is called once per frame
	void Update () 
	{
		HealthBar.fillAmount = Health / MaxHealth;
		if (!photonView.isMine) 
		{
			return;
		}

		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			PlayerAnimator.SetBool ("IsWalking", true);
			transform.Translate (Vector3.forward * Speed * Time.smoothDeltaTime);
		}
		else 
		{
			PlayerAnimator.SetBool ("IsWalking", false);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.Rotate (Vector3.down * Sensitivity);
		}
		else if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.Rotate (Vector3.up * Sensitivity);
		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			PlayerAnimator.SetTrigger ("Punch");
		}

		foreach (Canvas canvas in FindObjectsOfType<Canvas>()) 
		{
			if (canvas.gameObject.name == "HealthBarCanvas") 
			{
				canvas.transform.LookAt (Camera.transform.position);
			}
		}
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			stream.SendNext (this.Health);
		}
		else 
		{
			this.Health = (float)stream.ReceiveNext ();
		}
	}
		
	void OnTriggerEnter(Collider Col)
	{
		if (!photonView.isMine) 
		{
			return;
		}
		if (Col.gameObject.tag == "Fist") 
		{
			Health -= Col.gameObject.GetComponent<DamageClass> ().Damage;
			ApplyDamage (Damage);
		}
	}

	public void ColliderChanger(float Value)
	{
		if (Value == 0)
		{
			FistSphereCollider.enabled = false;
		}
		if (Value == 1)
		{
			FistSphereCollider.enabled = true;
		}
	}

	[PunRPC]

	public void ApplyDamage(float DamageAmount)
	{
		if (photonView.isMine) 
		{
			this.photonView.RPC ("ApplyDamage", PhotonTargets.OthersBuffered, new object[] { DamageAmount });
		}
	}
}
