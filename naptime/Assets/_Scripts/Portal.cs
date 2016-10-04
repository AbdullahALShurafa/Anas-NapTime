using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject player;
    bool activatePortal;
    bool isCameraViewDefault;
    public GameObject SpawnPointIn2D;
    public GameObject SpawnPointIn3D;
    bool isIn3Dworld;

    void Start()
    {
        activatePortal = false;
        isCameraViewDefault = true;
    }

    void Update()
    {
        Debug.Log(Player.myPlayer.isInSecondDimension);
        if (activatePortal)
        {
            Camera.main.fieldOfView += 30 * Time.deltaTime;
        }

        if (Camera.main.fieldOfView >= 180)
        {
            
            // Transport player to 2D world
            if(Player.myPlayer.isInSecondDimension == false)
            {
                if (SpawnPointIn2D != null)
                {
                    TransportPlayer(SpawnPointIn2D);
                    isIn3Dworld = false;
                }
            }
            // Transport Player to 3D world
            if(Player.myPlayer.isInSecondDimension == true)
            {
                if (SpawnPointIn3D != null)
                {
                    TransportPlayer(SpawnPointIn3D);
                    isIn3Dworld = true;
                }
            }

            isCameraViewDefault = false;
            activatePortal = false;
            Camera.main.GetComponent<follow>().rotationDamping = 0;
        }

        if (!isCameraViewDefault)
        {
            
            Camera.main.fieldOfView -= 20 * Time.deltaTime;
            if (Camera.main.fieldOfView <= 60)
            {
                Camera.main.fieldOfView = 60;
                isCameraViewDefault = true;
                Player.myPlayer.enabled = true;
                if(!isIn3Dworld)
                {
                    Player.myPlayer.isInSecondDimension = true;
                }
                if(isIn3Dworld)
                {
                    Player.myPlayer.isInSecondDimension = false;
                }
                
                
            }
        }



    }
   // TO DO::: PLAY IDLE ANIMATION
    void OnTriggerEnter(Collider obj)
    {
		// Going to 2D
        if (obj.gameObject.CompareTag("Player") && Player.myPlayer.isInSecondDimension == false)
        {
            HandlePortalEvents(new Vector3(0,90,0));
            GameManager.g_gameManager.isTimerPaused = true;
        }
        if (obj.gameObject.CompareTag("Player") && Player.myPlayer.isInSecondDimension == true)
        {
            HandlePortalEvents(new Vector3(0, 0, 0));
            GameManager.g_gameManager.isTimerPaused = false;
        }
    }

    void HandlePortalEvents(Vector3 a_playerTransform)
    {
        GetComponent<BoxCollider>().enabled = false;
        Player.myPlayer.enabled = false;
        activatePortal = true;
        player.transform.Rotate(a_playerTransform);
    }

    void TransportPlayer(GameObject a_point)
    {
        player.transform.localPosition = new Vector3(a_point.transform.position.x, a_point.transform.position.y, a_point.transform.position.z);
		Debug.Log("jj");
    }
}




