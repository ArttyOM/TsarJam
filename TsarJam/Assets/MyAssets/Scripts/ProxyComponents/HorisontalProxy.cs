using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class HorisontalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{

    private void Start()
    {
        Figure tupple = new Figure
        {
            moveType = MoveType.horisontal,
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
    //    var data = new Horisontal();
    //    dstManager.AddComponentData(entity, data);
    //    //throw new System.NotImplementedException();
    //}
}
