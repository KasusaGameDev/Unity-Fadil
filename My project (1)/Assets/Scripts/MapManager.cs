using UnityEngine;
using UnityEngine.UI;

public class ProgressGradeUI : MonoBehaviour
{
    [SerializeField] Text[] gradeTexts = new Text[8];
    [SerializeField] Text[] poinTexts = new Text[8];
    [SerializeField] Text averageGradeText;
    public GameObject munculkan;

    void Update()
    {
        if (PlayerProgress.Instance == null) return;

        int[] points = PlayerProgress.Instance.points;

        for (int i = 0; i < gradeTexts.Length; i++)
        {
            if (gradeTexts[i] == null) continue;

            int score = points[i];
            poinTexts[i].text = score.ToString();
            gradeTexts[i].text = GetGrade(score);
        }
        if (averageGradeText == null) return;

        int total = 0;

        for (int i = 0; i < points.Length; i++)
        {
            total += points[i];
        }

        float average = total / (float)points.Length;
        averageGradeText.text = GetGrade(Mathf.RoundToInt(average));
    }

    string GetGrade(int score)
    {
        if (score >= 90) return "A";
        if (score >= 80) return "B";
        if (score >= 70) return "C";
        return "";
    }

    public void munculin()
    {
        munculkan.gameObject.SetActive(true);
    }
}
