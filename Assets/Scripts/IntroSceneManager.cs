using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    private int current = 0;
    public List<GameObject> slides;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            LoadNext();
        }
    }

    private void LoadNext()
    {
        current++;
        if (current < slides.Count)
        {
            slides[current--].SetActive(false);
            slides[current].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}