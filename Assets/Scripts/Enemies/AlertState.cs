using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AlertState : IEnemyAI
{
    EnemyStates enemy;
    float timer = 0;
    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;

    }
    public void UpdateActions()
    {
        Search();
        Watch();
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            LookAround();
        }
    }

    void Watch()
    {
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange) &&
            hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.navMeshAgent.destination = hit.transform.position;    
            ToChaseState();
        }
    }

    void LookAround()
    {
        timer += Time.deltaTime;
        if (timer >= enemy.stayAlertTime)
        {
            timer = 0;
            ToPatrolState();
        }
    }

    void Search()
    {
        enemy.navMeshAgent.destination = enemy.LastKnownPosition;
        enemy.navMeshAgent.Resume();
    }


    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState()
    {
        Debug.Log("Ошибка");
    }

    public void ToAlertState()
    {
        Debug.Log("Ошибка");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}
