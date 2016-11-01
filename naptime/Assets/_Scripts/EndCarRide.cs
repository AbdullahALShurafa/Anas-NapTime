using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCarRide : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		collider.SendMessage("GetOutOfTheCar", SendMessageOptions.DontRequireReceiver);
	}
}
