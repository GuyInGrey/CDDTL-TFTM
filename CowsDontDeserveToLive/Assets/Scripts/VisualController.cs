using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualController : MonoBehaviour 
{
    public Sprite NormalSprite;
    public Sprite HoldingSprite;
    public SpriteRenderer Renderer;
    public PlayerController Player;
	
    void Update () 
    {
        if (Player.hasAnimal)
        {
            Renderer.sprite = HoldingSprite;
        }
        else
        {
            Renderer.sprite = NormalSprite;
        }
    }
}