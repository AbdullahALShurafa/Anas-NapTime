using UnityEngine;
using System.Collections;

public class PlayerBehaviour : PlayerMovement
{
	internal PlayerBehaviour(Animator a_animator, Transform a_transfrom, GameObject a_cam)
	{
		m_animator = a_animator;
		m_transform = a_transfrom;
		m_cam = a_cam;
	
	}

	internal void Behaviour()
	{
		FreeMovement ();
		JumpState ();
		SlideState();
	}


	//Method to make player jump
	void JumpState()
	{
		//if not in the state of jumping
		if (Input.GetKeyDown (KeyCode.Space) && Is_animation("Jump", false)) 
		{
			//Let protagonist jump
			TriggerAnimation ("Jump");
		}
	}

	void SlideState()
	{
		if (Input.GetKeyDown (KeyCode.DownArrow) && Is_animation("Slide" , false))
		{
			TriggerAnimation ("Slide");
		}
	}



}
