using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public GameObject player;
  //  bool activatePortal;
   // bool isCameraViewDefault;
    public GameObject SpawnPoint;
	internal bool isInSecondDimension = false;
	public static Portal _portalInstance;
	private int NoOfTimePlayerCanEnterPortal;
	public ParticleSystem PS_Transport;
	internal Animator m_animator;

	void Awake()
	{
		_portalInstance = this;
		m_animator = GetComponent<Animator>();

	}
//	void Start ()
//	{
//       // activatePortal = false;
//      //  isCameraViewDefault = true;
//	}
	

//	void Update () 
//	{
//		
//	       // manipulate field of view
//	        if(activatePortal)
//			Camera.main.fieldOfView += 30 * Time.deltaTime;
//	        
//
//	         if(Camera.main.fieldOfView >= 160)
//	        {
//	            player.transform.localPosition = new Vector3(SpawnPoint.transform.position.x,SpawnPoint.transform.position.y,SpawnPoint.transform.position.z);
//	            isCameraViewDefault = false;
//	            activatePortal = false;
//	            Camera.main.GetComponent<follow>().rotationDamping = 0;
//	            Camera.main.GetComponent<follow>().height = 2;
//	        }
//
//			// Return the normal field of view
//	          if(!isCameraViewDefault)
//	        {
//				Camera.main.fieldOfView -= 20 * Time.deltaTime;
//
//		        if (Camera.main.fieldOfView <= 60)
//		          {
//		              Camera.main.fieldOfView = 60;
//		              isCameraViewDefault = true;
//		          }
//	        }
//
//    }
//

	void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
			NoOfTimePlayerCanEnterPortal++;
			if (NoOfTimePlayerCanEnterPortal <=2)
			{
				Debug.Log("jhuhuhuh");
				player.transform.Rotate(0, 90, 0);
			}
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
}
