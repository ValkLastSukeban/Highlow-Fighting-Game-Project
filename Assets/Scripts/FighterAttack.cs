using UnityEngine;

public class FighterAttack : MonoBehaviour
{
    [SerializeField] private Height height;
    [SerializeField] private BoxCollider2D hurtboxCollider2D;
    [SerializeField] private int startupFrames;
    [SerializeField] private int activeFrames;
    [SerializeField] private int recoveryFrames;
    internal AttackPhase ActualAttackPhase { get; private set; }
    internal Height AttackHeight => height;
    private int StartupFrames => startupFrames;
    private int ActiveFrames => activeFrames;
    private int RecoveryFrames => recoveryFrames;
    private int attackFrameCount;
    private bool attackWasBlocked;
    private bool isAttackInProgress;

    internal void StartAttack()
    {
        if (isAttackInProgress) return;
        ActualAttackPhase = AttackPhase.Startup;
        attackFrameCount = 0;
        attackWasBlocked = false;
        isAttackInProgress = true;
    }
    private void Update()
    {
        if (isAttackInProgress)
        {
            ExecuteNextAttackFrame();
        }
    }
    private void ExecuteNextAttackFrame()
    {
        Debug.Log("Execute Next Attack Frame");
        if (ActualAttackPhase == AttackPhase.Finished)
        {
            Debug.Log("Attack Phase = Finished, reseting attack");
            ResetAttack();
            return;
        }

        if (attackFrameCount == 0 && ActualAttackPhase == AttackPhase.Ready)
        {
            Debug.Log("Attack started");
            ChangeAttackPhase(AttackPhase.Startup);
        }
        else if (attackFrameCount == StartupFrames && ActualAttackPhase == AttackPhase.Startup)
        {
            Debug.Log("Attack active");
            EnableHitbox();
            ChangeAttackPhase(AttackPhase.Active);
        }
        else if (attackFrameCount == StartupFrames + ActiveFrames && ActualAttackPhase == AttackPhase.Active)
        {
            Debug.Log("Attack recovery");
            DisableHitbox();
            ChangeAttackPhase(AttackPhase.Recovery);
        }
        else if (attackFrameCount == StartupFrames + ActiveFrames + RecoveryFrames && ActualAttackPhase == AttackPhase.Recovery)
        {
            Debug.Log("Attack finished");
            ChangeAttackPhase(AttackPhase.Finished);
            return;
        }
        CountAttackFrames();
    }

    private void ResetAttack()
    {
        if (!isAttackInProgress) return;
        DisableHitbox();
        ActualAttackPhase = AttackPhase.Ready;
        attackFrameCount = 0;
        attackWasBlocked = false;
        isAttackInProgress = false;
    }

    private void ChangeAttackPhase(AttackPhase newPhase)
    {
        ActualAttackPhase = newPhase;
    }

    private void CountAttackFrames()
    {
        attackFrameCount++;
        Debug.Log("Attack frame count: " + attackFrameCount);
    }
    private void EnableHitbox()
    {
        hurtboxCollider2D.enabled = true;
    }

    private void DisableHitbox()
    {
        hurtboxCollider2D.enabled = false;
    }

    internal void GotBlocked()
    {
        attackWasBlocked = true;
    }

}