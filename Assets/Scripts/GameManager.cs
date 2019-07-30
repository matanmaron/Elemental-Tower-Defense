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

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        PauseOnOff(true);
    }

    private void PauseOnOff(bool isFirstBoot = false)
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
        Debug.Log("Start Game");
        startText.text = "Resume Game";
    }

    private void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
