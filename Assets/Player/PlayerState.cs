using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerControlState ControlState { get; private set; }
    public PlayerActionState ActionState { get; private set; }

    public bool IsMoving { get; private set; }

    public void SetMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }
}
