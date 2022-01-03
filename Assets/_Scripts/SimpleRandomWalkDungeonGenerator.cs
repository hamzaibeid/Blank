using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;





    protected override void RunProcidularGenerator()
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk(randomWalkParameters,startPos);
        tileMapVisualizer.clear();
        tileMapVisualizer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition,tileMapVisualizer);

    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters,Vector2Int position)
    {

        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.itterations; i++)
        {
            var path = ProceduralGenerationAlgorithims.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.StartRandomlyEachItteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
        }
        return floorPositions;
    }

    
}
