using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class GridPositionProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    {
        int2 coords = Research4GridAPI.GetCurrentPos(this.transform.localPosition);
        Debug.Log(coords);
        Debug.Log(entity.Index);

        var data = new GridPosition { gridPos = coords};
        dstManager.AddComponentData(entity, data);
        //throw new System.NotImplementedException();
    }
}
