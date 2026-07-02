using System;
using Event_Channel;
using UnityEngine;

public class Fighter : MonoBehaviour
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
    
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashFrames;
    private float _dashInputAxis;

    private float _actionFrameCounter;

    [SerializeField] private FighterStance fighterStance;
    [SerializeField] private FighterAction fighterAction;
    
    [SerializeField] private FighterID fighterID = FighterID.Fighter1;

    [Header("Moves")]
    [SerializeField] private FighterMove fighterMoveHighAttack;
    [SerializeField] private FighterMove fighterMoveLowAttack;
    
    private ArenaSide _arenaSide;

    private bool hasBeenHit;
    
    private event Action ExecutingAction;

    internal void InitializeFighter()
    {
        RegisterInput();
        InitializeAction();
        InitializeStance();
        InitializePosition();
    }
    private void RegisterInput()
    {
        if (!inputController) return;
        inputController.MovePerformedAction += StartMoving;
        inputController.MoveStoppedAction += StopMoving;
        inputController.CrouchStartedAction += Crouch;
        inputController.CrouchStoppedAction += StandUp;
        inputController.HighAttackAction += StartHighAttack;
        inputController.LowAttackAction += StartLowAttack;

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

    private void OnDisable()
    {
        UnregisterActions();
    }

    private void UnregisterActions()
    {
        if (!inputController) return;
        inputController.MovePerformedAction -= StartMoving;
        inputController.MoveStoppedAction -= StopMoving;
        inputController.CrouchStartedAction -= Crouch;
        inputController.CrouchStoppedAction -= StandUp;
        inputController.HighAttackAction -= StartHighAttack;
        inputController.LowAttackAction -= StartLowAttack;
        
    }

    private void StartMoving(float movementInputAxis)
    {
        fighterAction.ChangeAction(FighterActions.Moving);
        ExecutingAction = MoveAction;
        animationController.Movement(movementInputAxis);
        _movementAxis.x = movementInputAxis;
    }

    private void StopMoving()
    {
        fighterAction.ChangeAction(FighterActions.Idle);
        ExecutingAction = IdleAction;
        animationController.Movement(0);
        _movementAxis.x = 0;
    }

    private void StartDashing(float dashInputAxis)
    {
        fighterAction.ChangeAction(FighterActions.Dashing);
        fighterStance.ChangeStance(FighterStances.Standing);
        ExecutingAction = DashAction;
        _actionFrameCounter = 0;
    }
    
    private void Crouch()
    {
        fighterStance.ChangeStance(FighterStances.Crouching);
        animationController.Crouching();
    }

    private void StandUp()
    {
        fighterStance.ChangeStance(FighterStances.Standing);
        animationController.StandingUp();
    }

    
    private void GotBlocked()
    {
        animationController.BlockHigh();
        animationController.BlockLow();
    }

    private void GotCountered()
    {
        animationController.CounterHit();
    }

    private void StartHighAttack()
    {
        fighterStance.ChangeStance(FighterStances.Standing);
        fighterAction.ChangeAction(FighterActions.Attacking);
        ExecutingAction = () => ExecuteFighterMove(fighterMoveHighAttack);
        animationController.AttackHigh();
        _actionFrameCounter = 0;
    }

    private void StartLowAttack()
    {
        fighterStance.ChangeStance(FighterStances.Crouching);
        fighterAction.ChangeAction(FighterActions.Attacking);
        ExecutingAction = () => ExecuteFighterMove(fighterMoveLowAttack);
        animationController.AttackLow();
        _actionFrameCounter = 0;
    }
    
    internal void Sway()
    {
        animationController.Sway();
    }

    

    private void FixedUpdate()
    {
        ExecutingAction?.Invoke();
    }
    
    private void DashAction()
    {
        _actionFrameCounter -= Time.fixedDeltaTime;

        fighterRigidbody2D.MovePosition(fighterRigidbody2D.position * dashDistance * Time.fixedDeltaTime);

        if (_actionFrameCounter <= 0)
        {
            fighterAction.ChangeAction(FighterActions.Idle);
        }
    }
    
    private void MoveAction()
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

    private void IdleAction() {}

    private void ExecuteFighterMove(FighterMove move)
    {
        Debug.Log($"Executing Fighter Move: {move.transform.gameObject.name} + { _actionFrameCounter }");

        if (_actionFrameCounter == 0)
        {
            Debug.Log($"Move Startup + {move.StartupFrames}");
            move.actualPhase = MovePhase.Startup;
        }

        else if (_actionFrameCounter >= move.StartupFrames && move.actualPhase == MovePhase.Startup)
        {
            Debug.Log($"Move Active + {move.ActiveFrames}");

            move.actualPhase = MovePhase.Active;
            move.EnableHurtbox();
        }

        else if (_actionFrameCounter >= move.ActiveFrames && move.actualPhase == MovePhase.Active)
        {
            Debug.Log($"Move Recovery + {move.RecoveryFrames}");

            move.actualPhase = MovePhase.Recovery;
            move.DisableHurtbox();
        }

        _actionFrameCounter ++;
        
        if (_actionFrameCounter >= move.RecoveryFrames && move.actualPhase == MovePhase.Recovery)
        {
            Debug.Log("Move Finished");

            fighterAction.ChangeAction(FighterActions.Idle);
            fighterStance.ChangeStance(FighterStances.Standing);
            ExecutingAction = IdleAction;
        }
    }
    
    internal void HitCheck(Height hitHeight)
    {
        if (hasBeenHit) return;
        if (hitHeight == Height.High)
        {
            if (fighterAction.IsAbleToBlock())
            {
                animationController.BlockHigh();
                Debug.Log("high block");
            }
            else
            {
                animationController.LaunchedWhileStanding();
                gameManagerEventChannel.OnPlayerHit(fighterID);
                hasBeenHit = true;
            }
        }

        if (hitHeight == Height.Low)
        {
            if (fighterStance.IsCrouching())
            {
                if (fighterAction.IsAbleToBlock())
                {
                    animationController.BlockLow();
                    Debug.Log("low block");
                }
                else
                {
                    animationController.KnockedOutWhileCrouching();
                    hasBeenHit = true;
                }
               
            }

            if (fighterStance.IsStanding())
            {
                animationController.KnockedOutWhileStanding();
                gameManagerEventChannel.OnPlayerHit(fighterID);
                hasBeenHit = true;
            }
        }
    }
}