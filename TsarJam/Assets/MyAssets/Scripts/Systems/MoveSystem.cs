using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        ForEach((ref GridPosition gridPosComponent, ref Vertical vertical) =>
        {
            Debug.Log(gridPosComponent.gridPos.ToString());
        });
    }
}
