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
    private GameObject FireTower;
    [SerializeField]
    private GameObject TowerBuild;
    [SerializeField]
    private GameObject WaterTower;
    [SerializeField]
    private GameObject EarthTower;
    [SerializeField]
    private GameObject WindTower;
    [SerializeField]
    private Transform StartPoint;
    [SerializeField]
    private Transform EndPoint;
    [SerializeField]
    private int WaveSize;
    [SerializeField]
    private GameObject Enemy1Spiders;
    [SerializeField]
    private Text TimerText;
    #endregion PublicFields

    #region PrivateFields
    private bool isFirstBoot;
	private bool isPaused;
    private Towers currTowerType;
    private GameObject currTower;
    private float Timer;
    private List<GameObject> Enemys;
    private int Money;
    #endregion PrivateFields

    #region enums
    enum Towers
    {
        None,
        FireTower,
        WaterTower,
        EarthTower,
        WindTower
    }
    #endregion enums

    #region private functions
    // Start is called before the first frame update
    void Start()
	{
        Money = 0;
        TimerText.text = string.Empty;
        currTowerType = Towers.None;
        currTower = null;
        isFirstBoot = true;
		isPaused = false;
        Enemys = new List<GameObject>();
		TogglePause();
        SetTimer();
	}

    private void SetTimer()
    {
        Timer = 5f;
        TimerText.text = Timer.ToString();
    }

    private void SpawnWave()
    {
        if (!isPaused)
        {
            if(Timer < 0)
            {
                TimerText.text = string.Empty;
                int sec = 0;
                for (int i = 0; i < WaveSize; i++)
                {
                    Invoke("SpawnEnemys",sec);
                    sec+=4;
                }
            }
            else
            {
                Timer -= Time.deltaTime; 
                TimerText.text = Timer.ToString();
            }
        }
    }

    private void SpawnEnemys()
    {
        Enemys.Add(Instantiate(Enemy1Spiders,StartPoint.transform.position,StartPoint.transform.rotation));
    }

    private void TogglePause()
	{
		isPaused = !isPaused;
		panelMenu.SetActive(isPaused);
		panelUI.SetActive(!isPaused);
        Time.timeScale = isPaused? 0 : 1;
        Debug.Log("time is "+Time.timeScale.ToString());
	}

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        //input and movment
        if (currTower != null)
        {
            //Debug.Log("moving tower");
            currTower.transform.position = GetMouseOnScreen();
        }
        //place tower
        if (Input.GetMouseButtonDown(0))
        {
            if (currTower != null)
            {
                SetTower();
            }
            else
            {
                CastRay();
            }
        }
        //spawn enemys
        if(Enemys.Count < 1)
        {
           SpawnWave();
        }
	}

    private void CastRay()
    {
        Debug.Log("cast ray..");
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000)) {            
            Debug.Log("ray hits: " + hit.collider.gameObject.name);
            var sct = hit.collider.gameObject.GetComponent<TowerScript>();
            if (sct != null)
            {
                sct.ToggleField();
            }
        }    
    }

    private Vector3 GetMouseOnScreen()
    {
        Vector3 wordPos;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            wordPos = hit.point;
        }
        else
        {
            wordPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        return wordPos;
    }

    private void SetTower()
    {
        Debug.Log("placing tower");
        switch (currTowerType)
        {
            case (Towers.FireTower):
                SetTower(FireTower);
                break;
            case (Towers.WaterTower):
                SetTower(WaterTower);
                break;
            case (Towers.EarthTower):
                SetTower(EarthTower);
                break;
            case (Towers.WindTower):
                SetTower(WindTower);
                break;
            default:
                break;
        }
    }

    private void SetTower(GameObject Tower)
    {
        var t = Instantiate(Tower);
        t.transform.position = GetMouseOnScreen();
        //t.transform.position = currTower.transform.position;
        Destroy(currTower);
        currTower = null;
        currTowerType = Towers.None;
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
        TogglePause();
    }

    private void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    private void ClickFireTower()
    {
        if (currTowerType != Towers.FireTower)
        {
            Debug.Log("Fire tower chosen");
            currTowerType = Towers.FireTower;
            currTower = MakeTower();
        }
        else
        {
            Debug.Log("none tower chosen");
            currTowerType = Towers.None;
        }
    }

    private GameObject MakeTower()
    {

        return Instantiate(TowerBuild, new Vector3(0,0,0), Quaternion.identity);
    }

    private void ClickWaterTower()
    {
        throw new NotImplementedException();
    }

    private void ClickEarthTower()
    {
        throw new NotImplementedException();
    }

    private void ClickWindTower()
    {
        throw new NotImplementedException();
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

    public void onButtonClickFireTower()
    {
        ClickFireTower();
    }

    public void onButtonClickWaterTower()
    {
        ClickWaterTower();
    }

    public void onButtonClickEarthTower()
    {
        ClickEarthTower();
    }

    public void onButtonClickWindTower()
    {
        ClickWindTower();
    }

    #endregion public functions
}