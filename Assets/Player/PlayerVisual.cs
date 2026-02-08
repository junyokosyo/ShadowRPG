using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;

    private Vector3 lastMoveDir = Vector3.forward;

    private void LateUpdate()
    {
        Vector3 dir = movement.MoveDirection;

        if (dir.sqrMagnitude > 0.001f)
        {
            lastMoveDir = dir.normalized;
        }

        // ここで lastMoveDir を使って
        // ・8方向Sprite切り替え
        // ・向き固定
        // ・攻撃方向
        // などができる
    }
}
