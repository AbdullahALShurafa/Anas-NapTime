using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Text timeText;

	float startTime;

	float minutes;
	float seconds;
	internal bool isTimerPaused;

	/// <summary>
	/// Screen fading
	/// </summary>
	public float fadeTime;
	public Image m_fadePlane;
	public GameObject endLevelPanel;
	public GameObject LostLevelPanel;
	public static GameManager g_gameManager;
	public Text endLevelScore;
	public Transform g_VfxPos;

	public ParticleSystem[] CollectiablesVFX;

	void Awake()
	{
		g_gameManager = this;

		// Start of every scene play the fading
		StartCoroutine (Fade (Color.black, Color.clear, 1));
		// Start the timer 
		StartCoroutine (Timer());
	}

//	void Update()
//	{
//		PanelsBehaviors();
//
//	}
//
	void PanelsBehaviors()
	{
		// when player dies
		if (!Player.myPlayer.isPlayerAlive)
		{
			// Show the game over panel
			LostLevelPanel.SetActive(true);
			isTimerPaused = true;

		}
	}

	internal void EndOfLevel()
	{
		// When level is cleared
		// play the panel animation and enable it
		endLevelPanel.SetActive(true);
		// Get me the timer score text from the timer text and implement in into my EndScore text
		endLevelScore.text = "Finished in: " +  minutes.ToString("00") + ":" + seconds.ToString("00") ;
		// i dont want player to have any behavior after this so disable his script
		Player.myPlayer.enabled = false;
	}

	// Call this whenever we want to play a VFX for collectiables
	public void InstantiateParticle(int i)
	{
		// instantiate and play the particle system 
		Instantiate (CollectiablesVFX[i] , g_VfxPos.position , Quaternion.identity);
		CollectiablesVFX[i].Play();
	}

	IEnumerator Timer()
	{
		while (true )
		{
			yield return new WaitForSeconds(1);

			// Check if wether we got the powerup or not & if we in the 2D world or not. because in the 2D world time pause.
			if (isTimerPaused == false && !Player.myPlayer.isInSecondDimension)
			{
				seconds++;

				if (seconds >= 59)
				{
					minutes++;
					seconds = 0;

				}

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

}
