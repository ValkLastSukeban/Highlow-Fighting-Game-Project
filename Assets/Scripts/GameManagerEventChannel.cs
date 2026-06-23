using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Game Manager Event Channel",menuName = "Scriptable Objects/Game Manager Event Channel")]
public class GameManagerEventChannel : ScriptableObject
{
    internal UnityAction<PlayerID> onPlayerHit;
    internal void InvokeOnPlayerHit(PlayerID playerID)
    {
        onPlayerHit?.Invoke(playerID);
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
