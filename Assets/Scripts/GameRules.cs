using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] private GameRulesData gameRulesData;

    internal static float PlayersStartingPosition { get; private set; }
    internal static float RoundStartIntroTime { get; private set; }

    
    private void Awake()
    {
        LoadGameRulesData();
    }

    private void LoadGameRulesData()
    {
        PlayersStartingPosition = gameRulesData.playersStartingPosition;
        RoundStartIntroTime = gameRulesData.roundStartShowTime;
    }
}