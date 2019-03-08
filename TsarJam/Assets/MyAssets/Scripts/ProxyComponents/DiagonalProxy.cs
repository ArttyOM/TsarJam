using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class DiagonalProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    {
        var data = new Diagonal();
        dstManager.AddComponentData(entity, data);
        //throw new NotImplementedException();
    }

}
