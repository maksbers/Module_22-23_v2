using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _damageRadius = 5f;
    [SerializeField] private float _damageAmount = 30f;
    [SerializeField] private float _delay = 2f;

    public float DamageRadius => _damageRadius;
    public float DamageAmount => _damageAmount;
    public float Delay => _delay;
}
