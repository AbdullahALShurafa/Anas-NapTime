using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Characters 
{


	/// Player Componenets, -------------------

	PlayerBehaviour m_playerBehaviour;

	public bool isPlayerGrounded = true;
	internal bool isPlayerAlive = true;
	private bool isShieldOn;
	internal bool isInSecondDimension = false;

	private Rigidbody rb;

	public Image flashingHurtImage;


	// Transforms and vector components for player
	internal Vector3 checkPointPos;

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
		myPlayer.enabled = true;

		m_animator = GetComponent<Animator> ();

		m_playerBehaviour = new PlayerBehaviour (m_animator,transform);

		g_CurrentHealth = m_health;

		g_currStamina = g_startingStamina;	

		g_lowStaminaTxt.enabled = false;

		rb = GetComponent<Rigidbody>();

		flashingHurtImage.enabled = false;

	}

	// Update is called once per frame
	void Update ()
	{ 

		JumpState();
		m_playerBehaviour.Behaviour ();

		if (g_totalHealth > 0 )
			Heart_Handler();

		 if (g_totalHealth <= 0)
			Death();


	}
		

	void Death()
	{
		// Player is Dead. 
		isPlayerAlive = false;

//		//Trigger dying animation.
		m_playerBehaviour.TriggerAnimation ("Death");

		// Fixes a bug that affects the UI of the Health.. , Tried to do it automaticly in the if condition but did'nt work therefore it needed a manual fix
		HeartUIContainer.sprite = HeartSprites[0];

			
	}

	void Respawn()
	{
		g_totalHealth--;
		// Respawn only when we have health 
		//TODO: Set player pos to this pos 
		// Activated checkpoint only when our player has total health else his dead and he shouldn't respawn at the checkpoint
		if (g_totalHealth >0) 
			this.transform.position = checkPointPos;

		// Refill his health and remove one of his total healths
		g_CurrentHealth = 5;
			
		g_totalHealthTxt.text = "x" + g_totalHealth; 
	}


	void Heart_Handler()
	{
		// Change the heart sprite depending on health
		HeartUIContainer.sprite = HeartSprites[g_CurrentHealth];
	}

	public int AddHealth()
	{
		// handles index out of range error
		if ( g_CurrentHealth >= 5)
			return 0;
		
		else
			return g_CurrentHealth++;

	}

	public void DeductStamina(int amount)
	{
		// How much stamina are we going to deduct when player is running?
		g_currStamina -= amount;
		// Change the slider value depending on the stamina
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

	public void AddStamina(int a_staminaToAdd)
	{
		g_currStamina += a_staminaToAdd;
		g_lowStaminaTxt.enabled = false;
	}

	IEnumerator ShieldOnBehavior (float a_shieldTime)
	{
		isShieldOn = true;
		// The time that the shield will stay on
		yield return new WaitForSeconds(a_shieldTime);
		isShieldOn = false;
	}

//	IEnumerator TimerPowerUp (float a_TimerDuration)
//	{
//		// Get the timer Bool from the gamemanager class and set it to false so that the timer doesn't run anymore.
//		GameManager.g_gameManager.isTimer = false;
//		// The time that the powerup will stay enabled
//		yield return new WaitForSeconds(a_TimerDuration);
//		// set everything back to default.
//		GameManager.g_gameManager.isTimer = true;
//	}
//

	public IEnumerator FlashHurtImage()
	{
		//Enable the image for the flash effect
		flashingHurtImage.enabled = true;
		yield return new WaitForSeconds(0.2f);
		flashingHurtImage.enabled = false;
		//yield return new WaitForSeconds(0.3f); 
	}

	public IEnumerator PauseTimePowerup(float a_time)
	{
		//Enable the image for the flash effect
		GameManager.g_gameManager.isTimerPaused = true;
		yield return new WaitForSeconds(a_time);
		GameManager.g_gameManager.isTimerPaused = false;
	}

	void FastRunningPowerUp ()
	{
		m_playerBehaviour.m_speed = 50;	
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.CompareTag ("floor"))
		{
			isPlayerGrounded = true;
			// stop the jump state
			m_playerBehaviour.m_animator.SetBool ("jump" ,false);
		//	rb.isKinematic = true;
		}

	}
		
	void OnTriggerEnter(Collider col)
	{
		// When enemies damage our player and our shield powerup is not on
		if (col.gameObject.CompareTag("Enemy") && !isShieldOn && isPlayerAlive)
		{
			// Damage the player and play the flash hurt effect
			g_CurrentHealth--;
			StartCoroutine(FlashHurtImage());

			// when player health is finished respawn him to the last checkpoint 
			if(g_CurrentHealth <=0 && g_totalHealth >0)
				Respawn();
		}

		if ( col.gameObject.CompareTag("coffee"))
		{
			AddStamina(100);
			GameManager.g_gameManager.InstantiateParticle(1);
			Destroy(col.gameObject);
		}

		if ( col.gameObject.CompareTag("shield"))
		{
			StartCoroutine(ShieldOnBehavior(5));
			Destroy(col.gameObject);
		}


		if ( col.gameObject.CompareTag("Heart"))
		{
			AddHealth();
			//TODO: play heart VFX here
			GameManager.g_gameManager.InstantiateParticle(0);
			Destroy(col.gameObject);

		}
		if ( col.gameObject.CompareTag("Arrow"))
		{
			FastRunningPowerUp();
			Destroy(col.gameObject);
		}

		if ( col.gameObject.CompareTag("clock"))
		{
			StartCoroutine(PauseTimePowerup(5));
			GameManager.g_gameManager.InstantiateParticle(2);
			Destroy(col.gameObject);
		}

		

	}
		

	void JumpState()
	{
			//if not in the state of jumping
		if (Input.GetKeyDown (KeyCode.Space) 
			&& m_playerBehaviour.Is_animation("Jump", false) 
			&& isPlayerGrounded 
			&& isPlayerAlive) 

			{
					//Let player jump
			//		m_playerBehaviour.BoolAnimation ("jump");
					m_animator.SetBool("jump",true);
					//Jump
					rb.isKinematic = false;
					transform.gameObject.GetComponent<Rigidbody>().AddForce (m_playerBehaviour.jumpVelocity, ForceMode.VelocityChange);
					isPlayerGrounded = false;
			}
	}



	//Events for certain clips..
	//while in the jump state
//	void EventOfJump()
//	{
//
//	}

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
//	void DisableJumpTrigger()
//	{
//		m_playerBehaviour.m_animator.SetBool ("Jump", false);
//	}

	void DisableSlideTrigger()
	{
		m_playerBehaviour.m_animator.SetBool("Slide", false);
	}

}