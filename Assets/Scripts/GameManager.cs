using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region outFields
    [SerializeField]
	private GameObject panelMenu;
	[SerializeField]
	private GameObject panelUI;
	[SerializeField]
	private Text startText;

	private bool isFirstBoot;
	private bool isPaused;
    private Towers currTower;
    #endregion outFields

    #region enums
    enum Towers
    {
        None,
        ArcherTower,
        CannonTower
    }
    #endregion enums

    #region private functions
    // Start is called before the first frame update
    void Start()
	{
        currTower = Towers.None;
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

    private void ClickArcherTower()
    {
        if (currTower != Towers.ArcherTower)
        {
            Debug.Log("archer tower chosen");
            currTower = Towers.ArcherTower;
        }
        else
        {
            Debug.Log("none tower chosen");
            currTower = Towers.None;
        }
    }
    #endregion private functions

    #region public funtions
    public void onButtonClickQuit()
	{
		QuitGame();
	}

	public void onButtonClickStart()
	{
		StartGame();
	}

    public void onButtonClickArcherTower()
    {
        ClickArcherTower();
    }
    #endregion public functions
}
