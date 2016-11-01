using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTeleport : MonoBehaviour
{
	public Transform respawnPoint;

	void OnTriggerEnter(Collider collider)
	{
		collider.SendMessage("Teleport", respawnPoint, SendMessageOptions.DontRequireReceiver);
	}
}
