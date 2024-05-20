using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [HideInInspector]
    public AudioClip explosionSound;

    AudioSource source;

    float lifespan;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        source.PlayOneShot(explosionSound);
    }
}
