using System;
using DefaultNamespace.Event_Channel;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private AttackHeight attackHeight;
    private bool _hasAlreadyHit;
    private Hitbox _lastHitboxHit;

    private void OnEnable()
    {
        _hasAlreadyHit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasAlreadyHit) return;
        
        _lastHitboxHit = other.GetComponent<Hitbox>();

        if (_lastHitboxHit == null)
        {
            Debug.LogWarning("[Hitbox] found a GameObject without [Hurtbox] attached to it.");
        }
        else
        {
            _lastHitboxHit.HasBeenHit(attackHeight);
        }

        _lastHitboxHit = null;
        _hasAlreadyHit = true;
    }
}