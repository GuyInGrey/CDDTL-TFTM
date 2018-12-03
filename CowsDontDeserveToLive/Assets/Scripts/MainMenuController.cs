using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
    public Transform Background;
    public float BackgroundRotationSpeed;

    // Update is called once per frame
    void Update () 
    {
        Background.Rotate(new Vector3(0, 0, BackgroundRotationSpeed * Time.deltaTime));
    }
}