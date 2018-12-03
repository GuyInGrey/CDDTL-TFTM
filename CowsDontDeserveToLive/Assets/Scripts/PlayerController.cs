using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float FallMultiplier;
    public float LowJumpMultiplier;

    public Rigidbody2D RigidBody;
    public SpriteRenderer SpriteRenderer;
    public LifeManager GameManager;
    public Canvas StartCanvas;
    public WorldGeneration Generator;
    public Tilemap CollisionTileMap;

    [Range(1, 10)]
    public float JumpVelocity;

    [Range(1, 10)]
    public int RepeatedJumps;

    public float SpeedMultiplier;

    private int repeatCount;
    public bool hasAnimal;

    public bool hasMovedFirst;

    void Update()
    {
        if (Input.GetButtonDown("Jump") && repeatCount < RepeatedJumps)
        {
            RigidBody.velocity = Vector2.up * JumpVelocity;
            repeatCount++;
        }

        if (RigidBody.velocity.y < 0)
        {
            RigidBody.velocity += Vector2.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
        }
        else if (RigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            RigidBody.velocity += Vector2.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }

        var position = transform.position;
        var playerPos = position;
        var playerPosGrid = CollisionTileMap.WorldToCell(playerPos);
        
        while (CollisionTileMap.GetTile(playerPosGrid) != null)
        {
            transform.Translate(new Vector3(0, 1, 0));

            playerPos = transform.position;
            playerPosGrid = CollisionTileMap.WorldToCell(playerPos);
        }
    }

    private void FixedUpdate()
    {
        var toChange = (Vector2.right * Input.GetAxis("Horizontal") * SpeedMultiplier).x;
        RigidBody.position = (Vector2)transform.position + new Vector2(toChange, 0);

        var flipSprite = SpriteRenderer.flipX ? (toChange > 0.01f) : (toChange < -0.01f);
        if (flipSprite)
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag.ToLower();

        switch (tag)
        {
            case "ground":
                repeatCount = 0;
                break;
            case "animal":
                if (!hasAnimal)
                {
                    Destroy(collision.gameObject);
                    hasAnimal = true;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.tag.ToLower();

        switch (tag)
        {
            case "alter":
                if (hasAnimal)
                {
                    hasAnimal = false;
                    GameManager.AnimalCollected();
                }
                break;
            case "start":
                if (hasAnimal)
                {
                    enabled = false;
                    StartCanvas.gameObject.SetActive(true);
                }
                break;
            case "exit":
                Debug.Log("Exiting...");
                Application.Quit();
                break;
        }
    }
}