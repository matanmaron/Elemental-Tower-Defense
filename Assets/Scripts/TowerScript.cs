using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    private double Range;

    [SerializeField]
    ParticleSystem field;

    private bool ShowField;
    private GameManager gameManager;
    private bool Reloaded;
    const int RealodTime = 1;
    int i = 0;
	void Start()
    {
        Reloaded = true;
        gameManager = GameObject.Find("GameManagerHolder").GetComponent<GameManager>();
        ShowField = false;
        field.Stop();
    }

    void Update()
    {
        IsEnemyInRange();
    }

    public void ToggleField()
    {
        ShowField = !ShowField;
        if (field != null)
        {

            if (ShowField)
            {
                field.Play();
            }
            else
            {
                field.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            Debug.Log("Field is " + ShowField.ToString());
        }
    }

    private void  IsEnemyInRange()
    {
        foreach (var enemy in gameManager.Enemys)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) <= Range)
            {
                //Debug.Log("in range");
                if (Reloaded)
                {
                    Debug.Log("shoot " + i);
                    i++;
                    Shoot(enemy);
                    Reloaded = false;
                    Invoke("ReloadTimer", RealodTime);
                }
            }
        }
    }

    private void Shoot(EnemyScript enemy)
    {
        enemy.TakeDamage(1);
    }

    private void ReloadTimer()
    {
        Reloaded = true;
    }
}
