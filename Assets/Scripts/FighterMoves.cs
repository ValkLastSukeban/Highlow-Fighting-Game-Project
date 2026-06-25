using DefaultNamespace.Event_Channel;
using Event_Channel;
using UnityEngine;

public class FighterMoves : MonoBehaviour
{
    [Header("Event Channel")] [SerializeField]
    private GameManagerEventChannel gameManagerEventChannel;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D fighterRigidbody2D;

    [Header("Scripts References")] 
    [SerializeField] private InputController inputController;
    [SerializeField] private AnimationController animationController;

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

    [SerializeField] private FighterStance fighterStance;
    [SerializeField] private FighterAction fighterAction;
    
    [SerializeField] private FighterID fighterID = FighterID.Fighter1;
    
    private ArenaSide _arenaSide;

    private float _xAxisLimit;
    
    internal void InitializeFighter()
    {
        InitializeAction();
        InitializeStance();
        InitializePosition();
        GetXAxisLimit();
        RegisterFighterActions();
    }

    private void InitializeAction()
    {
        fighterAction.ChangeAction(FighterActions.Idle);
    }

    private void InitializeStance()
    {
        fighterStance.ChangeStance(FighterStances.Standing);
    }

    private void InitializePosition()
    {
        transform.position = fighterID == FighterID.Fighter1
            ? -GameRules.PlayersStartingPosition
            : GameRules.PlayersStartingPosition;
    }

    private void GetXAxisLimit()
    {
        _xAxisLimit = GameRules.ArenaHorizontalLimit;
    }

    private void RegisterFighterActions()
    {
        if (!inputController) return;
        inputController.OnMovePerformed += StartMoving;
        inputController.OnMoveStopped += StopMoving;
    }
    
    private void OnDisable()
    {
        UnregisterActions();
    }

    private void UnregisterActions()
    {
        if (!inputController) return;
        inputController.OnMovePerformed -= StartMoving;
        inputController.OnMoveStopped -= StopMoving;
    }

    private void StartMoving(float movementInputAxis)
    {
        fighterAction.ChangeAction(FighterActions.Moving);
        animationController.Movement(movementInputAxis);
        _movementAxis.x = movementInputAxis;
    }

    private void StopMoving()
    {
        fighterAction.ChangeAction(FighterActions.Idle);
        animationController.Movement(0);
        _movementAxis.x = 0;
    }

    internal void StartDashing(float dashInputAxis)
    {
        fighterAction.ChangeAction(FighterActions.Dashing);

        _actualActionFrameTime = dashFrameDuration;
    }

    private void FixedUpdate()
    {
        switch (fighterAction.ActualAction)
        {
            case FighterActions.Dashing:
                UpdateDash();
                break;

            case FighterActions.Moving:
                UpdateMovement();
                break;

            case FighterActions.Idle:
                UpdateIdle();
                break;

            case FighterActions.Attacking:
                UpdateAttack();
                break;
        }
    }
    
    private void UpdateDash()
    {
        _dashTimer -= Time.fixedDeltaTime;

        fighterRigidbody2D.MovePosition(fighterRigidbody2D.position * dashDistance * Time.fixedDeltaTime);

        if (_dashTimer <= 0)
        {
            fighterAction.ChangeAction(FighterActions.Idle);
        }
    }
    
    private void UpdateMovement()
    {
        if (fighterStance.ActualStance == FighterStances.Standing)
        {
            fighterRigidbody2D.MovePosition(fighterRigidbody2D.position +_movementAxis * moveSpeed * Time.fixedDeltaTime);
        }
        else if (fighterStance.ActualStance == FighterStances.Crouching)
        {
            fighterRigidbody2D.MovePosition(fighterRigidbody2D.position +_movementAxis * moveSpeedCrouching * Time.fixedDeltaTime);
        }
    }

    private void UpdateIdle()
    {
        fighterRigidbody2D.linearVelocity = new Vector2(0, fighterRigidbody2D.linearVelocity.y);
    }

    private void UpdateAttack()
    {
        if (_actualActionFrameTime > attackHighFrameDuration)
        {
            fighterAction.ChangeAction(FighterActions.Idle);
        }
    }

    internal void GotHit(AttackHeight attackType)
    {
        if (attackType == AttackHeight.High)
        {
            if (fighterStance.IsStanding())
            {
                if (fighterAction.IsAbleToBlock())
                {
                    animationController.BlockHigh();
                }
                else
                {
                    animationController.LaunchedWhileStanding();
                    gameManagerEventChannel.InvokeOnPlayerHit(fighterID);
                }
            }
        }

        if (attackType == AttackHeight.Low)
        {
            if (fighterStance.IsCrouching())
            {
                if (fighterAction.IsAbleToBlock())
                {
                    animationController.BlockLow();
                }
                else
                {
                    animationController.KnockedOutWhileCrouching();
                }
            }
            else if (fighterStance.IsStanding())
            {
                animationController.KnockedOutWhileStanding();
                gameManagerEventChannel.InvokeOnPlayerHit(fighterID);
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
        fighterStance.ChangeStance(FighterStances.Standing);
        fighterAction.ChangeAction(FighterActions.Attacking);
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
    
    private int GetFacingDirection()
    {
        return _arenaSide == ArenaSide.Left ? -1 : 1;
    }
}