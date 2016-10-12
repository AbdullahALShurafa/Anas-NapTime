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
		// Back to 3D
        if (obj.gameObject.CompareTag("Player") && Player.myPlayer.isInSecondDimension == true)
        {
            HandlePortalEvents(new Vector3(0, 0, 0));
            GameManager.g_gameManager.isTimerPaused = false;
        }
    }

    void HandlePortalEvents(Vector3 a_playerTransform)
    {
		// Disable the portal collider so player can't go to 2D again.
        GetComponent<BoxCollider>().enabled = false;
		// Remove player behavior while doing the camera effect so that player dosent run or move
        Player.myPlayer.enabled = false;
		// bool toogle
        activatePortal = true;
		// Rotate my playe rin a certain transform
        player.transform.Rotate(a_playerTransform);
		// PLay the Idle animation so that he doesn't playing the last animation (ie. run )
		Player.myPlayer.Animation (AnimationClip.Idle);
		// Give player some stamina so if he goes to 2D with empty stamina 
		Player.myPlayer.AddStamina(100);


    }

    void TransportPlayer(GameObject a_point)
    {
        player.transform.localPosition = new Vector3(a_point.transform.position.x, a_point.transform.position.y, a_point.transform.position.z);
    }
}




