using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    public Transform Player;
    public Tilemap CollisionTilemap;
    public Grid TileGrid;
    public int WidthChunks, HeightChunks;

    List<Vector2Int> RenderedChunks;
    Vector2 Offset;

    public Tile full;
    public Tile grass;

    public bool WorldGeneratedFirstTime;
    public float IsGround;

    public GameObject AlterPrefab;
    public GameObject AnimalPrefab;

    public List<Match> Matches;

    private void Awake()
    {
        RenderedChunks = new List<Vector2Int>();
        var seed = StartGame.Seed;

        if (seed == "" || seed == null)
        {
            SceneManager.LoadScene(0);
            return;
        }

        var parts = Array.ConvertAll(seed.Split(','), a => float.Parse(a));
        Offset = new Vector2(parts[0], parts[1]);
    }

    void Update()
    {
        var chunkSize = 8;

        var playerPosition = Player.position;
        var playerLocationGrid = CollisionTilemap.WorldToCell(new Vector3((int)playerPosition.x, (int)playerPosition.y, 0));
        var playerChunk = new Vector2Int((playerLocationGrid.x / chunkSize), playerLocationGrid.y / chunkSize);
        var pos = new Vector2Int(playerChunk.x - WidthChunks, playerChunk.y - HeightChunks);
        var pos2 = new Vector2Int(pos.x + WidthChunks * 2, pos.y + HeightChunks * 2);

        for (var chunkY = pos.y; chunkY <= pos2.y; chunkY++)
        {
            for (var chunkX = pos.x; chunkX <= pos2.x; chunkX++)
            {
                var cur = new Vector2Int(chunkX, chunkY);
                if (!RenderedChunks.Contains(cur))
                {
                    for (var y = chunkY * chunkSize; y <= (chunkY * chunkSize) + chunkSize; y++)
                    {
                        for (var x = chunkX * chunkSize; x <= (chunkX * chunkSize) + chunkSize; x++)
                        {
                            var noise = GetNoise(x, y);
                            var hasBlock = noise < IsGround;
                            var hasBlockAbove = GetNoise(x, y + 1) < IsGround;
                            if (hasBlock)
                            {
                                CollisionTilemap.SetTile(new Vector3Int(x, y, 0), hasBlockAbove ? full : grass);

                                var remaining = (noise * 100) - Math.Truncate(noise * 100);

                                // 3%
                                if (remaining >= 0 && !hasBlockAbove)
                                {
                                    var alter = Instantiate(AlterPrefab);
                                    alter.transform.position = CollisionTilemap.CellToWorld(new Vector3Int(x, y + 1, 0));
                                    alter.transform.Translate(0.5f, 0.5f, 0);
                                }

                                //10%
                                if (remaining >= 0 && !hasBlockAbove)
                                {
                                    for (var i = 0; i < 10; i++)
                                    {
                                        var animal = Instantiate(AnimalPrefab);
                                        animal.transform.position = CollisionTilemap.CellToWorld(new Vector3Int(x, y + 1, 0));
                                        animal.transform.Translate(0.5f, 0.5f, 0);
                                    }
                                }
                            }
                        }
                    }

                    RenderedChunks.Add(cur);
                }
            }
        }
        WorldGeneratedFirstTime = true;
    }

    public float GetNoise(float x, float y)
    {
        return Mathf.PerlinNoise(x * 0.15f + Offset.x, y * 0.15f + Offset.y);
    }
}

[Serializable]
public class Match : System.Object
{
    public Tile TileToUse;
    public bool[] MatchingBools;
}