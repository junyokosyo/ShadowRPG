using UnityEngine;


public class BillboardToCamera : MonoBehaviour
{
    [SerializeField] private bool lockY = true;

    private Transform _self;
    private Transform _camTransform;

    private void Awake()
    {
        _self = transform;
    }

    private void OnEnable()
    {
        CameraEvents.CameraReady += SetCamera;
    }

    private void OnDisable()
    {
        CameraEvents.CameraReady -= SetCamera;
    }

    /// <summary>
    /// カメラの方向を向く
    /// </summary>
    private void LateUpdate()
    {
        if (_camTransform == null) return;

        Vector3 camPos = _camTransform.position;
        if (lockY) camPos.y = _self.position.y;
        
        _self.LookAt(camPos);
    }

    /// <summary>
    /// カメラを設定する
    /// </summary>
    public void SetCamera(Camera cam)
    {
        _camTransform = cam != null ? cam.transform : null;
    }
}
