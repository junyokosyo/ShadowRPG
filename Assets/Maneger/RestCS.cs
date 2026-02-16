using UnityEngine;

public class RestCS: MonoBehaviour
{
    public void Reset()
    {
        Debug.Log("リセット呼ばれた");
        if (SimpleSoundManager.Instance != null)
        {
            SimpleSoundManager.Instance.StopSE();
        }
        // シーンリロード
        UnityEngine.SceneManagement.SceneManager
            .LoadScene(UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().buildIndex);
    }
}
