using UnityEngine;

public class SimpleSoundManager : MonoBehaviour
{
    public static SimpleSoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioSource bgmSource; // BGM専用（Loop設定にする）

    [Header("Lists")]
    [SerializeField] private AudioClip[] seList;
    [SerializeField] private AudioClip[] bgmList; // BGMのリスト

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- SE用関数 ---
    public void PlaySEByIndex(int index)
    {
        if (seList != null && index >= 0 && index < seList.Length)
        {
            seSource.PlayOneShot(seList[index]);
        }
    }

    public void StopSE() => seSource.Stop();

    // --- BGM用関数 ---
    public void PlayBGMByIndex(int index)
    {
        if (bgmList != null && index >= 0 && index < bgmList.Length)
        {
            // 同じ曲が既に流れているなら何もしない
            if (bgmSource.clip == bgmList[index] && bgmSource.isPlaying) return;

            bgmSource.clip = bgmList[index];
            bgmSource.loop = true; // BGMは基本ループ
            bgmSource.Play();
        }
    }

    public void StopBGM() => bgmSource.Stop();

    // BGMの音量だけ下げたいとき用
    public void SetBGMVolume(float volume) => bgmSource.volume = Mathf.Clamp01(volume);
}