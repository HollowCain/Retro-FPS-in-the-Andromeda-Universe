using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : IEnemyAI
{
    EnemyStates enemy;
    public ChaseState (EnemyStates enemy)
    {
        this.enemy = enemy;
        
    }
    public void UpdateActions()
    {
        Watch();
        Chase();
    }

    void Watch()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange) &&
                hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.LastKnownPosition = hit.transform.position;
        }
        else
        {
            ToAlertState();
        }
    }

    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();
        if(enemy.navMeshAgent.remainingDistance <= enemy.attackRange)
        {
            ToAttackState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        Debug.Log("Не делай этого");
    }

    public void ToAttackState()
    {
        Debug.Log("Сейчас будет больно");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("Потерял врага");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("Не делай этого");
    }
}
