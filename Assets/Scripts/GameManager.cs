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
    [SerializeField]
    private GameObject ArchTower;
    #endregion PublicFields

    #region PrivateFields
    private bool isFirstBoot;
	private bool isPaused;
    private Towers currTowerType;
    private GameObject currTower;
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
        currTowerType = Towers.None;
        currTower = null;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOnOff();
        }
        //input and movment
        if (currTower != null)
        {
            Debug.Log("moving tower");
            currTower.transform.position = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (currTower != null)
            {
                Debug.Log("placing tower");
                //Color32 col = currTower.GetComponent<MeshRenderer>().material.color;
                //col.a = 255;
                currTower = null;
                currTowerType = Towers.None;
            }
        }
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
        if (currTowerType != Towers.ArcherTower)
        {
            Debug.Log("archer tower chosen");
            currTowerType = Towers.ArcherTower;
            currTower = Instantiate(ArchTower);
            currTower.transform.position = Input.mousePosition;
            //Color32 col = currTower.GetComponent<MeshRenderer>().material.color;
            //col.a = 150;
        }
        else
        {
            Debug.Log("none tower chosen");
            currTowerType = Towers.None;
        }
    }

    private void ClickCannonTower()
    {
        if (currTower != null)
        {
            Destroy(currTower);
        }
        if (currTowerType != Towers.CannonTower)
        {
            Debug.Log("cannon tower chosen");
            currTowerType = Towers.CannonTower;
        }
        else
        {
            Debug.Log("none tower chosen");
            currTowerType = Towers.None;
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