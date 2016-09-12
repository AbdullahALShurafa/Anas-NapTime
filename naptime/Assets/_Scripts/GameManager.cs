using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	


	/// <summary>
	/// Fading
	/// </summary>
	public float fadeTime;
	public Image m_fadePlane;


	void Awake()
	{
		StartCoroutine (Fade (Color.black, Color.clear, 1));
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
