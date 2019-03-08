using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Entities;
using Unity.Mathematics;


[Serializable]
public struct GridPosition: IComponentData 
{
    public int2 gridPos;
}
