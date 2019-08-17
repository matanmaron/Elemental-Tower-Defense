using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    private double Range;

    [SerializeField]
    internal int Cost;

    [SerializeField]
    private int Power;

    [SerializeField]
    ParticleSystem field;

    [SerializeField]
    ParticleSystem fire;

    [SerializeField]
    Transform fireHolder;

    private bool ShowField;
    private GameManager gameManager;
    private bool Reloaded;
    const int RealodTime = 1;
    //int i = 0;
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

    private void RotateFire(EnemyScript enemy)
    {
    //find the vector pointing from our position to the target
        Vector3 _direction = (enemy.transform.position - fireHolder.transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        fireHolder.transform.rotation = Quaternion.Slerp(fireHolder.transform.rotation, _lookRotation, Time.deltaTime * 50);
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
                    RotateFire(enemy);
                    //Debug.Log("shoot " + i);
                    //i++;
                    Shoot(enemy);
                    ShootFire();
                    Reloaded = false;
                    Invoke("ReloadTimer", RealodTime);
                    return;
                }
            }
        }
    }

    private void Shoot(EnemyScript enemy)
    {
        enemy.TakeDamage(Power);
    }

    private void ShootFire()
    {
        //Debug.Log("fire...");
        fire.Play();
        Invoke("ShootFireStop",1);
    }

    private void ShootFireStop()
    {
        fire.Stop();
        //Debug.Log("stop fire...");
    }

    private void ReloadTimer()
    {
        Reloaded = true;
    }
}
