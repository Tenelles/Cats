using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HeartParticles : MonoBehaviour
{
    [SerializeField] private Cat _cat;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _cat.GotPoints += OnCatGotPoints;
    }

    private void OnCatGotPoints(int points)
    {
        transform.position = _cat.transform.position;
        _particleSystem.Play();
    }

    private void OnDestroy()
    {
        _cat.GotPoints -= OnCatGotPoints;
    }
}
