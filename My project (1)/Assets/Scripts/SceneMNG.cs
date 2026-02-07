using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMNG : MonoBehaviour
{
    [Tooltip("Nama scene tujuan (harus sudah ada di Build Settings)")]
    public string sceneName;

    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name belum diisi.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
