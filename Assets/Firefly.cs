using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Firefly : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Invoke("PlayAnimation", Random.Range(0,15f));
    }

    private void PlayAnimation()
    {
        _animator.Play("Firefly");
    }
}
