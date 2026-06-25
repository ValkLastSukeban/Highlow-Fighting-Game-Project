using System;
using Event_Channel;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text fighterStatusText;
    [SerializeField] private DebugEventChannel debugEventChannel;

    private string actualActionString;
    private string actualStanceString;
    private void OnEnable()
    {
        debugEventChannel.FighterActionChanged += SetFighterActionString;
        debugEventChannel.FighterStanceChanged += SetFighterStanceString;
    }
    
    private void OnDisable()
    {
        debugEventChannel.FighterActionChanged -= SetFighterActionString;
        debugEventChannel.FighterStanceChanged -= SetFighterStanceString;
    }

    private void UpdateFighterInfoText()
    {
        fighterStatusText.text =
            $" {actualActionString}; {actualStanceString}";
    }

    private void SetFighterActionString(FighterActions action)
    {
        actualActionString = action.ToString();
    }

    private void SetFighterStanceString(FighterStances stance)
    {
        actualStanceString = stance.ToString();
    }



}
