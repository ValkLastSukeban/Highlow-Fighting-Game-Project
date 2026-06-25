using Event_Channel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerEventChannel gameManagerEventChannel;
    [SerializeField] private FighterMoves[] fighters;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        gameManagerEventChannel.PlayerHit += PlayerHasBeenHit;
    }

    private void OnDisable()
    {
        gameManagerEventChannel.PlayerHit -= PlayerHasBeenHit;
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

    private void PlayerHasBeenHit(FighterID fighterID)
    {
        
    }
}
