using UnityEngine;
using System.Collections.Generic;

public class PlayerMovementBehavior : Characters
{
	Player player;
	float angle;


	internal PlayerMovementBehavior(Animator a_animator, Transform a_transfrom)
	{
		m_animator = a_animator;
		m_transform = a_transfrom;
	}



	internal void Behaviour()
	{

		FreeMovement ();
		SecondDimentionMovement();

	}

	//Free Movement: allow protagonist to rotate and move only with forward animation clip in certain directions
	internal void FreeMovement()
	{    
		if(!player)
		{
			player = FindObjectOfType<Player>();

			if(!player)
			{
				Debug.LogError("Couldn't find player");
			}
		}


		if (!Player.myPlayer.isInSecondDimension)
		{
			angle = player.transform.rotation.eulerAngles.y;

			// Forward movement
			if (Is_keyPressed(KeyCode.W))
			{
				Run(Vector3.forward, AnimationClip.RunForward);
				Direction(angle);
				Player.myPlayer.DeductStamina(1);

				RotationKeys();
			}

			// Backward movement
			else if(Is_keyPressed(KeyCode.S))	
			{
				Run(Vector3.back, AnimationClip.RunBackward);
				Direction(angle);
				Player.myPlayer.DeductStamina(1);

				// Handle turns while going backwards
				RotationKeys();
			}

			// No forward or back
			else	
			{
				RotationKeys();
				Animation (AnimationClip.Idle);

			}
		}
	}


	void RotationKeys()
	{
		// Handle turns while going backwards
		if (Is_keyPressed(KeyCode.A))
		{
			Direction(angle - 180 * Time.deltaTime);

		}

		if (Is_keyPressed(KeyCode.D))
		{
			Direction(angle + 180 * Time.deltaTime);
		}
	}

	/// <summary>
	/// THe player movement when he get"s to the 2.5D gameplay. THe player stamina & Time will not run in this world.
	/// He can only movr Left & right , jump & slide. He will collect special powerup's and discover new area.s 
	/// </summary>
	internal void SecondDimentionMovement()
	{
		if (Player.myPlayer.isInSecondDimension)
		{
			if (Is_keyPressed(KeyCode.A)) 
			{
				//Move Player with the right animation
				Run(Vector3.forward, AnimationClip.RunLeft);
				//set his direction
				Direction (-90);
			}

			else if (Is_keyPressed(KeyCode.D))
			{
				Run (Vector3.forward , AnimationClip.RunRight);
				Direction (90);
			}


			else if(m_hit == 0)
			{
				m_speed = 0;
				Animation (AnimationClip.Idle);
			}
		}
	}
			
}
