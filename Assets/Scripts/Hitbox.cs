using DefaultNamespace.Event_Channel;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private FighterMoves fighterMoves;

    internal void HasBeenHit(AttackHeight attackType)
    {
        fighterMoves.GotHit(attackType);
    }
    
}
