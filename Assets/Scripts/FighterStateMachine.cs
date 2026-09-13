using Event_Channel;
using System;
using UnityEngine;

internal enum FighterStates
{
    StandIdle = 0,
    StandAttack = 1,
    StandBlocking = 2,
    StandBlockStun = 3,
    CrouchIdle = 4,
    CrouchAttack = 5,
    CrouchBlocking = 6,
    CrouchBlockStun = 7,
    WalkForward = 8,
    WalkBackward = 9,
    Dashing = 10,
    Swaying = 11,
    KnockedOut = 12
}
public class FighterStateMachine : MonoBehaviour
{
    [SerializeField] private DebugEventChannel debugEventChannel;
    internal FighterStates FighterState { get; private set; }

    private void Start()
    {
        FighterState = FighterStates.StandIdle;
    }

    internal void ChangeState(FighterStates newState)
    {
        if (FighterState == newState) return;
        FighterState = newState;
        debugEventChannel.OnFighterActionChanged(newState);
    }

    internal bool IsAttacking()
    {
        return FighterState is FighterStates.StandAttack or FighterStates.CrouchAttack;
    }

    private bool IsBlocking()
    {
        return FighterState is FighterStates.StandBlockStun;
    }

    private bool IsIdle()
    {
        return FighterState is FighterStates.StandIdle;
    }

    internal bool IsStanding()
    {
        return FighterState is FighterStates.StandIdle;
    }

    internal bool IsCrouching()
    {
        return FighterState is FighterStates.CrouchIdle;
    }

    internal bool IsKnockedOut()
    {
        return FighterState is FighterStates.KnockedOut;
    }

    internal bool IsWalking()
    {
        return FighterState is FighterStates.WalkForward or FighterStates.WalkBackward;
    }

}