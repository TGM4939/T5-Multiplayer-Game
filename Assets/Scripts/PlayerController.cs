using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	// Animator Variable
	public Animator anim;
    private Rigidbody rb;
    [SerializeField] private float maxSpeed, Force, MaxForce, stopForce, RightRotationSpeed, LeftRotationSpeed;
    private bool stoppingBool, isGrounded;
    [SerializeField] public float h, v;
    private bool jumping, walking, punching;
    private Vector3 tempDirection;
    public bool frozen;

    // Use this for initialization
    void Start () 
	{
        Vector3 zeroVel;
        zeroVel = new Vector3(0f, 0f, 0f);
        //jumping = anim.GetBool("isJumping");
        //walking = anim.GetBool("isWalking");
        //punching = anim.GetBool("isHitting");

        anim.SetFloat("idleMult", 1f);
        anim.SetFloat("runMult", 1f);
        frozen = false;
    }

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        h = anim.GetFloat("xPos");
        v = anim.GetFloat("zPos");
    }

    // Update is called once per frame
    void Update () 
	{
        //Mathf.Clamp(h,-5f,5f);
        //Mathf.Clamp(v,-5f,5f);
        /*
        Debug.Log(rb.velocity.magnitude);
        if(rb.velocity.magnitude > 3f)
        {
            stoppingBool = true;
        }
        else
        {
            stoppingBool = false;
        }
        */
	}

    private void FixedUpdate()
    {
        //MovementControls();
        if(frozen == false)
        {
            MovementControls2();
            anim.SetFloat("xPos", h);
            anim.SetFloat("zPos", v);
            AnimationPlayer();
        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isHitting", false);
            anim.SetBool("isWalking", false);
            rb.velocity = Vector3.zero;
            Invoke("Unfreeze", 1f);
        }


        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        transform.LookAt(transform.position + rb.velocity);
        //transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.LookRotation(tempDirection), 1f);
    }

    private void Unfreeze()
    {
        frozen = false;
    }

    private void LateUpdate()
    {
        //gameObject.transform.rotation.x
    }
    private void AnimationPlayer()
    {

        //walking = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        // For Walk State
        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        //{
        //    anim.SetBool("isWalking", true);
        //    //anim.SetFloat("idleMult", 50f);
        //}
        //if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        //{
        //    anim.SetBool("isWalking", false);
        //    //anim.SetFloat("idleMult", 1f);
        //    //anim.SetFloat("runMult", 50f);
        //}

        if(h != 0 || v != 0)
        {
            anim.SetBool("isWalking", true);
            //tempDirection = rb.velocity;
        }
        if(h == 0f && v == 0f)
        {
            anim.SetBool("isWalking", false);
        }

        // For Jump State
        if (Input.GetKey(KeyCode.Space))
        //else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
            //anim.SetFloat("runMult", 50f);
            //anim.SetFloat("idleMult", 50f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isJumping", false);
            //anim.SetFloat("runMult", 1f);
            //anim.SetFloat("idleMult", 1f);
        }

        // For Hit State
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.X))
        {
            anim.SetBool("isHitting", true);
            //anim.SetFloat("idleMult", 50f);
            //anim.SetFloat("runMult", 50f);
        }
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.X))
        {
            anim.SetBool("isHitting", false);
            //anim.SetFloat("runMult", 1f);
            //anim.SetFloat("idleMult", 1f);
        }
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
    }

    private void MovementControls()
    {
        Vector3 zeroVel;
        zeroVel = new Vector3(0, 0, 0);

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

            rb.AddForce(transform.forward * Force);
           // rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            Debug.Log("Move");
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = zeroVel;
            // rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            Debug.Log("Stop");
        }


        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * Force);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            //rb.AddForce(Vector3.back * Force);
            rb.velocity = zeroVel;
        }

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, RightRotationSpeed, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, LeftRotationSpeed, 0f);
        }

        /*
        else
        {
            if (rb.velocity.magnitude > 0.5f)
            {
                rb.AddForce(-rb.velocity.normalized * stopForce);
                Debug.Log("Stoping");
            }
            else
            {
                if(stoppingBool == false)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    Debug.Log("Stoping");
                }
            }
        }

        */

        /*
        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (rb.velocity.magnitude > 3))
            {
            rb.AddForce(-rb.velocity.normalized * stopForce);
            Debug.Log("Stoping");
            
            }
        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (rb.velocity.magnitude < 3) && stoppingBool == false)
        {
            rb.velocity = new Vector3(0, 0, 0);
            Debug.Log("Stoping");
        }
        */
    }
}
