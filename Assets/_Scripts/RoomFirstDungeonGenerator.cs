using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        tileMapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor,tileMapVisualizer);

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
