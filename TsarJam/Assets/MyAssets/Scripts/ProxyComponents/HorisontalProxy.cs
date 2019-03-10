using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

using static GObjAllocator;

public class HorisontalProxy : MonoBehaviour//, IConvertGameObjectToEntity
{
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
        if ((moveType == MoveType.horisontal) && (pos.Equals( grid.LocalToCell(this.transform.localPosition))))
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
         tupple = new Figure
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
