using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
	private static MenuButtons m_instance;

	public GameObject m_mainMenuHolder;
	public GameObject m_optionsMenuHolder;
	public GameObject m_quitCheckHolder;
	public GameObject m_creditsHolder;

	public RectTransform m_credits;
	public Image m_fadePlane;
	public Slider[] m_volumeSliders;
	public Image m_infoHolder;

	public GameObject g_BkMusic;

	void Awake()
	{
		
		if (!m_instance)
		{
			m_instance = this;
		} 

		else
		{
			Destroy (this.gameObject);
		}
	}


	public void OptionsMenu()
	{
		m_mainMenuHolder.SetActive (false);
		m_optionsMenuHolder.SetActive (true);
	}

	public void MainMenu()
	{
		m_mainMenuHolder.SetActive (true);
		m_optionsMenuHolder.SetActive (false);
		m_quitCheckHolder.SetActive (false);

	}

	public void Credits()
	{
		m_optionsMenuHolder.SetActive (false);
		StartCoroutine (Fade (Color.clear, Color.black, 1));
		m_creditsHolder.SetActive (true);
		StartCoroutine ("AnimateCredits");
		m_fadePlane.color = Color.clear;
	}

	public void ReturnToOptions()
	{
		m_optionsMenuHolder.SetActive (true);
		StopCoroutine ("AnimateCredits");
		m_fadePlane.color = Color.clear;
		m_creditsHolder.SetActive (false);
	}

	public void QuitCheck ()
	{
		m_mainMenuHolder.SetActive (false);
		m_quitCheckHolder.SetActive (true);
	}

	public void GoToScene(string a_GoToScene)
	{
		SceneManager.LoadScene(a_GoToScene);
		DontDestroyOnLoad(g_BkMusic);
	}

	public void TogglePanel (GameObject panel) 
	{
		panel.SetActive (!panel.activeSelf);
	}

	public void Quit()
	{
		Application.Quit();
	}

	IEnumerator AnimateCredits()
	{
		float speed = .04f;
		float animationPercent = 0;
		int dir = 1;

		while (animationPercent >= 0) 
		{
			animationPercent += Time.deltaTime * speed * dir;
			m_credits.anchoredPosition = Vector2.up * Mathf.Lerp (-350, 200, animationPercent);
			yield return null;
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
