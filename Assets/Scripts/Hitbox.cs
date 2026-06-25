using DefaultNamespace.Event_Channel;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private FighterMoves fighterActions;

    internal void HasBeenHit(AttackHeight attackType)
    {
        fighterActions.GotHit(attackType);
    }
    
}
