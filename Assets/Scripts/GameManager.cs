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
        gameManagerEventChannel.onPlayerHit += PlayerHasBeenHit;
    }

    private void OnDisable()
    {
        gameManagerEventChannel.onPlayerHit -= PlayerHasBeenHit;
    }
    
    private void Start()
    {
        SetupRoundStart();
        SetupFighters();
    }

    private void SetupRoundStart()
    {
        gameManagerEventChannel.onRoundStart(GameRules.RoundStartIntroTime);
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
