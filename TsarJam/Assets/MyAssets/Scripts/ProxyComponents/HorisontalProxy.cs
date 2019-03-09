using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class HorisontalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{
    private bool _isPositiveMovement = true;

    private void Start()
    {
        //Messenger<MoveType, Transform, bool>.Broadcast
        //    (InstanceEvents.OnAdding.ToString(), 
        //    MoveType.horisontal, 
        //    this.transform, 
        //    _isPositiveMovement);
    }

    //public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    //{
    //    var data = new Horisontal();
    //    dstManager.AddComponentData(entity, data);
    //    //throw new System.NotImplementedException();
    //}
}
