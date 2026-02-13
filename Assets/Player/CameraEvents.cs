using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    public static System.Action<Camera> CameraReady;

    // カメラが準備できたときに呼び出すイベント
    public void RaiseCameraReady(Camera cam)
    {
        CameraReady?.Invoke(cam);
    }
}
