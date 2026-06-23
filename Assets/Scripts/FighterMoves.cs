using DefaultNamespace.Event_Channel;
using UnityEngine;

public class FighterMoves : MonoBehaviour
{
    [Header("Event Channel")] [SerializeField]
    private GameManagerEventChannel gameManagerEventChannel;

    [Header("Scripts References")] [SerializeField]
    private AnimationController animationController;

    [SerializeField] private Rigidbody2D fighterRigidbody2D;

    [Header("Fighter Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedCrouching;
    private Vector2 _movementAxis;
    [SerializeField] private float attackHighFrameDuration;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashFrameDuration;
    private float _dashInputAxis;
    private float _dashTimer;

    private float _actualActionFrameTime;

    internal FighterStance ActualStance { private set; get; }
    internal FighterAction ActualAction { private set; get; }
    
    private ArenaSide _arenaSide;

    private PlayerID _fighterID;


    private void Start()
    {
        ActualAction = FighterAction.Idle;
        ActualStance = FighterStance.Standing;
    }
    
    internal void SetFighterID(PlayerID playerID)
    {
        _fighterID = playerID;
        _arenaSide = _fighterID == PlayerID.Player1 ? ArenaSide.Left : ArenaSide.Right;
    }

    internal void StartMoving(float movementInputAxis)
    {
        ActualAction = FighterAction.Moving;
        animationController.Movement(movementInputAxis);
        _movementAxis.x = movementInputAxis;
    }

    internal void StopMoving()
    {
        ActualAction = FighterAction.Idle;
        animationController.Movement(0);
        _movementAxis.x = 0;
    }

    private void UpdateMovement()
    {
        if (ActualStance == FighterStance.Standing)
        {
            fighterRigidbody2D.MovePosition(fighterRigidbody2D.position +_movementAxis * moveSpeed * Time.fixedDeltaTime);
        }
        else if (ActualStance == FighterStance.Crouching)
        {
            fighterRigidbody2D.MovePosition(fighterRigidbody2D.position +_movementAxis * moveSpeedCrouching * Time.fixedDeltaTime);
        }
    }

    private void UpdateIdle()
    {
        fighterRigidbody2D.linearVelocity = new Vector2(0, fighterRigidbody2D.linearVelocity.y);
    }

    internal void StartDashing(float dashInputAxis)
    {
        ActualAction = FighterAction.Dashing;

        _actualActionFrameTime = dashFrameDuration;
        SetFighterID(dashInputAxis > 0 ? ArenaSide.Right : ArenaSide.Left);
    }

    private void UpdateDash()
    {
        _dashTimer -= Time.fixedDeltaTime;

        fighterRigidbody2D.MovePosition(fighterRigidbody2D.position * dashDistance * Time.fixedDeltaTime);

        if (_dashTimer <= 0)
        {
            ActualAction = FighterAction.Idle;
        }
    }

    private void UpdateAttack()
    {
        if (_actualActionFrameTime > attackHighFrameDuration)
        {
            ActualAction = FighterAction.Idle;
        }
    }

    private void FixedUpdate()
    {
        switch (ActualAction)
        {
            case FighterAction.Dashing:
                UpdateDash();
                break;

            case FighterAction.Moving:
                UpdateMovement();
                break;

            case FighterAction.Idle:
                UpdateIdle();
                break;

            case FighterAction.Attacking:
                UpdateAttack();
                break;
        }
    }

    internal void GotHit(AttackHeight attackType)
    {
        if (attackType == AttackHeight.High)
        {
            if (IsStanding())
            {
                if (IsAbleToBlock())
                {
                    animationController.BlockHigh();
                }
                else
                {
                    animationController.LaunchedWhileStanding();
                    gameManagerEventChannel.InvokeOnPlayerHit(_fighterID);
                }
            }
        }

        if (attackType == AttackHeight.Low)
        {
            if (IsCrouching())
            {
                if (IsAbleToBlock())
                {
                    animationController.BlockLow();
                }
                else
                {
                    animationController.KnockedOutWhileCrouching();
                }
            }
            else if (IsStanding())
            {
                animationController.KnockedOutWhileStanding();
                gameManagerEventChannel.InvokeOnPlayerHit(_fighterID);
            }
        }
    }

    private void GotBlocked()
    {
    }

    private void GotCountered()
    {
        animationController.CounterHit();
    }

    internal void ExecuteAttackHigh()
    {
        ActualStance = FighterStance.Standing;
        ActualAction = FighterAction.Attacking;
        animationController.AttackHigh();
    }

    internal void ExecuteAttackLow()
    {
        animationController.AttackLow();
    }

    internal void Crouch()
    {
        animationController.Crouch();
    }

    internal void StandUp()
    {
        animationController.StandingUp();
    }

    internal void Sway()
    {
        animationController.Sway();
    }

    private bool IsAbleToBlock()
    {
        return ActualAction is FighterAction.Blocking or FighterAction.Idle or FighterAction.Moving;
    }

    private bool IsStanding()
    {
        return ActualStance is FighterStance.Standing;
    }

    private bool IsCrouching()
    {
        return ActualStance is FighterStance.Crouching;
    }

    private bool IsAttacking()
    {
        return ActualAction is FighterAction.Attacking;
    }

    private bool IsBlocking()
    {
        return ActualAction is FighterAction.Blocking;
    }

    private bool IsIdle()
    {
        return ActualAction is FighterAction.Idle;
    }

    private void SetFighterID(ArenaSide newSide)
    {
        _arenaSide = newSide;
    }

    private int GetFacingDirection()
    {
        return _arenaSide == ArenaSide.Left ? -1 : 1;
    }
}