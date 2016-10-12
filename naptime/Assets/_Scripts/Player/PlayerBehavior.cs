using UnityEngine;
using System.Collections;

public class PlayerBehaviour : PlayerMovement
{
	internal PlayerBehaviour(Animator a_animator, Transform a_transfrom)//, GameObject a_cam)
	{
		m_animator = a_animator;
		m_transform = a_transfrom;
	//	m_cam = a_cam;
	
	}

	internal void Behaviour()
	{
		
		FreeMovement ();
		SecondDimentionMovement();
	//	JumpState ();
		SlideState();
	}


	//Method to make player jump
//	void JumpState()
//	{
//		//if not in the state of jumping
//		if (Input.GetKeyDown (KeyCode.Space) && Is_animation("Jump", false)) 
//		{
//			//Let player jump
//			TriggerAnimation ("Jump");
//		}
//	}

	/// <summary>
	/// slide State. Will work if the player pressed the correct keys and if the slide animation is not being used 
	/// also when the player is only on the ground and not in the air or preforming the jump animation.
	/// </summary>
	void SlideState()
	{
		
		if (Input.GetKeyDown (KeyCode.DownArrow) && Is_animation("Slide" , false) && Player.myPlayer.isPlayerGrounded == true)
		{
			TriggerAnimation ("Slide");
		}
	}



}
