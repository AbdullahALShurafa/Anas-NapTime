using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Transform[] waypoints;
    public float speed = 1.0f;
	public Animator m_animator;

	int currentWaypoint = 0;

	public bool loop = true;

	private bool enemyIsAlive = true;

	CapsuleCollider cs;
	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();	}

	void OnTriggerEnter(Collider col)
	{

		// When enemies damage our player and our shield powerup is not on
		if (col.gameObject.CompareTag("Player") && Player.myPlayer.isAttacking)
		{
			m_animator.SetTrigger("die");
			enemyIsAlive = false;
			//Destroy(gameObject,5);

		}


	}

	void FixedUpdate () 
	{
		// Waypoint not reached yet? then move closer
		if (transform.position != waypoints[currentWaypoint].position && enemyIsAlive) 
		{
			
			Vector3 p = Vector3.MoveTowards(transform.position,
				waypoints[currentWaypoint].position,
				speed);
			transform.LookAt(waypoints[currentWaypoint]);

			rb.MovePosition(p);

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
