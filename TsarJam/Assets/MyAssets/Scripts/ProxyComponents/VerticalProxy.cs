using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class VerticalProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    {
        var data = new Vertical();
        dstManager.AddComponentData(entity, data);
        //throw new System.NotImplementedException();
    }
}
