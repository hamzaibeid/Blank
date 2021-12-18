using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    private SimpleRandomWalkSO randomWalkParameters;




    protected override void RunProcidularGenerator()
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk();
        tileMapVisualizer.clear();
        tileMapVisualizer.PaintFloorTiles(floorPosition);


    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.itterations; i++)
        {
            var path = ProceduralGenerationAlgorithims.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParameters.StartRandomlyEachItteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
        }
        return floorPositions;
    }

    
}
