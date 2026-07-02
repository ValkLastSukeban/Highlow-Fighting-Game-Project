using UnityEngine;

[CreateAssetMenu(fileName = "GameRulesData", menuName = "Scriptable Objects/Game Rules Data")]
public class GameRulesData : ScriptableObject
{
    [SerializeField] internal Vector3 playersStartingPosition;
    [SerializeField] internal float roundStartShowTime;
    [SerializeField] internal bool allowWalking;
}
