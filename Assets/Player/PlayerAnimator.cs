using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerState state;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

    private void Update()
    {
        animator.SetBool(IsMovingHash, state.IsMoving);
    }
}
