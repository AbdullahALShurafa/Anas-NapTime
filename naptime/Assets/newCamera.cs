using UnityEngine;
using System.Collections;

public class newCamera : MonoBehaviour {

	[Tooltip("Object to follow")]
		public Transform target;

		[Tooltip("Determines smoothing of camera movement")]
		[Range(0, 2)]
		public float smoothing;

		new Camera camera;
		Rigidbody body;
		Vector3 newRotation;

		void Start()
		{
			camera = GetComponent<Camera>();
			body = GetComponent<Rigidbody>();
			newRotation = camera.transform.rotation.eulerAngles;
		}

		void Update()
		{
			if (target && camera)
			{

		
				// Interpolate euler angles between current and target
				newRotation.x = Mathf.LerpAngle(newRotation.x, target.transform.rotation.eulerAngles.x, (1 / smoothing) * Time.deltaTime);
				newRotation.y = Mathf.LerpAngle(newRotation.y, target.transform.rotation.eulerAngles.y, (1 / smoothing) * Time.deltaTime);
				newRotation.z = Mathf.LerpAngle(newRotation.z, target.transform.rotation.eulerAngles.z, (1 / smoothing) * Time.deltaTime);


				// Give physical object body a velocity to move towards target and apply rotation changes
				body.velocity = (target.transform.position - camera.transform.position) * (5f / smoothing);
				camera.transform.rotation = Quaternion.Euler(newRotation);
			}
		}



}
