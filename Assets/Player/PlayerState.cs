using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    public bool FacingRight { get; private set; } = true;
    public bool IsDiving { get; private set; }
    public void SetMoving(bool moving)
    {
        IsMoving = moving;
    }

    /// <summary>
    /// ¶‰E‚ÌŒü‚«‚¾‚¯XV‚·‚é
    /// </summary>
    public void SetFacing(float x)
    {
        if (x > 0.01f)
            FacingRight = true;
        else if (x < -0.01f)
            FacingRight = false;
    }
    public void SetDiving(bool diving)
    {
        IsDiving = diving;
    }
}
