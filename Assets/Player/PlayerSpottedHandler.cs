using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSpottedHandler : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;
    //[SerializeField] private Volume postProcessVolume;
    private bool hasSpottedEffectPlayed = false;

    void Start()
    {
        playerState.OnSpottedEvent += HandleSpotted;
    }

    void OnDestroy()
    {
        playerState.OnSpottedEvent -= HandleSpotted;
    }

    void HandleSpotted()
    {
        Debug.Log("演出開始");

        if (animator != null &&hasSpottedEffectPlayed==false)
        {
            animator.SetTrigger("Spotted");
            hasSpottedEffectPlayed = true;
            playerState.SetActionLocked(true);
        }
    }
}
