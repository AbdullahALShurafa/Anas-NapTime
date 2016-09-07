using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Transform[] wayPoints;
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(wayPoints[0].transform.position, wayPoints[1].transform.position, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
