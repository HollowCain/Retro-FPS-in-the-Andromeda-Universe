using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    public int maxArmor;
    public AudioClip hit;
    public Flashscreen flash;
    AudioSource source;
    bool isGameOver = false;
    [SerializeField]
    float armor;
    [SerializeField]
    float health;

    void Start()
    {
        armor = 0;
        health = maxHealth;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        armor = Mathf.Clamp(armor, 0, maxArmor);
        health = Mathf.Clamp(health, -Mathf.Infinity, maxHealth);
        if (health <= 0 && !isGameOver)
        {
            isGameOver = true;
            GameManager.Instance.PlayerDeath();
        }
    }

    public void AddHealth(float value)
    {
        health += value;
    }

    public void AddArmor(float value)
    {
        armor += value;
    }

    void EnemyHit(float damage)
    {
        Debug.Log("EnemyHit called with damage: " + damage);
        Debug.Log("Initial armor: " + armor + ", Initial health: " + health);
        if (armor > 0 && armor >= damage)
        {
            armor -= damage;
        }
        else if (armor > 0 && armor < damage)
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }
        else
        {
            health -= damage;
        }
        Debug.Log("Updated armor: " + armor + ", Updated health: " + health);
        source.PlayOneShot(hit);
        if (flash != null)
        {
            flash.TookDamage();
        }
        else
        {
            Debug.LogError("Flash component is null");
        }
    }

}
