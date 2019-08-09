using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject EndPoint;

    private NavMeshAgent Nav;
    // Start is called before the first frame update
    void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
        GoToTarget();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void GoToTarget()
    {
        Nav.SetDestination(EndPoint.transform.position);
    }
}
