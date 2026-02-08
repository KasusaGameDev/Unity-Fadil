using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress Instance;

    public int level = 1;
    public bool ismale = true;

    // 8 jenis poin
    public int[] points = new int[8];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Void yang bisa dipanggil dari script lain
    public void CheckLevelProgress()
    {
        level = 1; // reset dulu ke level awal

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] >= 70)
            {
                level = i + 2;
            }
            else
            {
                break; // berhenti kalau syarat tidak terpenuhi
            }
        }
    }
}
