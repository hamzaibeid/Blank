using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridoreFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridoreLength = 14, coridoreCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
    
  



    protected override void RunProcidularGenerator()
    {
        CorridoreFirstDungeonGeneration();
    }

    private void CorridoreFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosintions = new HashSet<Vector2Int>();
        CreateCoridores(floorPositions,potentialRoomPosintions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPosintions);
        //create rooms and corridores
        floorPositions.UnionWith(roomPositions);
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPosintions)
    {

        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosintions.Count*roomPercent);

        //take rooms by random
        List<Vector2Int> roomToCreate = potentialRoomPosintions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach (var roomPosition in roomToCreate)
        {
            //generate rooms
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            //avoid repetition
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;

    }

    private void CreateCoridores(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPosintions)
    {
        var currentPosition = startPos;
        potentialRoomPosintions.Add(currentPosition);
        for (int i = 0; i < coridoreCount; i++)
        {
            var corridore = ProceduralGenerationAlgorithims.RandomWalkCorridore(currentPosition, corridoreLength);
            currentPosition = corridore[corridore.Count - 1];
            potentialRoomPosintions.Add(currentPosition);
            floorPositions.UnionWith(corridore);
        }
    }

}
