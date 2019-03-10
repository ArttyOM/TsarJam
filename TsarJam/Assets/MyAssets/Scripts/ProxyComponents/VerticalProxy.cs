using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class VerticalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{
    //private bool isPositiveMovement = true;

    private void Start()
    {
        // Debug.Log("Vertical Adding...");
       // (Transform, bool) tupple = (this.transform, true);
        Figure tupple = new Figure {
            moveType = MoveType.vertical,
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
    //    var data = new Vertical();
    //    dstManager.AddComponentData(entity, data);
    //    //throw new System.NotImplementedException();
    //}
}
