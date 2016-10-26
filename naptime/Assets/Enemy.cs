using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {



	public Animator m_animator;


	void OnTriggerEnter(Collider col)
	{

		// When enemies damage our player and our shield powerup is not on
		if (col.gameObject.CompareTag("Player") && Player.myPlayer.isAttackingOrSliding)
		{
			m_animator.SetTrigger("die");
			Destroy(gameObject,5);

		}


	}

}
