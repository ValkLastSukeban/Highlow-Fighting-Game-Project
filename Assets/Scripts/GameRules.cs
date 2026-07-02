using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] private GameRulesData gameRulesData;

    internal static Vector3 PlayersStartingPosition { get; private set; }
    internal static float RoundStartIntroTime { get; private set; }
    
    private void OnEnable()
    {
        LoadGameRulesData();
    }

    private void LoadGameRulesData()
    {
        PlayersStartingPosition = gameRulesData.playersStartingPosition;
        RoundStartIntroTime = gameRulesData.roundStartShowTime;
    }
}