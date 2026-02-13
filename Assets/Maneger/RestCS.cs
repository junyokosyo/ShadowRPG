using UnityEngine;

public class RestCS: MonoBehaviour
{
    public void Reset()
    {
        Debug.Log("リセット呼ばれた");

        // シーンリロード
        UnityEngine.SceneManagement.SceneManager
            .LoadScene(UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().buildIndex);
    }
}
