using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour {


	// canvas for pause and game scene set in engine 
	public Canvas m_gameCanvas;
	public Canvas m_pauseCanvas;
	public Canvas m_helpCanas;


	bool IsPasued = false;
	bool isHelpOpend = false;


	void Start () 
	{
		// at first the pause canvas is set to false
		m_pauseCanvas.enabled = false;
		m_helpCanas.enabled = false;
	}


	public void Goto (string a_scene) 
	{
		SceneManager.LoadScene(a_scene);
	}

	void Update()
	{

		CheckForPause();

	}

	void CheckForPause()
	{

		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			if(IsPasued == true)
			{
				Time.timeScale = 1.0f;
				m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
				m_pauseCanvas.enabled = false;
				IsPasued = false;
				TogglePlayerScript();


			} 

			else if (isHelpOpend == false)
			{
				Time.timeScale = 0.0f;
				m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
				m_pauseCanvas.enabled = true;	
				IsPasued = true;
				TogglePlayerScript();

			}
		}
	}


	public void Unpause()
	{
		// disable the canvas 
		m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
		// disable the pause canvas
		m_pauseCanvas.enabled = false;

		// unpause the game
		Time.timeScale = 1;
		IsPasued = false;

		TogglePlayerScript();


		// return audio
		//bk_source.UnPause();
	}


	public  void Pause()
	{
		if (isHelpOpend == false)
		{
			IsPasued = true;

			Time.timeScale = 0;
			//bk_source.Pause();

			// game canvas is set to off
			m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;

			// Enable the pause Scene .
			m_pauseCanvas.enabled = true;	
			TogglePlayerScript();


		}

	}

		
	public void OpenHelpCanvas()
	{
		if (IsPasued == false)
		{
			m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
			m_helpCanas.enabled = true;
			isHelpOpend = true;
		}

	}

	public void CloseHelpCanvas()
	{
		m_gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
		m_helpCanas.enabled = false;
		isHelpOpend = false;


	}

	void TogglePlayerScript()
	{
		GameObject PlayerScriptClass = GameObject.FindWithTag("Player");

		if (IsPasued)
		{
			PlayerScriptClass.GetComponent<Player>().enabled = false;
		}

		if (IsPasued == false)
		{
			PlayerScriptClass.GetComponent<Player>().enabled = true;
		}
	}
}


