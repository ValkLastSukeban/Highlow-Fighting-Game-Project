using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerEventChannel gameManagerEventChannel;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RoundStart();
    }

    private void OnEnable()
    {
        gameManagerEventChannel.onPlayerHit += PlayerHasBeenHit;
    }

    private void OnDisable()
    {
        gameManagerEventChannel.onPlayerHit -= PlayerHasBeenHit;
    }

    private void RoundStart()
    {
        gameManagerEventChannel.onRoundStart(GameRules.RoundStartIntroTime);
    }

    private void PlayerHasBeenHit(PlayerID playerID)
    {
        
    }
}
