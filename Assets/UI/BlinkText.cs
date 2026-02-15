using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float minAlpha = 0.4f;
    [SerializeField] private float maxAlpha = 1f;

    void Update()
    {
        // -1〜1 → 0〜1 に変換
        float t = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f;

        // イージング（より滑らか）
        t = t * t * (3f - 2f * t);  // SmoothStep的補間

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

}
