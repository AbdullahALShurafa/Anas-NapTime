using UnityEngine;

public class TrackObject : MonoBehaviour
{
	[Tooltip("Should we use free-look or 2D camera")]
	public bool freeLook = true;

	[Header("Free-look parameters")]
	[Tooltip("Object to follow")]
	public Transform target;

	[Tooltip("Determines smoothing of camera movement")]
	[Range(0, 2)]
	public float smoothing;

	[Header("2D view parameters")]
	[Tooltip("Direction for camera")]
	public Vector3 perspective;

	[Tooltip("2D camera angle")]
	public Vector3 angle;

	[Tooltip("Positional offset")]
	public Vector3 offset;

	[Tooltip("Distance for 2D camera from player")]
	[Range(5, 50)]
	public float distance;

	Player player;
	new Camera camera;
	Rigidbody body;
	Vector3 newRotation;

	void Start()
	{
		player = FindObjectOfType<Player>();
		camera = GetComponent<Camera>();
		body = GetComponent<Rigidbody>();
		newRotation = camera.transform.rotation.eulerAngles;

		if (perspective.magnitude == 0)
		{
			Debug.LogError("2D view perspective is not set");
		}
		else
		{
			perspective.Normalize();
		}
	}

	void Update()
	{
		if (camera && target)
		{
//			if (freeLook)
//			{
//				// Interpolate euler angles between current and target
//				newRotation.x = Mathf.LerpAngle(newRotation.x, target.transform.rotation.eulerAngles.x, (1 / smoothing) * Time.deltaTime);
//				newRotation.y = Mathf.LerpAngle(newRotation.y, target.transform.rotation.eulerAngles.y, (1 / smoothing) * Time.deltaTime);
//				newRotation.z = Mathf.LerpAngle(newRotation.z, target.transform.rotation.eulerAngles.z, (1 / smoothing) * Time.deltaTime);
//
//				// Give physical object body a velocity to move towards target and apply rotation changes
//				body.velocity = (target.transform.position - camera.transform.position) * (4 / smoothing);
//				camera.transform.rotation = Quaternion.Euler(newRotation);
//			}
//			else
//			{
				Vector3 targetPos = player.transform.position + offset;
				camera.transform.position = targetPos + perspective * distance;
				camera.transform.rotation = Quaternion.Euler(angle);
			//}
		}
	}
}

