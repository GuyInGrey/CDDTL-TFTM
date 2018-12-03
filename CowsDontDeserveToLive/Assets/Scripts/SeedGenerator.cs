using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedGenerator : MonoBehaviour 
{
    public InputField SeedBox;
    public static int SeedSideLength = 5;

    public void OnClick()
    {
        var max = int.Parse("1" + new string('0', SeedSideLength - 1));

        var rand = new System.Random(System.Guid.NewGuid().GetHashCode());
        var seed1 = rand.Next(max);
        var seed2 = rand.Next(max);
        var seedString = seed1.ToString() + "," + seed2.ToString();
        SeedBox.text = seedString;
    }
}