using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int MoveHash = Animator.StringToHash("Movement Axis");
    private static readonly int BackDashHash = Animator.StringToHash("Back Dash");
    private static readonly int ForwardDashHash = Animator.StringToHash("Forward Dash");
    private static readonly int SweptWhileStandingHash = Animator.StringToHash("Swept While Standing");
    private static readonly int SweptWhileCrouchingHash = Animator.StringToHash("Swept While Crouching");
    private static readonly int LaunchedWhileStandingHash = Animator.StringToHash("Launched While Standing");
    private static readonly int LaunchedWhileCrouchingHash = Animator.StringToHash("Launched While Crouching");
    private static readonly int HighBlockHash = Animator.StringToHash("High Block");
    private static readonly int LowBlockHash = Animator.StringToHash("Low Block");
    private static readonly int HighAttackHash = Animator.StringToHash("High Attack");
    private static readonly int LowAttackHash = Animator.StringToHash("Low Attack");
    private static readonly int PunishHash = Animator.StringToHash("Punish");
    private static readonly int CrouchingHasH = Animator.StringToHash("Crouch");
    private static readonly int CounterHitHash = Animator.StringToHash("CounterHit");
    private static readonly int SwayHash = Animator.StringToHash("Sway");
    
    private float ConvertFramesToSeconds(float frames)
    {
        return frames / 60;
    }
    
    internal void Crouch()
    {
        animator.SetBool(CrouchingHasH, true);
    }
    
    internal void Movement(float axisValue)
    {
        animator.SetFloat(MoveHash, axisValue);
    }

    internal void BackDash()
    {
        animator.SetBool(BackDashHash, true);
    }

    internal void ForwardDash()
    {
        animator.SetTrigger(ForwardDashHash);
    }

    internal void Sway()
    {
        animator.SetTrigger(SwayHash);
    }

    internal void LaunchedWhileStanding()
    {
        animator.SetTrigger(LaunchedWhileStandingHash);
    }

    internal void LaunchedWhileCrouching()
    {
        animator.SetTrigger(LaunchedWhileCrouchingHash);
    }
    
    internal void KnockedOutWhileStanding()
    {
        animator.SetTrigger(SweptWhileStandingHash);
    }

    internal void KnockedOutWhileCrouching()
    {
        animator.SetTrigger(SweptWhileCrouchingHash);
    }

    internal void StandingUp()
    {
        animator.SetBool(CrouchingHasH, false);
    }
    
    internal void AttackHigh()
    {
        animator.SetTrigger(HighAttackHash);
    }

    internal void AttackLow()
    {
        animator.SetTrigger(LowAttackHash);
    }

    internal void BlockHigh()
    {
        animator.SetTrigger(HighBlockHash);
    }

    internal void BlockLow()
    {
        animator.SetTrigger(LowBlockHash);
    }

    internal void Punish()
    {
        animator.SetTrigger(PunishHash);
    }

    internal void CounterHit()
    {
        animator.SetTrigger(CounterHitHash);
    }
}
