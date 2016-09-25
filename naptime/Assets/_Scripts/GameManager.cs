using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Text timeText;

	float startTime;
	float t;

	float minutes;
	float seconds;
	public bool isTimer = true;


	/// <summary>
	/// Fading
	/// </summary>
	public float fadeTime;
	public Image m_fadePlane;


	public static GameManager g_gameManager;


	void Awake()
	{
		g_gameManager = this;

		StartCoroutine (Fade (Color.black, Color.clear, 1));
		StartCoroutine (Timer());
	}


	IEnumerator Timer()
	{
		while (true )
		{
				yield return new WaitForSeconds(1);
			
				minutes = (int)(Time.time / 60);
				seconds = (int)(Time.time % 60);
				timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
		}
	}


		


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
