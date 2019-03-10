using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

public class Research4GridAPI : MonoBehaviour
{
    //public static int2 GetCurrentPos(Vector3 localPos)
    //{
    //    Vector3Int tmp = grid.LocalToCell(localPos);

    //    int2 result;
    //    result.x = tmp.x;
    //    result.y = tmp.y;

    //    return result;
    //}

    public static Grid grid = GObjAllocator.grid;
    public static TilemapCollider2D tilemapCollider = GObjAllocator.tilemapCollider;

    private void Awake()
    {
        if (!grid)
        {
            grid = GameObject.FindObjectOfType<Grid>(); ///если в игре будет больше одной сетки, возникнут проблемы.
            GObjAllocator.grid = grid;
        }

        if (!tilemapCollider)
        {
            tilemapCollider = GameObject.FindObjectOfType<TilemapCollider2D>();
            GObjAllocator.tilemapCollider = tilemapCollider;
        }
    }


    //public Vector2 direction;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
           // Debug.Log("Ju");
            MoveSystem.MoveNext(grid);
        }
        //    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    pos.z = 0;
        //    Debug.Log("Position is " + pos + ", gridCoords is " + grid.LocalToCell(pos));
        //}
    }
}
