using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public Transform[] waypoints;
    public int patrolRange;
    public int shootRange;
    public int attackRange;
    public Transform vision;
    public float stayAlertTime;

    public GameObject missile;
    public float missileDamage;
    public float missileSpeed;

    public bool onlyMelee = false;
    public float meleeDamage;
    public float attackDelay;

    public LayerMask raycast;

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public IEnemyAI currentState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public Vector3 LastKnownPosition;

    void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void OnTriggerEnter(Collider otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }
    void Start()
    {
        currentState = patrolState;
    }

   
    void Update()
    {
        currentState.UpdateActions();
    }

    void HiddenShot(Vector3 shotPosition)
    {
        Debug.Log("Кто стреляет?");
        LastKnownPosition = shotPosition;
        currentState = alertState;
    }

}
