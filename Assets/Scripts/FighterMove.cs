using System;
using UnityEngine;

public abstract class FighterMove : MonoBehaviour
{
    [SerializeField] private int startupFrames;
    [SerializeField] private int activeFrames;
    [SerializeField] private int recoveryFrames;
    internal Action MoveAction { get; private set;}

    internal AttackPhase ActualPhase { get; private set; }

    internal void ResetMove()
    {
        ActualPhase = AttackPhase.Ready;
    }
}