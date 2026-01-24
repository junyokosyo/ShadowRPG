using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        CameraEvents.CameraReady?.Invoke(Camera.main);
    }
}
