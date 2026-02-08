using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    public static System.Action<Camera> CameraReady;

    public void RaiseCameraReady(Camera cam)
    {
        CameraReady?.Invoke(cam);
    }
}
