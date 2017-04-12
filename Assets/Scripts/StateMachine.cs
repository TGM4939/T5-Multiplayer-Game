using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AgentState
{
	Arrive,
	Seek,
	Attack
};

public class StateMachine : MonoBehaviour 
{

	//access to the class having the agent behaviours
	private Steerings steeringClass; 

	private AgentState agentState;
	private GameObject agent;
	private GameObject player;
	private Rigidbody myRigidbody, playerRigidbody;

	private Animator anim;

	private void Awake()
	{
		agent = this.gameObject;
		player = GameObject.FindGameObjectWithTag("Player");
		myRigidbody = gameObject.GetComponent<Rigidbody>();
		playerRigidbody = player.GetComponent<Rigidbody>();


		steeringClass = GetComponent<Steerings>();

		anim = gameObject.GetComponent<Animator>();
	}

	void Start ()
	{


		//setting the default agent state to seeking
		//SetAgentState(AgentState.Seek);
	}

	void Update()
	{
		//setting the Agent State based on distnce
		if (steeringClass.distance <= 0.8f)
		{
			anim.SetBool("IsWalking", false);
			SetAgentState(AgentState.Attack);
			return;
		}

		if (steeringClass.MOV == true)
		{
			SetAgentState(AgentState.Arrive);
		}
		else
		{
			SetAgentState(AgentState.Seek);
		}


		if (myRigidbody.velocity.z != 0f || myRigidbody.velocity.x != 0f)
		{
			anim.SetBool("IsWalking", true);
		}

		//if (steeringClass.distance <= 0.7f)
		//{
		//    anim.SetBool("isWalking", false);
		//    SetAgentState(AgentState.Attack);
		//}
	}


	void FixedUpdate()
	{
		//steeringClass.ObstacleAvoidance();
		//transform.LookAt(transform.position + myRigidbody.velocity);
		//steeringClass.Distance();
		/*
        if (steeringClass.MOV == true)
        {
            SetAgentState(AgentState.Arrive);
        }

        else
        {
            SetAgentState(AgentState.Seek);
        }
        */
		steeringClass.ObstacleAvoidance();

		switch (agentState)
		{
		case AgentState.Arrive:
			{
				//if (steeringClass.MOV == true)
				//{
				//Debug.Log(steeringClass.MOV);
				steeringClass.Arrive();
				//SetAgentState(AgentState.Arrive);
				Debug.Log("<color=red>AgentState is ARRIVE</color>");
				//}
			}
			break;

		case AgentState.Seek:
			{
				//if (steeringClass.MOV == false)
				//{
				/*
					steeringClass.Seek();
					Debug.Log("<color = blue>AgentState is Seek</color>");
					*/

				steeringClass.predictedVel();
				//SetAgentState(AgentState.Seek);
				Debug.Log("<color=blue>AgentState is PREDICTED VELOCITY</color>");
				//}

			}
			break;
		case AgentState.Attack:
			{
				//if (steeringClass.MOV == true)
				//{
				//Debug.Log(steeringClass.MOV);
				anim.SetTrigger("Punch");
				steeringClass.Attacking1();
				// myRigidbody.velocity = 
				//steeringClass.Arrive();
				//SetAgentState(AgentState.Arrive);
				//Debug.Log("<color=red>AgentState is ARRIVE</color>");
				//}
			}
			break;
		}
		steeringClass.ObstacleAvoidance();
		transform.LookAt(transform.position + myRigidbody.velocity);
	}
	/*
    private void LateUpdate()
    {
        steeringClass.ObstacleAvoidance();
        transform.LookAt(transform.position + myRigidbody.velocity);
    }
    */


	void SetAgentState(AgentState state)
	{
		agentState = state;

		//debugging
		switch (state)
		{
		case AgentState.Arrive:
			//Debug.Log("AgentState = Arrive");
			break;

		case AgentState.Seek:
			//Debug.Log("AgentState = Seek");
			break;
		case AgentState.Attack:
			break;
		}

	}
}