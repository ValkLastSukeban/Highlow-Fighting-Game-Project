using Event_Channel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerEventChannel gameManagerEventChannel;
    [SerializeField] private Fighter[] fighters;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        gameManagerEventChannel.PlayerHit += FinishRound;
    }

    private void OnDisable()
    {
        gameManagerEventChannel.PlayerHit -= FinishRound;
    }
    
    private void Start()
    {
        SetupRoundStart();
        SetupFighters();
    }

    private void SetupRoundStart()
    {
        gameManagerEventChannel.OnRoundStart(GameRules.RoundStartIntroTime);
    }

    private void SetupFighters()
    {
        foreach (var fighter in fighters)
        {
            fighter.InitializeFighter();
        }
    }

    private void FinishRound(FighterID fighterDowned)
    {
        
    }
    
}
