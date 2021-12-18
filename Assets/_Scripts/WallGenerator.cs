using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
   public static void CreateWalls(HashSet<Vector2Int> floorPositions ,TileMapVisualizer tileMapVisualizer)
    {
        var BasicWallPositions = FindWallsInDirections(floorPositions,Direction2D.cardinalDirectionsList);
        foreach (var Position in BasicWallPositions)
        {
            tileMapVisualizer.PaintSingleWall(Position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int>floorPositions , List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var Position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighborPosition = Position + direction;
                if (floorPositions.Contains(neighborPosition) == false)
                    wallPositions.Add(neighborPosition);
            }
        }
        return wallPositions;
    }
}
