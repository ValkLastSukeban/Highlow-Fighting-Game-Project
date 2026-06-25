using System;
using UnityEngine;
using UnityEngine.Events;

namespace Event_Channel
{
    [CreateAssetMenu(fileName = "Game Manager Event Channel",menuName = "Scriptable Objects/Game Manager Event Channel")]
    public class GameManagerEventChannel : ScriptableObject
    {
        internal event Action<FighterID> PlayerHit;
        internal void OnPlayerHit(FighterID fighterID)
        {
            PlayerHit?.Invoke(fighterID);
        }

        internal event Action<float> RoundStart;
        internal void OnRoundStart(float showTime)
        {
            RoundStart?.Invoke(showTime);
        }

        internal event Action FightStart;
        internal void OnFightStart()
        {
            FightStart?.Invoke();
        }


    }
}
