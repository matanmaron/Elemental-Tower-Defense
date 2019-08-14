using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject EndPoint;
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private float StartHealth;

    private GameManager gameManager;
    private NavMeshAgent Nav;
    private Animator anim;

    private float Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = StartHealth;
        gameManager = GameObject.Find("GameManagerHolder").GetComponent<GameManager>();
        Nav = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        GoToTarget();
        WalkAnim();
    }

    private void WalkAnim()
    {
        //Debug.Log("Walking Anim");
        anim.Play("walk");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void GoToTarget()
    {
        Nav.SetDestination(EndPoint.transform.position);
    }

    internal void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log("health is " + Health);
        HealthBar.fillAmount = Health / StartHealth;

        if (Health <= 0f)
        {
            Debug.Log("dead...");
            Die();
        }
    }

    private void Die()
    {
        gameManager.RemoveEnemy(enemy);
        Destroy(enemy);
    }

}
