using System;
using System.Collections;
using Event_Channel;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [Header("Event Channel")] [SerializeField]
    private GameManagerEventChannel gameManagerEventChannel;
    
    [Header("Controller ID")] [SerializeField] private ControllerID controllerID = ControllerID.Controller1;
    internal ControllerID ControllerID => controllerID;

    private PlayerInputSystem _playerInputSystem;
    
    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        gameManagerEventChannel.RoundStart += DisableInputsByTime;

        if (controllerID == ControllerID.Controller1)
        {
            _playerInputSystem.Fighter1.Move.performed += OnMoveInput;
            _playerInputSystem.Fighter1.Move.canceled += OnMoveInput;
            _playerInputSystem.Fighter1.AttackHigh.started += OnHighAttackInput;
            _playerInputSystem.Fighter1.AttackLow.started += OnLowAttackInput;
            _playerInputSystem.Fighter1.ForwardDash.started += OnRightDashInput;
            _playerInputSystem.Fighter1.BackDash.started += OnLeftDashInput;
            _playerInputSystem.Fighter1.Down.started += OnDownInput;
            _playerInputSystem.Fighter1.Down.canceled += OnDownInput;
            _playerInputSystem.Fighter1.Sway.started += OnSway;
        }
        else if (controllerID == ControllerID.Controller2)
        {
            _playerInputSystem.Fighter2.Move.performed += OnMoveInput;
            _playerInputSystem.Fighter2.Move.canceled += OnMoveInput;
            _playerInputSystem.Fighter2.AttackHigh.started += OnHighAttackInput;
            _playerInputSystem.Fighter2.AttackLow.started += OnLowAttackInput;
            _playerInputSystem.Fighter2.ForwardDash.started += OnRightDashInput;
            _playerInputSystem.Fighter2.BackDash.started += OnLeftDashInput;
            _playerInputSystem.Fighter2.Down.started += OnDownInput;
            _playerInputSystem.Fighter2.Down.canceled += OnDownInput;
            _playerInputSystem.Fighter2.Sway.started += OnSway;

        }
    }

    private void OnDisable()
    {
        gameManagerEventChannel.RoundStart -= DisableInputsByTime;

        if (controllerID == ControllerID.Controller1)
        {
            _playerInputSystem.Fighter1.Move.performed -= OnMoveInput;
            _playerInputSystem.Fighter1.Move.canceled -= OnMoveInput;
            _playerInputSystem.Fighter1.AttackHigh.started -= OnHighAttackInput;
            _playerInputSystem.Fighter1.AttackLow.started -= OnLowAttackInput;
            _playerInputSystem.Fighter1.ForwardDash.started -= OnRightDashInput;
            _playerInputSystem.Fighter1.BackDash.started -= OnLeftDashInput;
            _playerInputSystem.Fighter1.Down.started -= OnDownInput;
            _playerInputSystem.Fighter1.Down.canceled -= OnDownInput;
            _playerInputSystem.Fighter1.Sway.started -= OnSway;

        }
        else if (controllerID == ControllerID.Controller2)
        {
            _playerInputSystem.Fighter2.Move.performed -= OnMoveInput;
            _playerInputSystem.Fighter2.Move.canceled -= OnMoveInput;
            _playerInputSystem.Fighter2.AttackHigh.started -= OnHighAttackInput;
            _playerInputSystem.Fighter2.AttackLow.started -= OnLowAttackInput;
            _playerInputSystem.Fighter2.ForwardDash.started -= OnRightDashInput;
            _playerInputSystem.Fighter2.BackDash.started -= OnLeftDashInput;
            _playerInputSystem.Fighter2.Down.started -= OnDownInput;
            _playerInputSystem.Fighter2.Down.canceled -= OnDownInput;
            _playerInputSystem.Fighter2.Sway.started -= OnSway;

        }
    }

    private void DisableInputsByTime(float timeDisabled)
    {
        StartCoroutine(DisableInputsBySetTime(timeDisabled));
    }

    private IEnumerator DisableInputsBySetTime(float time)
    {
        _playerInputSystem.Disable();
        yield return new WaitForSeconds(time);
        _playerInputSystem.Enable();
    }
    
    internal event Action<float> MovePerformedAction;
    internal event Action MoveStoppedAction;
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.performed) MovePerformedAction?.Invoke(context.ReadValue<float>());
        if(context.canceled) MoveStoppedAction?.Invoke();
    }

    internal event Action CrouchStartedAction;
    internal event Action CrouchStoppedAction;
    private void OnDownInput(InputAction.CallbackContext context)
    {
        if (context.started) { CrouchStartedAction?.Invoke(); }
        else if (context.canceled) { CrouchStoppedAction?.Invoke(); }
    }


    internal event Action HighAttackAction;
    private void OnHighAttackInput(InputAction.CallbackContext context)
    {
        HighAttackAction?.Invoke();
    }

    internal event Action LowAttackAction;
    private void OnLowAttackInput(InputAction.CallbackContext context)
    {
        LowAttackAction?.Invoke();
    }

    private void OnRightDashInput(InputAction.CallbackContext context)
    {
        // fighterMovesManager.StartDashing(1);
    }

    private void OnLeftDashInput(InputAction.CallbackContext context)
    {
        // fighterMovesManager.StartDashing(-1);
    }
    
    private void OnSway(InputAction.CallbackContext context)
    {
         // fighterMovesManager.Sway();
    }

}