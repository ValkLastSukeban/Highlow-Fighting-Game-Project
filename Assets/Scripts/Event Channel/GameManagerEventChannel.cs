using UnityEngine;
using UnityEngine.Events;

namespace Event_Channel
{
    [CreateAssetMenu(fileName = "Game Manager Event Channel",menuName = "Scriptable Objects/Game Manager Event Channel")]
    public class GameManagerEventChannel : ScriptableObject
    {
        internal UnityAction<FighterID> onPlayerHit;
        internal void InvokeOnPlayerHit(FighterID fighterID)
        {
            onPlayerHit?.Invoke(fighterID);
        }

        internal UnityAction<float> onRoundStart;
        internal void InvokeOnRoundStart(float showTime)
        {
            onRoundStart?.Invoke(showTime);
        }

        internal UnityAction onFightStart;
        internal void OnFightStart()
        {
            onFightStart?.Invoke();
        }


    }
}
