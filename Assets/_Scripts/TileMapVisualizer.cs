﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap FloorTileMap, WallTileMap;
    [SerializeField]
    private TileBase FloorTile, WallTop;



    public void PaintFloorTiles(IEnumerable<Vector2Int> FloorPos)
    {
        PaintTiles(FloorPos, FloorTileMap, FloorTile);  
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap TileMap, TileBase Tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(TileMap,Tile,position);
        }
    }

    internal void PaintSingleWall(Vector2Int position)
    {
        PaintSingleTile(WallTileMap, WallTop,position);
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition,tile);
    }

    public void clear()
    {
        FloorTileMap.ClearAllTiles();
        WallTileMap.ClearAllTiles();
    }

}
