using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class DiagonalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{ 

    private void Start()
    {
        Figure tupple = new Figure
        {
            moveType = MoveType.diagonal,
            transform = this.transform,
            direction = 0
        };

        Messenger<Figure>.Broadcast
            (InstanceEvents.OnAdding.ToString(),
            tupple
            );
    }

    //public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    //{
    //    var data = new Diagonal();
    //    dstManager.AddComponentData(entity, data);
    //    //throw new NotImplementedException();
    //}
}
