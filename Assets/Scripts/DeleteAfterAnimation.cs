using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterAnimation : MonoBehaviour
{
    public float delay = 0f;
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length + delay);
    }
}
