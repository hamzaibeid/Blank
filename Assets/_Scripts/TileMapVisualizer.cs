using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap FloorTileMap, WallTileMap;
    [SerializeField]
    private TileBase FloorTile, WallTop, WallSideRight, WallSideLeft, WallBottom, WallFull,
        WallInnerCornerDownLeft, WallInnerCornerDownRight,
        WallDiagonalCornerDownRight, WallDiagonalCornerDownLeft, WallDiagonalCornerUpRight, WallDiagonalCornerUpLeft;
        



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

    internal void PaintSingleWall(Vector2Int position,string binaryType)
    {
        //Debug.Log(position+" type: " + binaryType);
        int typeAsInt = Convert.ToInt32(binaryType,2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = WallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = WallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = WallSideLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = WallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = WallFull;
        }
        if (tile != null)        
        PaintSingleTile(WallTileMap, tile,position);
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

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = WallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = WallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = WallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = WallBottom;
        }
        if (tile != null)
            PaintSingleTile(WallTileMap,tile,position);
    }
}
