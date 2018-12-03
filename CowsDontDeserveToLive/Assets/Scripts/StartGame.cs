using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour 
{
    public InputField SeedInput;

    public static string Seed;

    public void OnClick()
    {
        Seed = SeedInput.text;
        Debug.Log("Starting: Seed is '" + Seed + "'");

        var c = SceneManager.GetActiveScene().buildIndex;
        if (c < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(c + 1);
        }
    }
}