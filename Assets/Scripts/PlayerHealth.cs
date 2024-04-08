using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    public AudioClip hit;
    public Flashscreen flash;
    AudioSource source;
    float health;
    bool isGameOver = false;

    void Start()
    {
        health = maxHealth;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        health = Mathf.Clamp(health, -Mathf.Infinity, maxHealth);
        if(health <= 0 && !isGameOver)
        {
            isGameOver = true;
            GameManager.Instance.PlayerDeath();
        }
    }

    void EnemyHit(float damage)
    {
        source.PlayOneShot(hit);
        health -= damage;
        flash.TookDamage();
        Debug.Log(health);
    }
}
