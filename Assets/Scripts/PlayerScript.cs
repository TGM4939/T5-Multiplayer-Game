using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScript : Photon.PunBehaviour {

	public Animator PlayerAnimator;
	public Image HealthBar;
	public float Speed, Sensitivity, Health, Damage, MaxHealth, Force, maxForce, maxSpeed;
	private GameObject Camera;
	public GameObject Fist;
	public SphereCollider FistSphereCollider;
	public bool frozen;
	public float h, v;
	private Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		MaxHealth = 10;
		Health = MaxHealth;
		Damage = 1;
		Camera = GameObject.FindGameObjectWithTag ("MainCamera");
		FistSphereCollider.enabled = false;
		Fist.GetComponent<DamageClass> ().Damage = this.Damage;
		rb = gameObject.GetComponent<Rigidbody>();
		frozen = false;
	}

	private void Awake()
	{
		h = PlayerAnimator.GetFloat("xPos");
		v = PlayerAnimator.GetFloat("zPos");
	}

	// Update is called once per frame
	void Update () 
	{
		HealthBar.fillAmount = Health / MaxHealth;	
		/*foreach (Canvas canvas in FindObjectsOfType<Canvas>()) 
		{
			if (canvas.gameObject.name == "HealthBarCanvas") 
			{
				canvas.transform.LookAt (Camera.transform.position);
			}
		}*/
	}
	private void FixedUpdate()
	{
		if (!photonView.isMine)
		{
			return;
		}
		//controls();
		if(frozen == false)
		{
			MovementControls2();
			PlayerAnimator.SetFloat("xPos", h);
			PlayerAnimator.SetFloat("zPos", v);
			AnimationPlayer();
		}
		else
		{
			PlayerAnimator.SetBool("isWalking", false);
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			Invoke("Unfreeze", 1.5f);
		}

		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		transform.LookAt(transform.position + rb.velocity);

	}

	private void Unfreeze()
	{
		frozen = false;
	}

	private void AnimationPlayer()
	{
		if (h != 0 || v != 0)
		{
			PlayerAnimator.SetBool("isWalking", true);
			//tempDirection = rb.velocity;
		}
		if (h == 0f && v == 0f)
		{
			PlayerAnimator.SetBool("isWalking", false);
		}
		/*if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.X))
		{
			PlayerAnimator.SetBool("isHitting", true);
			//anim.SetFloat("idleMult", 50f);
			//anim.SetFloat("runMult", 50f);
		}
		if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.X))
		{
			PlayerAnimator.SetBool("isHitting", false);
			//anim.SetFloat("runMult", 1f);
			//anim.SetFloat("idleMult", 1f);
		}*/
	}

	private void MovementControls2()
	{

		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		if (h > 0f)
		{
			rb.AddForce(Vector3.right * Force);
		}
		if (h < 0f)
		{
			rb.AddForce(Vector3.left * Force);
		}

		if (v > 0f)
		{
			rb.AddForce(Vector3.forward * Force);
		}
		if (v < 0f)
		{
			rb.AddForce(Vector3.back * Force);
		}

		if (h == 0f && v == 0f)
		{
			//Vector3 tempAngular = rb.angularVelocity;
			//Quaternion tempRotation = transform.rotation;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			//tempDirection = rb.velocity;
			//transform.rotation = tempRotation;
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.photonView.RPC("Punch", PhotonTargets.All);
		}
	}
	private void controls()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			PlayerAnimator.SetBool("IsWalking", true);
			transform.Translate(Vector3.forward * Speed * Time.smoothDeltaTime);
		}
		else
		{
			PlayerAnimator.SetBool("IsWalking", false);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(Vector3.down * Sensitivity);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(Vector3.up * Sensitivity);
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			this.photonView.RPC("Punch", PhotonTargets.All);
		}
	}

	[PunRPC]
	void Punch()
	{
		PlayerAnimator.SetTrigger ("Punch");
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