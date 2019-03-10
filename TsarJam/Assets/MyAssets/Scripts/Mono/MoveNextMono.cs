using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MoveNextMono : MonoBehaviour
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
            MoveNext();
        }
    }


    public Toggle toggle;
    public IEnumerator Start()
    {
        while (true)
        {
            if(toggle.isOn)
            {
                MoveNext();
                yield return new WaitForSeconds(0.5f);
            }

            yield return null;
        }
    }


    public void MoveNext()
    {
        MoveSystem.MoveNext(grid);
        KillSystem.KillBroadcaster();

        DragNDrop.Unlock();
    }

}
