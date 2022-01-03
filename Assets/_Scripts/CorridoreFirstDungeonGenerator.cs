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
        CorridoreFirstGeneration();
    }

    private void CorridoreFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosintions = new HashSet<Vector2Int>();
        CreateCoridores(floorPositions,potentialRoomPosintions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPosintions);
        //create rooms and corridores
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds,roomPositions);
        floorPositions.UnionWith(roomPositions);
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        //Creating rooms at dead ends
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters,position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {

        List<Vector2Int> deadEnds = new List<Vector2Int>();
        //finding dead ends
        foreach (var position in floorPositions)
        {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighborsCount++;
                    
                }

            }
            if(neighborsCount == 1)
            {
                deadEnds.Add(position);
            }

        }
        return deadEnds;
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
