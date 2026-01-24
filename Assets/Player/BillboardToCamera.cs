using UnityEngine;

/// <summary>
/// 見た目を常にカメラ方向へ向ける
/// </summary>
public class BillboardToCamera : MonoBehaviour
{
    [SerializeField] private bool lockY = true;

    private Camera _camera;
    private Transform _camTransform;
    private Transform _self;

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
    /// カメラの方向に向き保つ
    /// </summary>
    private void LateUpdate()
    {
        if (_camTransform == null) return;

        Vector3 camPos = _camTransform.position;

        if (lockY)
            camPos.y = _self.position.y;

        _self.LookAt(camPos);
    }

    /// <summary>
    /// 外部からカメラを注入する
    /// </summary>
    public void SetCamera(Camera cam)
    {
        _camera = cam;
        _camTransform = cam != null ? cam.transform : null;
    }
}
