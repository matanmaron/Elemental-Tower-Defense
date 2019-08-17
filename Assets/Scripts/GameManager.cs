using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region PublicFields
    [SerializeField]
	private GameObject panelMenu;
	[SerializeField]
	private GameObject panelUI;
    [SerializeField]
    private GameObject panelWin;
    [SerializeField]
    private GameObject panelLose;
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
    private EnemyScript Enemy1Spiders;
    [SerializeField]
    private EnemyScript Enemy2Pirates;
    [SerializeField]
    private EnemyScript Enemy3Chicken;
    [SerializeField]
    private EnemyScript Enemy4Barbarian;
    [SerializeField]
    private EnemyScript Enemy5Knight;
    [SerializeField]
    private Text TimerText;
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text HealthText;

    public List<EnemyScript> Enemys;
    #endregion PublicFields

    #region PrivateFields
    private bool isFirstBoot;
	private bool isPaused;
    private Towers currTowerType;
    private GameObject currTower;
    private float Timer;
    private bool RunNextWave;
    private bool StartTimer;
    private bool RunTimer;
    private int wave;
    private bool isGameOver;
    internal int Life;
    internal int Money;

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

    internal void RemoveEnemy(GameObject enemy)
    {
        Enemys.Remove(enemy.GetComponent<EnemyScript>());
    }
    #endregion enums

    #region private functions
    // Start is called before the first frame update
    void Start()
	{
        isGameOver = false;
        panelLose.SetActive(false);
        panelWin.SetActive(false);
        wave = 1;
        RunNextWave = true;
        StartTimer = true;
        RunTimer = false;
        Money = 10;
        Life = 5;
        TimerText.text = string.Empty;
        currTowerType = Towers.None;
        currTower = null;
        isFirstBoot = true;
		isPaused = false;
        Enemys = new List<EnemyScript>();
		TogglePause();
        SetMoney();
        SetHealth();
	}

    private void SetTimer()
    {
        Timer = 5f;
        TimerText.text = Timer.ToString();
    }

    internal void SetMoney()
    {
        MoneyText.text = Money.ToString();
    }

    internal void SetHealth()
    {
        HealthText.text = Life.ToString();
        if (Life<=0)
        {
            GameOverLose();
        }
    }

    private void GameOverWin()
    {
        isGameOver = true;
        panelUI.SetActive(false);
        Debug.Log("GameOver - win");
        panelWin.SetActive(true);
    }

    private void GameOverLose()
    {
        isGameOver = true;
        panelUI.SetActive(false);
        Debug.Log("GameOver - lose");
        panelLose.SetActive(true);
    }

    private void SpawnWave()
    {
        RunNextWave = false;
        int sec = 0;
        for (int i = 0; i < WaveSize; i++)
        {
            Invoke("SpawnEnemys",sec);
            sec+=4;
        }
    }

    private void SpawnEnemys()
    {
        switch (wave)
        {
            case 1: Enemys.Add(Instantiate(Enemy1Spiders,StartPoint.transform.position,StartPoint.transform.rotation)); break;
            case 2: Enemys.Add(Instantiate(Enemy2Pirates, StartPoint.transform.position, StartPoint.transform.rotation)); break;
            case 3: Enemys.Add(Instantiate(Enemy3Chicken, StartPoint.transform.position, StartPoint.transform.rotation)); break;
            case 4: Enemys.Add(Instantiate(Enemy4Barbarian, StartPoint.transform.position, StartPoint.transform.rotation)); break;
            case 5: Enemys.Add(Instantiate(Enemy5Knight, StartPoint.transform.position, StartPoint.transform.rotation)); break;
            default:
                Debug.Log("no more enemys...");
                GameOverWin();
                break;
        }
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
        if (isGameOver)
        {
            return;
        }

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
        CheckTimerAndWave();
	}

    private void CheckTimerAndWave()
    {
        if (!isPaused)
        {
            if (StartTimer && RunNextWave)
            {
                SetTimer();
                RunTimer = true;
                StartTimer = false;
            }
            else if (RunTimer)
            {
                if (Timer>0)
                {
                    Timer -= Time.deltaTime;
                    TimerText.text = Timer.ToString();
                }
                else
                {
                    TimerText.text = string.Empty;
                    SpawnWave();
                    RunTimer = false;
                }
            }
            else if(Enemys.Count < 1)
            {
                RunNextWave = true;
                //next wave
                StartTimer = true;
                wave++;
            }
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
        var cost = Tower.GetComponent<TowerScript>().Cost;
        if (Money - cost >= 0)
        {
            var t = Instantiate(Tower);
            t.transform.position = GetMouseOnScreen();
            //t.transform.position = currTower.transform.position;
            Destroy(currTower);
            currTower = null;
            currTowerType = Towers.None;
            Money -= cost;
            SetMoney();
        }
        else
        {
            currTower = null;
            currTowerType = Towers.None;
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
        TogglePause();
    }

    private void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    private void ResetGame()
    {
        Debug.Log("reset game");
        SceneManager.LoadScene("MainScene"); //Load scene called Game
    }

    private void ClickTower(Towers clicktype)
    {
        if (currTowerType != clicktype)
        {
            Debug.Log("Fire tower chosen");
            currTowerType = clicktype;
            currTower = MakeTower();
        }
        else
        {
            Debug.Log("none tower chosen");
            currTowerType = Towers.None;
        }
    }

    private void ClickFireTower()
    {
        ClickTower(Towers.FireTower);
    }

    private GameObject MakeTower()
    {

        return Instantiate(TowerBuild, new Vector3(0,0,0), Quaternion.identity);
    }

    private void ClickWaterTower()
    {
        ClickTower(Towers.WaterTower);
    }

    private void ClickEarthTower()
    {
        ClickTower(Towers.EarthTower);
    }

    private void ClickWindTower()
    {
        ClickTower(Towers.WindTower);
    }

    #endregion private functions

    #region public funtions
    public void onButtonClickQuit()
	{
		QuitGame();
	}

    public void onButtonClickReset()
    {
        ResetGame();
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
