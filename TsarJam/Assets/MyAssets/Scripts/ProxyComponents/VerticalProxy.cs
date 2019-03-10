using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using static GObjAllocator;

public class VerticalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{
    //private bool isPositiveMovement = true;
    Figure tupple;

    private void Awake()
    {
        Messenger<Vector3Int, MoveType>.AddListener("Kill", Death);
    }

    private void OnDestroy()
    {
        Messenger<Figure>.Broadcast(InstanceEvents.OnDeath.ToString(), tupple);

        Messenger<Vector3Int, MoveType>.RemoveListener("Kill", Death);
    }

    void Death(Vector3Int pos, MoveType moveType)
    {

        if ((moveType == MoveType.vertical)&& (pos.Equals(grid.LocalToCell(this.transform.localPosition))))
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // Debug.Log("Vertical Adding...");
       // (Transform, bool) tupple = (this.transform, true);
        tupple = new Figure {
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
