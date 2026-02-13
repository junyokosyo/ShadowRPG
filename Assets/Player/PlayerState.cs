using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    public bool FacingRight { get; private set; } = true;
    public bool IsDiving { get; private set; }
    public bool IsDetecctable { get;private set; }=true;


    /// <summary>
    /// プレイヤーに動いているかどうかを設定する
    /// </summary>
    /// 
    public void SetMoving(bool moving)
    {
        IsMoving = moving;
    }
    /// <summary>
    /// 左右の向きだけ更新する
    /// </summary>
    public void SetFacing(float x)
    {
        if (x > 0.01f)
            FacingRight = true;
        else if (x < -0.01f)
            FacingRight = false;
    }
    /// <summary>
    /// プレイヤーがダイビングしているかどうかを設定する
    /// </summary>
    /// <param name="diving"></param>
    public void SetDiving(bool diving)
    {
        IsDiving = diving;
        UpdateDetectable();
    }
    private void UpdateDetectable()
    {
        IsDetecctable = !IsDiving;
    }
}
