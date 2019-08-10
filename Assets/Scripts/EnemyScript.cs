using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject EndPoint;
    [SerializeField]
    private int Health;
    [SerializeField]
    private Image HealthBar;
    private int StartHealth;
    private NavMeshAgent Nav;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        StartHealth = Health;
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

    public void TakeDamage(int amount)
    {
        Health -= amount;
        HealthBar.fillAmount = Health / StartHealth;
        if (Health < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this);
    }
}
