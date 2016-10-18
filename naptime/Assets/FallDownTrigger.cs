using UnityEngine;
using System.Collections;

public class FallDownTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Player.myPlayer.Respawn();
		}
	}
}
