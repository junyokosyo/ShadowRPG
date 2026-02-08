using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerState state;

    private void Update()
    {
        animator.SetBool("IsMoving", state.IsMoving);
    }
}
