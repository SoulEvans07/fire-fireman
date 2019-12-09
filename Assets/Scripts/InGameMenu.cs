using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    public bool isGameOver;
    public string nextLevel;
    public string mainMenu = "MainMenu";

    private void Awake() {
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Update() {
        if (Input.GetButton("Cancel") && !isGameOver) {
            Pause();
        }
    }

    public void MainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu);
    }

    public void Reset() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevel);
    }

    public void Pause() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void SettingsMenu() {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void BackToPauseMenu() {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Continue() {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver() {
        Time.timeScale = 0;
        losePanel.SetActive(true);
        isGameOver = true;
    }

    public void Win() {
        Time.timeScale = 0;
        winPanel.SetActive(true);
        isGameOver = true;
    }
}