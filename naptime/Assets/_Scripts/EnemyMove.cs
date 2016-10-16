using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Transform[] waypoints;
    public float speed = 1.0f;


	int currentWaypoint = 0;

	public bool loop = true;

	void FixedUpdate () {
		// Waypoint not reached yet? then move closer
		if (transform.position != waypoints[currentWaypoint].position) 
		{
			
			Vector3 p = Vector3.MoveTowards(transform.position,
				waypoints[currentWaypoint].position,
				speed);
			transform.LookAt(waypoints[currentWaypoint]);

			GetComponent<Rigidbody>().MovePosition(p);

		}

		// Waypoint reached, select next one
		else
		{

			currentWaypoint = (currentWaypoint + 1);

		}


		if (loop)
		{
			if (currentWaypoint == waypoints.Length)
				currentWaypoint = 0;
		}
	}
}
