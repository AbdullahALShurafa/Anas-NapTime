using UnityEngine;
using System.Collections;

public class RotateEnemy : MonoBehaviour {

    public float g_speed = 10;
    public Vector3 rotation;

	// Update is called once per frame
	void Update () {
        
        transform.Rotate(rotation * g_speed * Time.deltaTime);

        
    }
}
