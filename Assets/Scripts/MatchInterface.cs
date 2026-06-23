using System.Collections;
using UnityEngine;

public class MatchInterface : MonoBehaviour
{
    [SerializeField] private GameManagerEventChannel gameManagerEventChannel;
    [SerializeField] private GameObject knockdownCountText;
    [SerializeField] private GameObject roundStartText;

    private void OnEnable()
    {
        gameManagerEventChannel.onRoundStart += ShowRoundStartText;
    }

    private void ShowRoundStartText(float showTime)
    {
        StartCoroutine(RoundStartShowCoroutine(showTime));
    }

    private IEnumerator RoundStartShowCoroutine(float showTime)
    {
        knockdownCountText.SetActive(true);
        yield return new WaitForSeconds(showTime/2);
        knockdownCountText.SetActive(false);
        
        roundStartText.SetActive(true); 
        yield return new WaitForSeconds(showTime/4);
        roundStartText.SetActive(false);
        
    }
}
