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
        if (Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange, enemy.raycast) &&
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
        enemy.navMeshAgent.isStopped = false;
        if(enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }else if (enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMelee == false)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        Debug.Log("�� ����� �����");
    }

    public void ToAttackState()
    {
        Debug.Log("������ ����� ������");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("������� �����");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("�� ����� �����");
    }
}
