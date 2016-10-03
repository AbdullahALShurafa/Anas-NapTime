using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject player;
    bool activatePortal;
    bool isCameraViewDefault;
    public GameObject SpawnPointIn2D;
    public GameObject SpawnPointIn3D;
    bool isIn3Dworld;
    // Use this for initialization
    void Start()
    {
        activatePortal = false;
        isCameraViewDefault = true;
    }

    // Update is called once per frame
    void Update()
    {
        
       // Debug.Log(Player.myPlayer.isInSecondDimension);
        Debug.Log(isIn3Dworld);
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
            Camera.main.GetComponent<follow>().height = 2;
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

        if (obj.gameObject.CompareTag("Player") && Player.myPlayer.isInSecondDimension == false)
        {
            HandlePortalEvents(new Vector3(0,90,0));
            
        }
        if (obj.gameObject.CompareTag("Player") && Player.myPlayer.isInSecondDimension == true)
        {
            HandlePortalEvents(new Vector3(0, 0, 0));
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
    }
}


//	IEnumerator TransportToOtherDimension()
//	{
//		
////		//TODO: Play the particle system , Fade screen then teleport. Same thing for going back just patrol him last position
////		PS_Transport.Play();
////		isInSecondDimension = true;
////		Player.myPlayer.enabled = false;
////		yield return new WaitForSeconds(10);
////		GameManager.g_gameManager.Fading(Color.clear, Color.black, 1);
////
////        player.transform.localPosition = new Vector3(SpawnPoint.transform.position.x,SpawnPoint.transform.position.y,SpawnPoint.transform.position.z);
////		Player.myPlayer.enabled = true;
////		player.transform.Rotate(0, 90, 0);
//	}

