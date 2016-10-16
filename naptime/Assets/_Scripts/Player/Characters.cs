using UnityEngine;
using System.Collections.Generic;

public enum AnimationClip
{
	Idle = -1,
	RunForward = 2,
	RunBackward = 1,
	RunRight = 3,
	RunLeft = 4,
	Die = 8,
};



public class Characters : MonoBehaviour
{
	internal Animator m_animator;
	internal Transform m_transform;

	internal float m_speed ;
	internal float m_rotationSpeed = 10;

	internal GameObject m_cam;

	internal int m_hit = 0;

	internal Vector3 m_distance ;
	internal Vector3 jumpVelocity = new  Vector3(0, 10f, 0);


	//Checking is player in the state of certain animation
	internal bool Is_animation(string a_nameTag, bool a_isActive)
	{
		return m_animator.GetCurrentAnimatorStateInfo (0).IsTag (a_nameTag).Equals (a_isActive);
	}

	//Apply Certain animation clips as according to the condition menditon in mecanim
	internal void Animation(AnimationClip a_animationClip)
	{
		m_animator.SetFloat ("Anim", (int)a_animationClip);
	}

	//This method calls certain animtions that uses triggers to active
	internal void TriggerAnimation(string a_animationClip)
	{
		m_animator.SetTrigger (a_animationClip);
	}

	//This method calls certain animtions that uses triggers to active
//	internal void BoolAnimation(string a_animationClip)
//	{
//		m_animator.SetBool(a_animationClip , true);
//	}
//
	//To check if certain input key is being hold down
	internal bool Is_keyPressed(KeyCode a_key)
	{
		return Input.GetKey (a_key) && (m_hit == 0);
	}




	internal void Direction(float a_direction)
	{
		//Free movemnt, allowing player to move and play forward animation at all time
		Animation (AnimationClip.RunForward);
		m_transform.rotation=Quaternion.AngleAxis(a_direction, Vector3.up );


	}

	internal void Run(Vector3 a_position, AnimationClip a_animationClip)
	{
		m_transform.Translate (a_position * m_speed * Time.deltaTime);

		Animation(a_animationClip);
	}
}