using UnityEngine;
using System.Collections;

public class RotateTrigger : MonoBehaviour {

    public GameObject RotateObject;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
            RotateObject.GetComponent<RotateEnemy>().enabled = true;
        }
    }
}
