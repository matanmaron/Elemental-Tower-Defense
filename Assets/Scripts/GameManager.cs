using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region PublicFields
    [SerializeField]
	private GameObject panelMenu;
	[SerializeField]
	private GameObject panelUI;
	[SerializeField]
	private Text startText;
    #endregion PublicFields

    #region PrivateFields
    private bool isFirstBoot;
	private bool isPaused;
    private Towers currTower;
    #endregion PrivateFields

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
        //input and movment

	}

    private void LateUpdate()
    {
        //folowing camera

    }

    private void FixedUpdate()
    {
        //for all physics

    }

    private void StartGame()
    {
        if (!isFirstBoot)
        {
            Debug.Log("Resume Game");
        }
        else
        {
            Debug.Log("Start Game");
            startText.text = "Resume Game";
            isFirstBoot = false;
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

    private void ClickCannonTower()
    {
        if (currTower != Towers.ArcherTower)
        {
            Debug.Log("cannon tower chosen");
            currTower = Towers.CannonTower;
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

    public void onButtonClickCannonTower()
    {
        ClickCannonTower();
    }
    #endregion public functions
}
