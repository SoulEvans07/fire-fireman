using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string playScene;

    public void Play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}