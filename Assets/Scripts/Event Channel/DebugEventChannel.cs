using System;
using UnityEngine;

namespace Event_Channel
{    
    [CreateAssetMenu(fileName = "Debug Event Channel",menuName = "Scriptable Objects/Debug Event Channel")]
    public sealed class DebugEventChannel : ScriptableObject
    {
        internal event Action<FighterStates> FighterActionChanged;
        internal void OnFighterActionChanged(FighterStates fighterAction)
        {
            FighterActionChanged?.Invoke(fighterAction);
        }
        
        internal event Action<FighterStates> FighterStanceChanged;
        internal void OnFighterStanceChanged(FighterStates fighterStance)
        {
            FighterStanceChanged?.Invoke(fighterStance);
        }
    }
}