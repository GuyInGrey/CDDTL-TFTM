using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour 
{
    public LifeManager Life;
    public Text Text;

    void Update () 
    {
        Text.text = Life.Score.ToString("0.0");
    }
}