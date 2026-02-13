using UnityEngine;

[RequireComponent(typeof(Light))]
public class SpotlightVision : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private float angleOffset = 7f;
    private Light spotLight;

    void Awake()
    {
        spotLight = GetComponent<Light>();
    }

    void Update()
    {
        if (playerState.IsOnSpotted == false)
        {
            if (playerState == null) return;
            if (!playerState.IsDetecctable) return;

            Vector3 toPlayer = playerState.transform.position - transform.position;
            float distance = toPlayer.magnitude;

            if (distance > spotLight.range) return;

            float angle = Vector3.Angle(transform.forward, toPlayer);
            float effectiveAngle = (spotLight.spotAngle * 0.5f)- angleOffset;
            if (angle <= effectiveAngle)
            {
                Ray ray = new Ray(transform.position, toPlayer.normalized);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, spotLight.range))
                {
                    if (hit.transform == playerState.transform)
                    {
                        playerState.OnSpotted();
                    }
                }
            }

        }

    }
}
