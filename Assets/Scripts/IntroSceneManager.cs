using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public string next = "Level 1";
    public int current = 0;
    public List<GameObject> slides;

    private bool buttonDown = false;

    private void Update()
    {
        if (Input.anyKey)
        {
            if (!buttonDown)
            {
                buttonDown = true;
                LoadNext();
            }
        }
        else 
        {
            buttonDown = false;
        }
    }

    private void LoadNext()
    {
        current++;
        if (current < slides.Count)
        {
            slides[current-1].SetActive(false);
            slides[current].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(next);
        }
    }
}