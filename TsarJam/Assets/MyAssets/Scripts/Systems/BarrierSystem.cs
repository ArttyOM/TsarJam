using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GObjAllocator;

public static class BarrierSystem 
{
    //public static TilemapCollider2D tilemapCollider; в аллокаторе
    public static bool IsBlocked(Vector3Int pos)
    {
        Vector3 point = grid.GetCellCenterLocal(pos);
        bool result = tilemapCollider.OverlapPoint(point);
        return result;
    }
}
