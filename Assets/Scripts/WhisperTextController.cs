using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class WhisperTextController : MonoBehaviour
{
    public TextMeshProUGUI whisperText;
    public CanvasGroup canvasGroup;
    public AudioSource whisperAudio;

    public string[] onScreenTexts;
    private List<string> shuffledTexts = new List<string>();
    private int currentTextIndex = 0;

    public float fadeSpeed = 1f;
    public float displayDuration = 3f;
    public float cyclePause = 5f;
    public float audioDuration = 32f;
    public float[] textTriggerInterval = { 3.5f, 11.5f, 20f, 28f };
    public float ActualstartTimeInSeconds = 20f; // <- Start time of the whispers, let the player sink in. 
    public float endTimeInSeconds = 120f; // <- Adjust as needed (2 minutes = 120s)
    private int nextTextIndex = 0;
    private float audioStartTime;
    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private bool isPaused = true;
    private float globalStartTime;
    private bool hasEndedPermanently = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
        ShuffleTexts();
    }

    void Update()
    {
        if (isPaused || hasEndedPermanently) return;

        if (Time.time - globalStartTime >= endTimeInSeconds)
        {
            Debug.Log("Whispers have reached their end time and will stop permanently.");
            StopWhispers();
            hasEndedPermanently = true;
            return;
        }
        float elapsed = Time.time - audioStartTime;

        if (nextTextIndex < textTriggerInterval.Length && elapsed >= textTriggerInterval[nextTextIndex])
        {
            ShowNextShuffledText();
            nextTextIndex++;
        }

        if (elapsed >= audioDuration)
        {
            StartCoroutine(RestartCycleAfterPause());
        }

        if (isFadingIn)
        {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;
                isFadingIn = false;
                Invoke(nameof(StartFadeOut), displayDuration);
            }
        }

        if (isFadingOut)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0f)
            {
                canvasGroup.alpha = 0f;
                isFadingOut = false;
            }
        }
    }

    public void StartAudioCycle()
    {
        globalStartTime = Time.time;
        StartCoroutine(DelayedStartCoroutine()); // Delay logic moved here
    }

    private IEnumerator DelayedStartCoroutine()
    {
        yield return new WaitForSeconds(ActualstartTimeInSeconds); // <-- DELAY TIME
        isPaused = false;
        audioStartTime = Time.time;
        nextTextIndex = 0;

        if (whisperAudio != null)
            whisperAudio.Play();
    }

    public void PauseWhispers()
    {
        isPaused = true;
        if (whisperAudio.isPlaying)
            whisperAudio.Pause();
    }

    public void ResumeWhispers()
    {
        isPaused = false;
        if (!whisperAudio.isPlaying)
            whisperAudio.Play();
    }

    public void StopWhispers()
    {
        isPaused = true;
        if (whisperAudio.isPlaying)
            whisperAudio.Stop();
    }

    void ShuffleTexts()
    {
        shuffledTexts = new List<string>(onScreenTexts);
        for (int i = 0; i < shuffledTexts.Count; i++)
        {
            string temp = shuffledTexts[i];
            int randomIndex = Random.Range(i, shuffledTexts.Count);
            shuffledTexts[i] = shuffledTexts[randomIndex];
            shuffledTexts[randomIndex] = temp;
        }
        currentTextIndex = 0;
    }

    void ShowNextShuffledText()
    {
        if (shuffledTexts.Count == 0) return;

        if (currentTextIndex >= shuffledTexts.Count)
        {
            ShuffleTexts();
        }

        string chosenText = shuffledTexts[currentTextIndex];
        currentTextIndex++;

        whisperText.text = chosenText;

        float randomRangeX = Random.Range(-300f, 300f);
        float randomRangeY = Random.Range(-150f, 150f);
        whisperText.rectTransform.anchoredPosition = new Vector2(randomRangeX, randomRangeY);

        isFadingIn = true;
    }

    void StartFadeOut()
    {
        isFadingOut = true;
    }

    IEnumerator RestartCycleAfterPause()
    {
        isPaused = true;
        whisperAudio.Stop();
        yield return new WaitForSeconds(cyclePause);

        //Prevent restarting if ed time has passed
        if (hasEndedPermanently) yield break;
        StartAudioCycle();
    }
}
