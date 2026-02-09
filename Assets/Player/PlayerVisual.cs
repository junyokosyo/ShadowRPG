using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private SpriteRenderer sprite;

    private void LateUpdate()
    {
        sprite.flipX = !state.FacingRight;
    }
}
