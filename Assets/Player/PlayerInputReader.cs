using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    public Vector2 MoveInput { get; private set; }
    public bool IsDiving { get; private set; }
    public event Action<bool> OnDiveChanged;
    private InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMoveCanceled;
        input.Player.Dive.performed += OnDive;
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMoveCanceled;
        input.Player.Dive.performed -= OnDive;
        input.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        MoveInput = Vector2.zero;
    }

    private void OnDive(InputAction.CallbackContext ctx)
    {
        if (playerState.IsActionLocked)return; // 連打防止
        
            IsDiving = !IsDiving;
            OnDiveChanged?.Invoke(IsDiving);
            if (IsDiving) { playerState.SetActionLocked(true); }
        
    }
}
