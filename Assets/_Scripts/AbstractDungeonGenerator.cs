﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualizer tileMapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero; 


    public void GenerateDungeon()
    {
        tileMapVisualizer.clear();
        RunProcidularGenerator();
    }

    protected abstract void RunProcidularGenerator();
}
