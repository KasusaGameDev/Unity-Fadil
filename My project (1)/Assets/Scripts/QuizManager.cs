using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        [TextArea] public string questionText;
        public string correctAnswer;
        public string wrongAnswer1;
        public string wrongAnswer2;
        public string wrongAnswer3;
    }

    [Header("QUESTION DATA")]
    public List<Question> questions = new List<Question>();

    [Header("UI")]
    public Text questionTextUI;
    public Button[] answerButtons; // 4 button
    public Text timerText;

    [Header("RESULT POPUP")]
    public Image popupResult;
    public Text scoreText;
    public Text timeLeftText;
    public Text rightAnswerText;
    public Text gradeText;

    [Header("TIMER")]
    public float totalTime = 60f;
    public float warningTime = 10f;

    [Header("AUDIO")]
    public AudioSource audioSource;
    public AudioClip chooseClip;

    private int currentQuestionIndex = 0;
    private int correctCount = 0;

    private float currentTime;
    private float startTime;

    private bool isFinished = false;
    private bool lastTenSecondsTriggered = false;


    [Header("TYPEWRITER")]
    public float typingSpeed = 0.03f;
    void Start()
    {
        popupResult.gameObject.SetActive(false);

        currentTime = totalTime;
        startTime = Time.time;

        ShowQuestion();
    }

    void Update()
    {
        if (isFinished) return;
        currentTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (currentTime <= warningTime && !lastTenSecondsTriggered)
        {
            lastTenSecondsTriggered = true;
            OnLastTenSeconds();
        }

        if (currentTime <= 0)
        {
            FinishQuiz();
        }
    }

    // =====================
    // QUIZ FLOW
    // =====================

    void ShowQuestion()
{
    if (currentQuestionIndex >= questions.Count)
    {
        FinishQuiz();
        return;
    }

    Question q = questions[currentQuestionIndex];

    List<string> answers = new List<string>()
    {
        q.correctAnswer,
        q.wrongAnswer1,
        q.wrongAnswer2,
        q.wrongAnswer3
    };

    Shuffle(answers);

    for (int i = 0; i < answerButtons.Length; i++)
    {
        string answer = answers[i];
        Button btn = answerButtons[i];

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => SelectAnswer(answer));
    }

    StopAllCoroutines();
    StartCoroutine(ShowQuestionSequence(q, answers));
}


    void SelectAnswer(string selectedAnswer)
    {
        if (isFinished) return;

        if (audioSource && chooseClip)
            audioSource.PlayOneShot(chooseClip);

        if (selectedAnswer == questions[currentQuestionIndex].correctAnswer)
            correctCount++;

        currentQuestionIndex++;
        ShowQuestion();
    }

    void FinishQuiz()
{
    if (isFinished) return;
    isFinished = true;

    float usedTime = Time.time - startTime;
    float timeLeft = Mathf.Max(0, totalTime - usedTime);

    int score = Mathf.RoundToInt((float)correctCount / questions.Count * 100f);

    popupResult.gameObject.SetActive(true);
    // AnimateScale(popupResult.transform, 0.15f);

    StopAllCoroutines();

    StartCoroutine(AnimateScore(score));
    StartCoroutine(AnimateTimeLeft(totalTime, timeLeft));
    StartCoroutine(AnimateRightAnswer(correctCount, questions.Count));

    gradeText.text = GetGrade(score);

    Debug.Log("FINAL SCORE: " + score);
}


    // =====================
    // EFFECT HOOK
    // =====================

    void OnLastTenSeconds()
    {
        // isi sendiri:
        // - play warning sound
        // - anim timer merah / putih
    }

    // =====================
    // UTIL
    // =====================

    void AnimateScale(Transform target, float duration)
    {
        StopCoroutine("ScaleTween");
        StartCoroutine(ScaleTween(target, Vector3.zero, Vector3.one, duration));
    }

    IEnumerator ScaleTween(Transform target, Vector3 from, Vector3 to, float duration)
    {
        float t = 0f;
        target.localScale = from;

        while (t < duration)
        {
            t += Time.deltaTime;
            target.localScale = Vector3.Lerp(from, to, t / duration);
            yield return null;
        }

        target.localScale = to;
    }

    string GetGrade(int score)
    {
        if (score >= 91) return "A";
        if (score >= 81) return "B";
        if (score >= 71) return "C";
        if (score >= 61) return "D";
        if (score >= 41) return "E";
        return "F";
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }


    IEnumerator TypeText(Text uiText, string fullText)
{
    uiText.text = "";
    foreach (char c in fullText)
    {
        uiText.text += c;
        yield return new WaitForSeconds(typingSpeed);
    }
}
IEnumerator ShowQuestionSequence(Question q, List<string> answers)
{
    // Pertanyaan
    yield return StartCoroutine(TypeText(questionTextUI, q.questionText));

    // Jawaban 1–4
    for (int i = 0; i < answerButtons.Length; i++)
    {
        Text btnText = answerButtons[i].GetComponentInChildren<Text>();
        yield return StartCoroutine(TypeText(btnText, answers[i]));
    }
}

IEnumerator AnimateScore(int targetScore, float duration = 1f)
{
    float t = 0f;
    int start = 0;

    while (t < duration)
    {
        t += Time.deltaTime;
        int value = Mathf.RoundToInt(Mathf.Lerp(start, targetScore, t / duration));
        scoreText.text = value.ToString() + " Poin";
        yield return null;
    }

    scoreText.text = targetScore.ToString() + " Poin";
}

IEnumerator AnimateTimeLeft(float fromTime, float toTime, float duration = 1f)
{
    float t = 0f;

    while (t < duration)
    {
        t += Time.deltaTime;
        float value = Mathf.Lerp(fromTime, toTime, t / duration);
        timeLeftText.text = Mathf.CeilToInt(value) + " Detik";
        yield return null;
    }

    timeLeftText.text = Mathf.CeilToInt(toTime) + " Detik";
}


IEnumerator AnimateRightAnswer(int targetCorrect, int totalQuestion, float duration = 1f)
{
    float t = 0f;
    int start = 0;

    while (t < duration)
    {
        t += Time.deltaTime;
        int value = Mathf.RoundToInt(Mathf.Lerp(start, targetCorrect, t / duration));
        rightAnswerText.text = value + "/" + totalQuestion;
        yield return null;
    }

    rightAnswerText.text = targetCorrect + "/" + totalQuestion;
}


}
