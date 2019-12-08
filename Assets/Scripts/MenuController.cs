using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;

    private void Awake()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }


    public string playScene;

    public void Play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void Settings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}