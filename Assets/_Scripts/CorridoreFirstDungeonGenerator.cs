using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridoreFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridoreLength = 14,coridoreCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;
    [SerializeField]
    public SimpleRandomWalkSO roomGenerationParameters;

    

    protected override void RunProcidularGenerator()
    {
        CorridoreFirstDungeonGeneration();
    }

    private void CorridoreFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        CreateCoridores(floorPositions);
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tileMapVisualizer);
    }

    private void CreateCoridores(HashSet<Vector2Int> floorPositions)
    {
        var currentPosition = startPos;
        for (int i = 0; i < coridoreCount; i++)
        {
            var corridore = ProceduralGenerationAlgorithims.RandomWalkCorridore(currentPosition,corridoreLength);
            currentPosition = corridore[corridore.Count - 1];
            floorPositions.UnionWith(corridore);
        }
    }
}
