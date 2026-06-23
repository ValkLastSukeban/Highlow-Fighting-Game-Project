using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [Header("Event Channel")] [SerializeField]
    private GameManagerEventChannel gameManagerEventChannel;

    [Header("Script Reference")] [SerializeField]
    private FighterMoves fighterMoves;

    [Header("Player ID")] [SerializeField] private PlayerID playerID = PlayerID.Player1;

    private PlayerInputSystem _playerInputSystem;
    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        gameManagerEventChannel.onRoundStart += DisableInputsByTime;
        fighterMoves.SetFighterID(playerID);

        if (playerID == PlayerID.Player1)
        {
            _playerInputSystem.Fighter1.Move.performed += OnMoveInput;
            _playerInputSystem.Fighter1.Move.canceled += OnMoveInput;
            _playerInputSystem.Fighter1.AttackHigh.started += OnAttackHighInput;
            _playerInputSystem.Fighter1.AttackLow.started += OnAttackLowInput;
            _playerInputSystem.Fighter1.ForwardDash.started += OnRightDashInput;
            _playerInputSystem.Fighter1.BackDash.started += OnLeftDashInput;
            _playerInputSystem.Fighter1.Down.started += OnDown;
            _playerInputSystem.Fighter1.Down.canceled += OnDown;
            _playerInputSystem.Fighter1.Sway.started += OnSway;
        }
        else
        {
            _playerInputSystem.Fighter2.Move.performed += OnMoveInput;
            _playerInputSystem.Fighter2.Move.canceled += OnMoveInput;
            _playerInputSystem.Fighter2.AttackHigh.started += OnAttackHighInput;
            _playerInputSystem.Fighter2.AttackLow.started += OnAttackLowInput;
            _playerInputSystem.Fighter2.ForwardDash.started += OnRightDashInput;
            _playerInputSystem.Fighter2.BackDash.started += OnLeftDashInput;
            _playerInputSystem.Fighter2.Down.started += OnDown;
            _playerInputSystem.Fighter2.Down.canceled += OnDown;
            _playerInputSystem.Fighter2.Sway.started += OnSway;

        }
    }

    private void OnDisable()
    {
        gameManagerEventChannel.onRoundStart -= DisableInputsByTime;

        if (playerID == PlayerID.Player1)
        {
            _playerInputSystem.Fighter1.Move.performed -= OnMoveInput;
            _playerInputSystem.Fighter1.Move.canceled -= OnMoveInput;
            _playerInputSystem.Fighter1.AttackHigh.started -= OnAttackHighInput;
            _playerInputSystem.Fighter1.AttackLow.started -= OnAttackLowInput;
            _playerInputSystem.Fighter1.ForwardDash.started -= OnRightDashInput;
            _playerInputSystem.Fighter1.BackDash.started -= OnLeftDashInput;
            _playerInputSystem.Fighter1.Down.started -= OnDown;
            _playerInputSystem.Fighter1.Down.canceled -= OnDown;
            _playerInputSystem.Fighter1.Sway.started -= OnSway;

        }
        else
        {
            _playerInputSystem.Fighter2.Move.performed -= OnMoveInput;
            _playerInputSystem.Fighter2.Move.canceled -= OnMoveInput;
            _playerInputSystem.Fighter2.AttackHigh.started -= OnAttackHighInput;
            _playerInputSystem.Fighter2.AttackLow.started -= OnAttackLowInput;
            _playerInputSystem.Fighter2.ForwardDash.started -= OnRightDashInput;
            _playerInputSystem.Fighter2.BackDash.started -= OnLeftDashInput;
            _playerInputSystem.Fighter2.Down.started -= OnDown;
            _playerInputSystem.Fighter2.Down.canceled -= OnDown;
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
    
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.performed) fighterMoves.StartMoving(context.ReadValue<float>());
        if(context.canceled) fighterMoves.StopMoving();
    }

    private void OnDown(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     fighterMovesManager.Crouch();
        // }
        // else if (context.canceled)
        // {
        //     fighterMovesManager.StandUp();
        // }
    }

    private void OnAttackHighInput(InputAction.CallbackContext context)
    {
        // fighterMovesManager.ExecuteAttackHigh();
    }

    private void OnAttackLowInput(InputAction.CallbackContext context)
    {
        // fighterMovesManager.ExecuteAttackLow();
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