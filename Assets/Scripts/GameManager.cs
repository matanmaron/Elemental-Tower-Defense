using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject panelMenu;
	[SerializeField]
	private GameObject panelUI;
	[SerializeField]
	private Text startText;

	private bool isFirstBoot;
	private bool isPaused;

	// Start is called before the first frame update
	void Start()
	{
		isFirstBoot = true;
		isPaused = false;
		PauseOnOff();
	}

	private void PauseOnOff()
	{
		isPaused = !isPaused;
		panelMenu.SetActive(isPaused);
		panelUI.SetActive(!isPaused);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void onButtonClickQuit()
	{
		QuitGame();
	}

	public void onButtonClickStart()
	{
		StartGame();
	}

	private void StartGame()
	{
		if (isFirstBoot)
		{
			Debug.Log("Start Game");
			startText.text = "Resume Game";
			isFirstBoot = false;
		}
		else
		{
			Debug.Log("Resume Game");
		}
		PauseOnOff();
	}

	private void QuitGame()
	{
		Debug.Log("quit game");
		Application.Quit();
	}
}
