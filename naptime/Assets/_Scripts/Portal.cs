using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject player;
    bool activatePortal;
    bool isCameraViewDefault;
    public GameObject SpawnPoint;
   
	// Use this for initialization
	void Start () {
        activatePortal = false;
        isCameraViewDefault = true;
	}
	
	// Update is called once per frame
	void Update () {
       
        if(activatePortal)
        {
            Camera.main.fieldOfView += 30 * Time.deltaTime;
           
        }
        if(Camera.main.fieldOfView >= 180)
        {
            player.transform.localPosition = new Vector3(SpawnPoint.transform.position.x,SpawnPoint.transform.position.y,SpawnPoint.transform.position.z);
            isCameraViewDefault = false;
            activatePortal = false;
            Camera.main.GetComponent<follow>().rotationDamping = 0;
            Camera.main.GetComponent<follow>().height = 2;
        }
         if(!isCameraViewDefault)
        {
            Camera.main.fieldOfView -= 20 * Time.deltaTime;
            if (Camera.main.fieldOfView <= 60)
            {
                Camera.main.fieldOfView = 60;
                isCameraViewDefault = true;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
       


    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
          //  Player.in2Dwotld = true;
            activatePortal = true;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.Rotate(0, 90, 0);
            


        }
    }
}
