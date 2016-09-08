
using UnityEngine;

public class CheckPoints : MonoBehaviour 
{
	

	/// Indicate if the checkpoint is activated
	public bool Activated = false;

	public static GameObject[] CheckPointsList;


	void Start()
	{
		CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
	}

	/// Get position of the last activated checkpoint
	public static Vector3 GetActiveCheckPointPosition()
	{
		// If player die without activate any checkpoint, we will return a default position
		Vector3 result = new Vector3(0, 0, 0);

		if (CheckPointsList != null)
		{
			foreach (GameObject cp in CheckPointsList)
			{
				// We search the activated checkpoint to get its position
				if (cp.GetComponent<CheckPoints>().Activated)
				{
					result = cp.transform.position;
					break;
				}
			}
		}

		return result;
	}



	/// Activate the checkpoint
	private void ActivateCheckPoint()
	{
		// We deactive all checkpoints in the scene
		foreach (GameObject cp in CheckPointsList)
		{
			cp.GetComponent<CheckPoints>().Activated = false;
		}

		// We activated the current checkpoint
		Activated = true;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			ActivateCheckPoint();
		}
	}
}
