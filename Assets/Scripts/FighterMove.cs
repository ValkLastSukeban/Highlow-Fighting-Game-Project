using UnityEngine;

public class FighterMove : MonoBehaviour
{
    [SerializeField] private Height height;
    [SerializeField] private BoxCollider2D hurtboxCollider2D;
    [SerializeField] private float startupFrames;
    [SerializeField] private float activeFrames;
    [SerializeField] private float recoveryFrames;
    internal float StartupFrames => startupFrames;
    internal float ActiveFrames => activeFrames;
    internal float RecoveryFrames => recoveryFrames;
    internal MovePhase actualPhase;
    
    internal void EnableHurtbox()
    {
        hurtboxCollider2D.enabled = true;
    }
    
    internal void DisableHurtbox()
    {
        hurtboxCollider2D.enabled = false;
    }
    
}