using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class DiagonalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{
    private bool isPositiveMovement = true;

    private void Start()
    {
        //Messenger<MoveType, Transform, bool>.Broadcast
        //    (InstanceEvents.OnAdding.ToString(), 
        //    MoveType.diagonal, 
        //    this.transform,
        //    isPositiveMovement);
    }

    //public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    //{
    //    var data = new Diagonal();
    //    dstManager.AddComponentData(entity, data);
    //    //throw new NotImplementedException();
    //}
}
