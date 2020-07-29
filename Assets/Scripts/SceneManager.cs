using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void toStart()
    {
        Application.LoadLevel("Hallway");
    }

    public void toStory()
    {
        Application.LoadLevel("Story");
    }

    public void toMenu()
    {
        Application.LoadLevel("Start");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
