using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	public float speed = 40;

	GameObject player = null;
	bool isRiding = false;

	public GameObject secondCam;


	// Update is called once per frame
	void Update()
	{
		if (player && player.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
		{
			isRiding = true;
			player.SetActive(false);
			FindObjectOfType<TrackObject>().target = transform;
		}

		if (isRiding)
		{
			Vector3 pos = transform.position;
			FindObjectOfType<TrackObject>().enabled = false;
			//FindObjectOfType<Camera>().transform.parent = this.transform;
			secondCam.SetActive (true);
			// = this.transform;


			pos.z += Mathf.Lerp(speed, speed * 2, Input.GetAxis("Jump")) * Time.deltaTime;
			pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
			transform.position = pos;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<Player>())
		{
			player = collider.gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.GetComponent<Player>())
		{
			player = null;
		}
	}

	public void GetOutOfTheCar()
	{
		secondCam.SetActive (false);

		player.SetActive(true);
		FindObjectOfType<TrackObject>().enabled = true;

		player.transform.position = transform.position + Vector3.forward * 5;
		isRiding = false;

		FindObjectOfType<TrackObject>().target = player.transform;
	}

	public void Teleport(Transform point)
	{
		transform.position = point.position;
	}
}
