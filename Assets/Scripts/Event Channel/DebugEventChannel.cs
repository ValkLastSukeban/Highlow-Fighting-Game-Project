using System;
using UnityEngine;

namespace Event_Channel
{
    public class DebugEventChannel : ScriptableObject
    {
        internal event Action<FighterActions> FighterActionChanged;
        internal virtual void OnFighterActionChanged(FighterActions fighterAction)
        {
            FighterActionChanged?.Invoke(fighterAction);
        }
        
        internal event Action<FighterStances> FighterStanceChanged;
        internal virtual void OnFighterStanceChanged(FighterStances fighterStance)
        {
            FighterStanceChanged?.Invoke(fighterStance);
        }

        
    }
}