using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Characters 
{


	/// Player Componenets, -------------------

	PlayerMovementBehavior m_playerBehaviour;

	public bool isPlayerGrounded = true;
	internal bool isPlayerAlive = true;
	internal bool isShieldOn;
	internal bool isInSecondDimension = false;
	internal bool isAttackingOrSliding = false;

	public AudioClip[] SFXsounds;

	private Rigidbody rb;

	public Image flashingHurtImage;
	public GameObject DeathFader;

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

		m_playerBehaviour = new PlayerMovementBehavior (m_animator,transform);

		g_CurrentHealth = m_health;

		g_currStamina = g_startingStamina;	

		g_lowStaminaTxt.enabled = false;

		rb = GetComponent<Rigidbody>();

		flashingHurtImage.enabled = false;



	}



	// Update is called once per frame
	void Update ()
	{ 

		//JumpState();
		//AttackState();
		//SlideState();
		Behavior();
		m_playerBehaviour.Behaviour ();

		if (g_totalHealth > 0 )
			Heart_Handler();

		 if (g_totalHealth <= 0)
			Death();


	}

	public void playAudio(int clipNumber)
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = SFXsounds[clipNumber];
		audio.Play ();

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

	internal void Respawn()
	{
		//	 remove one of his total healths
		g_totalHealth--;
		// Respawn only when we have totalhealth 
		//TODO: Set player pos to this pos 
		// Activated checkpoint only when our player has total health else his dead and he shouldn't respawn at the checkpoint
		if (g_totalHealth >0) 
		{
			StartCoroutine(FaderOfDeath(2));
		
			this.transform.position = checkPointPos;
		}

		// Refill his health 
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
		// only deduct stamina will player is moving on ground
		if (isPlayerGrounded)
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

	}

	public void AddStamina(int a_staminaToAdd)
	{
		// Add stamina when we take coffee.
		g_currStamina += a_staminaToAdd;
		// Disable the "need stamina: text
		g_lowStaminaTxt.enabled = false;
	}

	IEnumerator ShieldOnBehavior (float a_shieldTime)
	{
		isShieldOn = true;
		// The time that the shield will stay on
		yield return new WaitForSeconds(a_shieldTime);
		isShieldOn = false;
	}


	IEnumerator FaderOfDeath (float a_FadeTime)
	{
		DeathFader.SetActive (true);
		yield return new WaitForSeconds(a_FadeTime);
		DeathFader.SetActive (false);

	}

	void Behavior()
	{
		// Attack 
		if (Input.GetKeyDown (KeyCode.LeftShift) && Is_animation("Attack" , false) && isPlayerGrounded)
		{
			TriggerAnimation ("Attack");
			isAttackingOrSliding = true;
		}

		// Slide
		if (Input.GetKeyDown (KeyCode.DownArrow) && Is_animation("Slide" , false) && isPlayerGrounded)
		{
			TriggerAnimation ("Slide");
			isAttackingOrSliding = true;
			transform.gameObject.GetComponent<Rigidbody>().AddForce (m_playerBehaviour.jumpVelocity, ForceMode.Acceleration);


		}

		//Jump
		//if not in the state of jumping
		if (Input.GetKeyDown (KeyCode.Space) 
			&& m_playerBehaviour.Is_animation("Jump", false) 
			&& isPlayerGrounded 
			&& isPlayerAlive
			&& !isAttackingOrSliding ) 

		{

			//Let player jump
			m_animator.SetBool("jump",true);

			//Jump
			//rb.isKinematic = false;
			isPlayerGrounded = false;

		}
	}
		

	public IEnumerator FlashHurtImage()
	{
		//Enable the image for the flash effect
		flashingHurtImage.enabled = true;
		yield return new WaitForSeconds(0.2f);
		flashingHurtImage.enabled = false;
	}

	public IEnumerator PauseTimePowerup(float a_time)
	{
		//Enable the bool for the time powerup
		GameManager.g_gameManager.isTimerPaused = true;
		// How much the time is going to freez for?
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
			Debug.Log("floor");
			m_playerBehaviour.m_animator.SetBool ("jump" ,false);
		//	rb.isKinematic = true;
		}

	}
		
	void OnTriggerEnter(Collider col)
	{
		// When enemies damage our player and our shield powerup is not on
		if (col.gameObject.CompareTag("Enemy")
			&& !isShieldOn
			&& isPlayerAlive
			&& !isAttackingOrSliding)
		{
			// Damage the player and play the flash hurt effect
			g_CurrentHealth--;
			playAudio(4);
			StartCoroutine(FlashHurtImage());

			// when player health is finished respawn him to the last checkpoint 
			if(g_CurrentHealth <=0 && g_totalHealth >0)
				Respawn();
		}

		if ( col.gameObject.CompareTag("coffee"))
		{
			AddStamina(400);
			playAudio(3);
			GameManager.g_gameManager.InstantiateParticle(1);
			Destroy(col.gameObject);
		}

		if ( col.gameObject.CompareTag("shield"))
		{
			StartCoroutine(ShieldOnBehavior(5));
			playAudio(3);

			Destroy(col.gameObject);
		}


		if ( col.gameObject.CompareTag("Heart"))
		{
			AddHealth();
			playAudio(3);

			//TODO: play heart VFX here
			GameManager.g_gameManager.InstantiateParticle(0);
			Destroy(col.gameObject);

		}
		if ( col.gameObject.CompareTag("Arrow"))
		{
			playAudio(3);

			m_playerBehaviour.m_speed = 50;
			//FastRunningPowerUp();
			//Destroy(col.gameObject);
		}

		if ( col.gameObject.CompareTag("clock"))
		{
			StartCoroutine(PauseTimePowerup(5));
			playAudio(3);

			GameManager.g_gameManager.InstantiateParticle(2);
			Destroy(col.gameObject);
		}

		if ( col.gameObject.CompareTag("endLevel"))
		{
			GameManager.g_gameManager.EndOfLevel();
		}
		

	}
		





	//Events for certain clips..
	//This is being called inside certain animation clips, to make player move. 
	void ActionMove(float a_speed)
	{
		m_playerBehaviour.m_speed = a_speed;
	}

	void JumpForceEvent()
	{
		transform.gameObject.GetComponent<Rigidbody>().AddForce (m_playerBehaviour.jumpVelocity, ForceMode.VelocityChange);

	}
		
	//Events Disables
	//Following logic will disable certain parameter

	void DisableAttackOrSlideTrigger()
	{
		isAttackingOrSliding = false;
		Debug.Log (isAttackingOrSliding);
	}

}