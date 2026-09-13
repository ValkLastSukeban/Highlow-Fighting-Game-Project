using System;
using Event_Channel;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Event Channel")]
    [SerializeField]
    private GameManagerEventChannel _gameManagerEventChannel;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _fighterRigidbody2D;

    [Header("Scripts References")]
    [SerializeField] private InputController _inputController;
    [SerializeField] private AnimationController _animationController;

    [Header("Fighter Values")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveSpeedCrouching;
    [SerializeField] private FighterStateMachine _fighterStateMachine;

    [SerializeField] private FighterID _fighterID = FighterID.Fighter1;

    [Header("Moves")]
    [SerializeField] private FighterAttack _electricUppercut;
    [SerializeField] private FighterAttack _hellSweep;
    // [SerializeField] private FighterMoveDash fighterMoveDash;
    private event Action FixedUpdateAction;
    private Vector2 _horizontalAxis;

    internal void InitializeFighter()
    {
        InitializeStateMachine();
        InitializePosition();
    }

    private void InitializeStateMachine()
    {
        _fighterStateMachine.ChangeState(FighterStates.StandIdle);
    }

    private void InitializePosition()
    {
        transform.position = _fighterID == FighterID.Fighter1
            ? -GameRules.PlayersStartingPosition
            : GameRules.PlayersStartingPosition;
    }

    private void OnEnable()
    {
        RegisterInput();
    }

    private void RegisterInput()
    {
        if (!_inputController) return;
        _inputController.MovePerformedAction += StartMoving;
        _inputController.MoveStoppedAction += StopMoving;
        _inputController.CrouchStartedAction += StartCrouching;
        _inputController.CrouchStoppedAction += StopCrouching;
        _inputController.HighAttackAction += StartHighAttack;
        _inputController.LowAttackAction += StartLowAttack;

    }

    private void OnDisable()
    {
        UnregisterInput();
    }

    private void UnregisterInput()
    {
        if (!_inputController) return;
        _inputController.MovePerformedAction -= StartMoving;
        _inputController.MoveStoppedAction -= StopMoving;
        _inputController.CrouchStartedAction -= StartCrouching;
        _inputController.CrouchStoppedAction -= StopCrouching;
        _inputController.HighAttackAction -= StartHighAttack;
        _inputController.LowAttackAction -= StartLowAttack;

    }

    private void StartMoving(float horizontalInputAxis)
    {

        if (_fighterStateMachine.FighterState != FighterStates.StandIdle) return;
        Debug.Log("StartMoving: " + horizontalInputAxis);
        FixedUpdateAction = MoveAction;
        if (horizontalInputAxis > 0)
        {
            _fighterStateMachine.ChangeState(FighterStates.WalkForward);
        }
        else if (horizontalInputAxis < 0)
        {
            _fighterStateMachine.ChangeState(FighterStates.WalkBackward);
        }
        _animationController.Movement(horizontalInputAxis);
        _horizontalAxis.x = horizontalInputAxis;
    }

    private void MoveAction()
    {
        _fighterRigidbody2D.MovePosition(_fighterRigidbody2D.position + _horizontalAxis * _moveSpeed * Time.fixedDeltaTime);
    }

    private void StopMoving()
    {
        if (!_fighterStateMachine.IsWalking()) { return; }
        Debug.Log("Stopped Moving");
        FixedUpdateAction = EmptyAction;
        _fighterStateMachine.ChangeState(FighterStates.StandIdle);
        _animationController.Movement(0);
        _horizontalAxis.x = 0;
    }

    private void StartDashing(float dashInputAxis)
    {
        FixedUpdateAction = DashAction;
        _fighterStateMachine.ChangeState(FighterStates.Dashing);
    }

    private void DashAction()
    {
    }

    private void StartCrouching()
    {
        if (_fighterStateMachine.FighterState != FighterStates.StandIdle ||
            _fighterStateMachine.FighterState == FighterStates.WalkForward ||
            _fighterStateMachine.FighterState == FighterStates.WalkBackward) return;
        FixedUpdateAction = CrouchAction;
        _fighterStateMachine.ChangeState(FighterStates.CrouchIdle);
        _animationController.Crouching();
    }
    private void CrouchAction()
    {

    }
    private void StopCrouching()
    {
        if (_fighterStateMachine.FighterState != FighterStates.CrouchIdle) return;
        FixedUpdateAction = EmptyAction;
        _fighterStateMachine.ChangeState(FighterStates.StandIdle);
        _animationController.StandingUp();
    }

    private void StartHighAttack()
    {
        if(_fighterStateMachine.IsStanding() || _fighterStateMachine.IsWalking())
        {
            Debug.Log("Start High Attack");
            FixedUpdateAction = () => AttackAction(_electricUppercut);
            _fighterStateMachine.ChangeState(FighterStates.StandAttack);
            _animationController.AttackHigh();
        }
    }

    private void StartLowAttack()
    {
        if(_fighterStateMachine.IsCrouching())
        {
            Debug.Log("Start Low Attack");
            FixedUpdateAction = () => AttackAction(_hellSweep);
            _fighterStateMachine.ChangeState(FighterStates.CrouchAttack);
            _animationController.AttackLow();
        }
    }

    internal void Sway()
    {
        _animationController.Sway();
    }

    private void FixedUpdate()
    {
        if(FixedUpdateAction != null)
        {
            FixedUpdateAction?.Invoke();
            Debug.Log("Fixed Update Action Invoked: " + FixedUpdateAction.Method.Name);

        }
    }

    private void EmptyAction() { }

    private void AttackAction(FighterAttack attack)
    {
        Debug.Log("Attack Action Invoked: " + attack.name);
        if (attack.ActualAttackPhase != AttackPhase.Ready)
        {
            Debug.Log("Attack was ready and is started");
            attack.StartAttack();
            _fighterStateMachine.ChangeState(attack.AttackHeight == Height.High ? FighterStates.StandAttack : FighterStates.CrouchAttack);
        }
        if (attack.ActualAttackPhase == AttackPhase.Finished)
        {
            Debug.Log("Attacking has Finished");
            if (attack.AttackHeight == Height.High)
            {
                _fighterStateMachine.ChangeState(FighterStates.StandIdle);
            }
            else if (attack.AttackHeight == Height.Low)
            {
                _fighterStateMachine.ChangeState(FighterStates.CrouchIdle);
            }
        }
    }

    internal bool BlockCheck(Height hitHeight)
    {
        if (_fighterStateMachine.FighterState == FighterStates.KnockedOut) return false;
        if (hitHeight == Height.High)
        {
            return HighBlockCheck();
        }
        if (hitHeight == Height.Low)
        {
            return LowBlockCheck();
        }
        return false;
    }

    private bool HighBlockCheck()
    {
        if (_fighterStateMachine.FighterState == FighterStates.WalkBackward)
        {
            _animationController.BlockHigh();
            _fighterStateMachine.ChangeState(FighterStates.StandBlockStun);
            Debug.Log("high block");
            return true;
        }
        GotHit(Height.High);
        return false;
    }
    private bool LowBlockCheck()
    {
        if (_fighterStateMachine.FighterState == FighterStates.CrouchBlocking)
        {
            _animationController.BlockLow();
            _fighterStateMachine.ChangeState(FighterStates.CrouchBlockStun);
            Debug.Log("low block");
            return true;
        }
        GotHit(Height.Low);
        return false;
    }

    private void GotHit(Height hitHeight)
    {
        _fighterStateMachine.ChangeState(FighterStates.KnockedOut);
        _gameManagerEventChannel.OnPlayerHit(_fighterID);

        if (hitHeight == Height.High)
        {
            _animationController.LaunchedWhileStanding();
        }
        else if (hitHeight == Height.Low)
        {
            _animationController.KnockedOutWhileCrouching();
        }
    }
}