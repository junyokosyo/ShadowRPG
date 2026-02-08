using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    [SerializeField] private bool lockY = true;

    private Transform _camTransform;

    private void LateUpdate()
    {
        if (_camTransform == null) return;

        Vector3 dir = _camTransform.forward;

        if (lockY)
        {
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.0001f) return;
        }

        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    public void SetCamera(Camera cam)
    {
        _camTransform = cam != null ? cam.transform : null;
    }
}
