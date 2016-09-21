﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{


	/// Player Componenets, -------------------
	public GameObject m_cam;
	private Animator m_animator;
	PlayerBehaviour m_playerBehaviour;
	internal bool isPlayerGrounded = true;
	private Rigidbody rb;
	// Transforms and vector components for player
	public Transform m_startPoisition;
	internal Vector3 checkPointPos;
	//---------------------------------------

	//UI Componenets-------------
	//-------------------------


	// Health componenets------
	/// <summary>
	/// Player health is 5 lives and total  health is 2 therefore whenever the player loses his 5 lives he will spawn at the checkpoint and total health get's to 1. if the player 
	/// loses his hearts again he will lose the whole level. For example : player get's hit 5 times he respawns at the last checkpoint and his total health is x1 now.
	/// This logic is done in order to use both checkpoints and game over mechanics in the game therefore the player total health is 10 not 5 because he has 2 chances before the game
	/// ends
	/// </summary>
	// The hearts health which is 5
	public int m_health = 5;
	public int g_CurrentHealth;

	// The total health which is x2
	public int g_totalHealth = 2;
	public Text g_totalHealthTxt;
	public Sprite[] HeartSprites;

	public Image HeartUIContainer;
	//-------------------------

	// Singelton
	public static Player myPlayer;

	// Stamina Coponenets----
	public int g_startingStamina = 500;
	public int g_currStamina;
	public Text g_lowStaminaTxt;
	public Slider g_StaminaSlider;
	internal bool IsStaminaEmpty = false;
	//------------------


	void Awake()
	{
		myPlayer = this;
	}

	void Start () 
	{
		m_animator = GetComponent<Animator> ();
		m_playerBehaviour = new PlayerBehaviour (m_animator,transform,m_cam);

		g_CurrentHealth = m_health;

		g_currStamina = g_startingStamina;	

		g_lowStaminaTxt.enabled = false;

		rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update ()
	{ 
		m_playerBehaviour.Behaviour ();
		Heart_Handler();

		// when total health is x0 pop up the game over canvas.
		 if ( g_totalHealth <=0)
			Death();
		

	}
		

	void Death()
	{
//			//Trigger dying animation.
//			m_playerBehaviour.Animation(AnimationClip.Die);
//
//			//TODO: Pop up option menu to restart or go to menu
	}

	void Respawn()
	{
		//TODO: Set player pos to this pos 
		this.transform.position = checkPointPos;

		// Refill his health and remove one of his total healths
		g_CurrentHealth = 5;
		g_totalHealth--;
		g_totalHealthTxt.text = "x" + g_totalHealth; 

	}


	void Heart_Handler()
	{
		// Change the heart sprite depending on health
		HeartUIContainer.sprite = HeartSprites[g_CurrentHealth];

	}

	public int AddHealth()
	{
		return g_CurrentHealth++;

	}

	public void DeductStamina(int amount)
	{
		g_currStamina -= amount;
		g_StaminaSlider.value = g_currStamina;

		if (g_currStamina <= 0 )
		{
			//TODO : Go slower
			m_playerBehaviour.m_speed = 5;
			g_currStamina = 0;
			g_lowStaminaTxt.enabled = true;
			IsStaminaEmpty = true;

		}

		else
			g_lowStaminaTxt.enabled = false;

	}

	 void AddStamina(int a_staminaToAdd)
	{
		g_currStamina += a_staminaToAdd;
		g_lowStaminaTxt.enabled = false;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.CompareTag ("floor"))
		{
			isPlayerGrounded = true;
			//Debug.Log( "should be true" + isPlayerGrounded);
			rb.isKinematic = true;


		}
	}



	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			g_CurrentHealth--;

			if(g_CurrentHealth <=0 && g_totalHealth >0)
				Respawn();
		}
		if ( col.gameObject.CompareTag("Coin"))
		{
			AddStamina(100);
			Destroy(col.gameObject);
		}
	}
		

	//Events for certain clips..
	//while in the jump state
	void EventOfJump()
	{
		
		//Jump
		rb.isKinematic = false;
		transform.gameObject.GetComponent<Rigidbody>().AddForce (m_playerBehaviour.jumpVelocity, ForceMode.VelocityChange);
		isPlayerGrounded = false;

	}

	void EventOfSlide()
	{
			m_playerBehaviour.m_animator.SetBool("Slide", true);
	}

	//This is being called inside certain animation clips, to make player move. 
	void ActionMove(float a_speed)
	{
		m_playerBehaviour.m_speed = a_speed;
	}
		
	//Events Disables
	//Following logic will disable certain parameter
	void DisableJumpTrigger()
	{
		m_playerBehaviour.m_animator.SetBool ("Jump", false);
	}

	void DisableSlideTrigger()
	{
		m_playerBehaviour.m_animator.SetBool("Slide", false);
	}




}