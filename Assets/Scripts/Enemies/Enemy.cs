using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Sprite deadBody;
    public int maxhealth;
    float health;

    EnemyStates es;
    NavMeshAgent nma;
    SpriteRenderer sr;
    BoxCollider bc;
    private void Start()
    {
        health = maxhealth;
        es = GetComponent<EnemyStates>();
        nma = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            es.enabled = false;
            nma.enabled = false;
            sr.sprite = deadBody;
            bc.center = new Vector3(0, -0.43f, 0);
            bc.size = new Vector3(2.21f, 1.36f, 0.2f);
        }
    }

    void WeaponHit(float damage)
    {
        health -= damage;
    }
}
