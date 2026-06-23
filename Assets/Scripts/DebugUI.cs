using System;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text fighterStatusText;
    [SerializeField] private FighterMoves fighterReference;

    private void FetchFighterInfo()
    {
        if (fighterReference == null) return;
        SetFighterStatusText(fighterReference.ActualStance.ToString(), fighterReference.ActualAction.ToString());
    }
    
    private void SetFighterStatusText(string actualStance, string actualAction)
    {
        fighterStatusText.text =
            $" {actualAction}; {actualStance}";
    }
    
    private void Update()
    {
        FetchFighterInfo(); 
    }
    

    

}
