using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour, IPointerClickHandler
{
	private int fingerID = -1;
	bool isPaused = false;
	public GameObject pauseUI;

	private void Awake()
	{
		isPaused = false;
		Time.timeScale = 1.0f;

		if (pauseUI)
		{
			pauseUI.SetActive(false);
		}
	}

	public void SetPause()
	{
		if(pauseUI)
		{
			pauseUI.SetActive(true);
		}
		Time.timeScale = 0.0f;
		Debug.Log("Paused");
		isPaused = true;
	}

	public void UnsetPause()
	{
		if (pauseUI)
		{
			pauseUI.SetActive(false);
		}
		Time.timeScale = 1.0f;
		Debug.Log("UnPaused");
		isPaused = false;
	}

	public void TriggerPause()
	{
		if (isPaused)
		{
			UnsetPause();
		}
		else
		{
			SetPause();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
#if !UNITY_EDITOR
     fingerID = 0; 
#endif
		if (EventSystem.current.IsPointerOverGameObject(fingerID))
		{
			Debug.Log(eventData.lastPress);

			TriggerPause();
		}
		else
		{
			Debug.Log(" NOT game object touches");
		}
	}
}
