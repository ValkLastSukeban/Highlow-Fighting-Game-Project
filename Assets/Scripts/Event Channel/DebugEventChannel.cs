using System;
using UnityEngine;

namespace Event_Channel
{    
    [CreateAssetMenu(fileName = "Debug Event Channel",menuName = "Scriptable Objects/Debug Event Channel")]
    public sealed class DebugEventChannel : ScriptableObject
    {
        internal event Action<FighterActions> FighterActionChanged;
        internal void OnFighterActionChanged(FighterActions fighterAction)
        {
            FighterActionChanged?.Invoke(fighterAction);
        }
        
        internal event Action<FighterStances> FighterStanceChanged;
        internal void OnFighterStanceChanged(FighterStances fighterStance)
        {
            FighterStanceChanged?.Invoke(fighterStance);
        }
    }
}