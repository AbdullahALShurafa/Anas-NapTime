using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : Characters 
{
	

	//Free Movement: allow protagonist to rotate and move only with forward animation clip in certain directions
	internal void FreeMovement()
	{    
		// when player is in the default movement.
		if (!Player.myPlayer.isInSecondDimension)
		{

			if (Is_keyPressed(KeyCode.W)) 
			{
				if (Is_keyPressed(KeyCode.A)) 
				{
					//Move Player with the right animation
					Run(Vector3.forward, AnimationClip.RunForward);
					//set his direction
					Direction (315);

				} 

				else if (Is_keyPressed(KeyCode.D)) 
				{
					//Move Player with the right animation
					Run(Vector3.forward, AnimationClip.RunRight);
					//set his direction
					Direction (45);
				} 

				else 
				{
					//Move Player with the right animation
					Run(Vector3.forward, AnimationClip.RunForward);
					//set his direction
					    Direction (0);
				}

					Player.myPlayer.DeductStamina(1);    
			}

			else if (Is_keyPressed(KeyCode.S))
			{
				if (Is_keyPressed(KeyCode.A)) 
				{
					//Move Player with the right animation
					Run(Vector3.forward, AnimationClip.RunBackward);
					//set his direction
					    Direction (225);                
				} 
				else if (Is_keyPressed(KeyCode.D)) 
				{
					//Move Player with the right animation
					Run(Vector3.forward, AnimationClip.RunRight);
					//set his direction
					    Direction (135);                
				} 
				else 
				{
					Run(Vector3.forward, AnimationClip.RunBackward);
					    Direction (180);

				}

				Player.myPlayer.DeductStamina(1);    
			}

			else if (Is_keyPressed(KeyCode.A)) 
			{
				Run (Vector3.forward , AnimationClip.RunLeft);
				Direction (-90);

				Player.myPlayer.DeductStamina(1);                
			}

			else if (Is_keyPressed(KeyCode.D))
			{
				Run(Vector3.forward, AnimationClip.RunRight);
				 Direction (90);
				Player.myPlayer.DeductStamina(1);  



			}

			else if(m_hit == 0)
			{
				m_speed = 0;
				Animation (AnimationClip.Idle);
			}
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