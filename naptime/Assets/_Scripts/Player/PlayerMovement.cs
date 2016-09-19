using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : Protagonist 
{



	//Free Movement: allow protagonist to rotate and move only with forward animation clip in certain directions
	internal void FreeMovement()
	{    
		if (Is_keyPressed(KeyCode.W)) 
		{

			//Move Player with the right animation
			Run(Vector3.forward, AnimationClip.RunForward);
			//set his direction
			Direction (0);

			// Deduct his stamina.
			Player.myPlayer.DeductStamina(1);
		}

		else if (Is_keyPressed(KeyCode.S))
		{

			Run (Vector3.forward , AnimationClip.RunBackward);
			Direction (180);
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
			Run (Vector3.forward , AnimationClip.RunRight);
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