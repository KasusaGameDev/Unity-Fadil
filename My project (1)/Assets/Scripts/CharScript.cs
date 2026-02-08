using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    [SerializeField] Animator animatorInti;
    [SerializeField] Button nextBtn;
    [SerializeField] Button testBtn;
    [SerializeField] Button startBtn;
    [SerializeField] Button endBtn;

    [SerializeField] string[] levelScenes = new string[8];

    [SerializeField] GameObject cowo;
    [SerializeField] GameObject cewe;
    
    private bool jalan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerProgress.Instance != null)
        {
            int level = PlayerProgress.Instance.level;
            bool cowok = PlayerProgress.Instance.ismale;
            endBtn.gameObject.SetActive(level > 8 & !jalan);
            startBtn.gameObject.SetActive(level < 1 & !jalan);
            testBtn.gameObject.SetActive(!jalan & level > 0 & level < 9);
            nextBtn.gameObject.SetActive(!jalan & level > 0 & level < 9);
            animatorInti.SetInteger("Level", level + 1);

            cowo.gameObject.SetActive(cowok);
            cewe.gameObject.SetActive(!cowok);
        }
        
    }
    public void Next()
    {
        if (PlayerProgress.Instance != null)
        {
            PlayerProgress.Instance.CheckLevelProgress();
        }
    }
    public void ChangeGender(bool parameter)
    {
        if (PlayerProgress.Instance != null)
        {
            PlayerProgress.Instance.ismale = parameter;
        }
    }
    public void Open()
{
    if (PlayerProgress.Instance == null) return;

    int level = PlayerProgress.Instance.level;

    // valid hanya level 1–8
    if (level < 1 || level > 8) return;

    string sceneName = levelScenes[level - 1];

    if (!string.IsNullOrEmpty(sceneName))
    {
        SceneManager.LoadScene(sceneName);
    }
}


    public void Animate()
    {
        animator.SetBool("Bounce", true);
        jalan = true;
    }
    public void Stop()
    {
        
        animator.SetBool("Bounce", false);
        jalan = false;
    }
}
