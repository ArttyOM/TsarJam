using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Research4GridAPI : MonoBehaviour
{
    public static int2 GetCurrentPos(Vector3 localPos)
    {
        Vector3Int tmp = grid.LocalToCell(localPos);

        int2 result;
        result.x = tmp.x;
        result.y = tmp.y;

        return result;
    }

    public static Grid grid;

    private void Awake()
    {
        if (!grid) grid = GameObject.FindObjectOfType<Grid>(); ///если в игре будет больше одной сетки, возникнут проблемы.
    }


    public Vector2 direction;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        pos.z = 0;
    //        Debug.Log("Position is "+pos + ", gridCoords is "+grid.LocalToCell(pos));
    //    }
    //}
}
