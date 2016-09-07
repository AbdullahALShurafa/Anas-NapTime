using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	public GameObject m_cam;

	private Animator m_animator;

	PlayerBehaviour m_playerBehaviour;

	public Transform m_startPoisition;

	//UI Componenets-------------
	public Text g_lowStaminaTxt;
	//-------------------------

	// Health componenets------

	public int m_health = 5;
	public int g_CurrentHealth;

	public Sprite[] HeartSprites;

	public Image HeartUIContainer;
	//-------------------------

	// Singelton
	public static Player myPlayer;

	// Stamina Coponenets----
	public int g_startingStamina = 500;
	public int g_currStamina;
	public Slider g_StaminaSlider;
	public bool IsStaminaEmpty = false;
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

	}

	// Update is called once per frame
	void Update ()
	{ 
		m_playerBehaviour.Behaviour ();
		Heart_Handler();

	}
		

	void Death()
	{
		//Is player out of health
		if (m_health <= 0) 
		{
			//Trigger dying animation.
			m_playerBehaviour.Animation(AnimationClip.Die);

			//TODO: Pop up option menu to restart or go to menu
		}
	}

	void Respawn()
	{
		m_playerBehaviour.Animation (AnimationClip.Idle);
		// Change this to last checkpoint
		transform.position = m_startPoisition.position;
		//m_health = 5;
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
		if (col.gameObject.CompareTag("Enemy"))
			{
				g_CurrentHealth--;
			}
	}

	void OnTriggerEnter(Collider col)
	{
		if ( col.gameObject.CompareTag("Coin"))
		{
			AddStamina(100);
			Destroy(col.gameObject);
		}
	}
		

	//______________________________
	//Events for certain clips..

	//while in the jump state
	void EventOfJump()
	{
		//Jump
		transform.gameObject.GetComponent<Rigidbody>().AddForce (m_playerBehaviour.jumpVelocity, ForceMode.VelocityChange);

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