using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour {


	// canvas for pause and game scene set in engine 
	public Canvas m_gameCanvas;
	public Canvas m_pauseCanvas;

	bool IsPasued = false;


	void Start () 
	{
		// at first the pause canvas is set to false
		m_pauseCanvas.enabled = false;
	}


	public void Goto (string a_scene) 
	{
		SceneManager.LoadScene(a_scene);
	}

	public void LoadCurrentScene()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
		

	void Update()
	{
		CheckForPause();

	}

	void CheckForPause()
	{

		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			if(IsPasued == true)
			{
				Time.timeScale = 1.0f;
			//	m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
				m_pauseCanvas.enabled = false;
				IsPasued = false;
				TogglePlayerScript();
				// return audio
				//bk_source.UnPause();

			} 

			else 
			{
				Time.timeScale = 0.0f;
			//	m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = false;
				m_pauseCanvas.enabled = true;	
				IsPasued = true;
				TogglePlayerScript();

			}
		}
	}




	void TogglePlayerScript()
	{
		// Toggle player's controller if the game is paused.
		if (IsPasued)
		{
			Player.myPlayer.enabled = false;
		}

		if (!IsPasued)
		{
			Player.myPlayer.enabled = true;
		}
	}
}


