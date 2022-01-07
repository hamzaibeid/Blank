using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    
    [SerializeField]
    private int dungeonWidth = 20, dungeonheight = 20;
    
    [SerializeField]
    [Range(0,10)]
    private int offSet = 1;

    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void RunProcidularGenerator()
    {
        CreateRooms();

    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithims.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos,
        new Vector3Int(dungeonWidth,dungeonheight,0)),minRoomWidth,minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        HashSet<Vector2Int> corridores = ConnectRooms(roomCenters);
        floor.UnionWith(corridores); 
        tileMapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor,tileMapVisualizer);

    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridores = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0,roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);
        while(roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter,roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridore = CreateCorridore(currentRoomCenter,closest);
            currentRoomCenter = closest;
            corridores.UnionWith(newCorridore);
        }
        return corridores;
    }

    private HashSet<Vector2Int> CreateCorridore(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridore = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridore.Add(position);
        while (position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridore.Add(position);
            
        }
        while (position.x != destination.x) 
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x< position.x)
            {
                position += Vector2Int.left;

            }
            corridore.Add(position);
        }
        return corridore;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float Distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position,currentRoomCenter);
            if (currentDistance < Distance)
            {
                Distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offSet; col <room.size.x - offSet;col++)
            {
                for (int row = offSet; row <room.size.y-offSet; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                    //to decorate save each hash set seperatly
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
