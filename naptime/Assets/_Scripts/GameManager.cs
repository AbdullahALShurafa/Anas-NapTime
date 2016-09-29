using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Text timeText;

	float startTime;

	float minutes;
	float seconds;
	public bool isTimer = true;
	internal bool isTimerPaused;

	/// <summary>
	/// Screen fading
	/// </summary>
	public float fadeTime;
	public Image m_fadePlane;
	public Image g_portalPanel;
	public GameObject endLevelPanel;
	public static GameManager g_gameManager;
	public Text highScore;


	void Awake()
	{
		g_gameManager = this;

		StartCoroutine (Fade (Color.black, Color.clear, 1));
		StartCoroutine (Timer());
	}

//	void Update()
//	{
//		// Timer in Minutes and seconds. 
//		startTime += Time.deltaTime;
//		// Check for 1 seconds and add a second. also check if the timer is being paused or not. because the timer will be paused by using a time powerup or at 2.5D Mode.
//		if (startTime >=1 && isTimerPaused == false )
//		{
//			seconds++;
//			//Reset the starttime to 0 so that we calculate a new second.
//			startTime = 0;
//			// when seconds get's to 60 means we got a full minitue
//			if (seconds >= 59)
//			{
//				// Add our minute 
//				minutes++;
//				// reset seconds so we calculate a new minute.
//				seconds = 0;
//
//			}
//			// Update our text.
//			timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
//		}
//	}

	void LevelEnded()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			endLevelPanel.SetActive(true);
			highScore.text = "Finished in: " +  minutes.ToString("00") + ":" + seconds.ToString("00") ;
			Player.myPlayer.enabled = false;

		}
	}

	IEnumerator Timer()
	{
		while (true )
		{
			yield return new WaitForSeconds(1);

			// Check if wether we got the powerup or not & if we in the 2D world or not. because in the 2D world time pause.
			if (isTimerPaused == false && Player.myPlayer.isInSecondDimension)
			{
				seconds++;

				if (seconds >= 59)
				{
					minutes++;
					seconds = 0;

				}

				//minutes = (int)(Time.deltaTime / 60);
				//seconds = (int)(Time.deltaTime % 60);
				timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
			}

		
		}
	}

	// Fading used for transsisions between scenes.
	IEnumerator Fade(Color a_from, Color a_to, float a_time)
	{
		float speed = 1 / a_time;
		float percent = 0;

		while (percent < 1) 
		{
			percent += Time.deltaTime * speed;
			m_fadePlane.color = Color.Lerp (a_from, a_to, percent);

			yield return null;
		}

	}

	// USed for portal fading
	public void Fading(Color a_from, Color a_to, float a_time)
	{
		float speed = 1 / a_time;
		float percent = 0;

		while (percent < 1) 
		{
			percent += Time.deltaTime * speed;
			g_portalPanel.color = Color.Lerp (a_from, a_to, percent);

		}

	}

}
