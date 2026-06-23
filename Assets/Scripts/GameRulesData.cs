using UnityEngine;

[CreateAssetMenu(fileName = "GameRulesData", menuName = "Scriptable Objects/Game Rules Data")]
public class GameRulesData : ScriptableObject
{
    [SerializeField] internal float roundStartShowTime;
    [SerializeField] internal bool allowWalking;
    [SerializeField] internal float screenHorizontalLimit;
    [SerializeField] internal float playersStartingPosition;
}
