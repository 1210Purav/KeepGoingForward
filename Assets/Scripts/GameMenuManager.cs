using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameplayUI; // Optional if you add one later
    public WhisperTextController whisperController;

    private bool isPaused = true;

    void Start()
    {
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        if (gameplayUI) gameplayUI.SetActive(false);

        // Disable whispers at start
        if (whisperController != null)
            whisperController.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!mainMenuPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        if (gameplayUI) gameplayUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;

        if (whisperController != null)
        {
            whisperController.gameObject.SetActive(true);
            whisperController.StartAudioCycle();
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (whisperController != null)
        {
            whisperController.PauseWhispers();
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (whisperController != null)
        {
            whisperController.ResumeWhispers();
        }
    }

    public void BackToMenu()
    {
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        if (gameplayUI) gameplayUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        if (whisperController != null)
        {
            whisperController.StopWhispers();
            whisperController.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
