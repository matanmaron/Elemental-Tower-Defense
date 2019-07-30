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

    private bool isPaused;
    private bool isFirstBoot;

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
        throw new NotImplementedException();
    }

    private void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
