using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class video : MonoBehaviour {

	public MovieTexture movie;
	private Renderer rend;
	public AudioSource clip;


	/// <summary>
	/// Fading
	/// </summary>
	public float fadeTime;
	public Image m_fadePlane;
	bool isMovieStarted = false;

	void Awake()
	{
		StartCoroutine (Fade (Color.black, Color.clear, 1));
	}
	void Start () 

	{
		 isMovieStarted = false;

		rend = GetComponent<Renderer>();
		clip = GetComponent<AudioSource>();
		rend.material.mainTexture = movie as MovieTexture;

		//rend.enabled = false;

	
	}

	void Update()
	{
		if (!movie.isPlaying && isMovieStarted)
		{
			SceneManager.LoadScene("Level1");
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

		//rend.enabled = true;
		clip.clip = movie.audioClip;
		movie.Play();
		clip.Play();
		isMovieStarted=true;
	}

	public void SkipBtn()
	{
		SceneManager.LoadScene("Level1");


	}

}
