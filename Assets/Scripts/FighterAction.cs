using Event_Channel;
using UnityEngine;

public enum FighterActions
{
    Idle = 0,
    Moving = 1,
    Dashing = 2,
    Attacking = 3,
    Blocking = 4,
    Reversal = 5,
    Swaying = 6
}

public class FighterAction : MonoBehaviour
{
    [SerializeField] private DebugEventChannel debugEventChannel;
    internal FighterActions ActualAction { get; private set; }

    internal void ChangeAction(FighterActions newAction)
    {
        if (ActualAction == newAction) return;
        ActualAction = newAction;
        debugEventChannel.OnFighterActionChanged(newAction);
    }

    internal bool IsAbleToBlock()
    {
        return ActualAction is FighterActions.Blocking or FighterActions.Idle or FighterActions.Moving;
    }

    private bool IsAttacking()
    {
        return ActualAction is FighterActions.Attacking;
    }

    private bool IsBlocking()
    {
        return ActualAction is FighterActions.Blocking;
    }

    private bool IsIdle()
    {
        return ActualAction is FighterActions.Idle;
    }
}