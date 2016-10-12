
using UnityEngine;

public class CheckPoints : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" )
		{
			// store / change the player position to the checkpoint position.
			other.GetComponent<Player>().checkPointPos = transform.position;

		}
	}
}
