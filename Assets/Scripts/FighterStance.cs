using Event_Channel;
using UnityEngine;

public enum FighterStances
{
    Standing = 0,
    Crouching = 1,
    KnockedOut = 2
}

public class FighterStance : MonoBehaviour
{
    [SerializeField] private DebugEventChannel debugEventChannel;
    internal FighterStances ActualStance { get; private set; }

    internal void ChangeStance(FighterStances newStance)
    {
        if (ActualStance == newStance) return;
        ActualStance = newStance;
        debugEventChannel.OnFighterStanceChanged(newStance);
    }
    
    internal bool IsStanding()
    {
        return ActualStance is FighterStances.Standing;
    }

    internal bool IsCrouching()
    {
        return ActualStance is FighterStances.Crouching;
    }

    internal bool IsKnockedOut()
    {
        return ActualStance is FighterStances.KnockedOut;
    }
}