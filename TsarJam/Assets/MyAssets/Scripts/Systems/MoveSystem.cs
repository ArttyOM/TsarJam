using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using System.Collections.Generic;
/*
//public class MoveSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        ForEach((ref GridPosition gridPosComponent, ref Vertical vertical) =>
//        {
//            Debug.Log(gridPosComponent.gridPos.ToString());
//        });
//    }
//}
*/

public enum MoveType { horisontal, vertical, diagonal}


public static class MoveSystem
{
    #region privateregion

    //private static List<(Transform, bool)> _Verticals = new List<(Transform, bool)>();
    //private static Dictionary<Transform, bool> _Horisontals = new Dictionary<Transform, bool>();
    //private static Dictionary<Transform, bool> _Diagonals = new Dictionary<Transform, bool>();

    /// <summary>
    /// а в конструкторе то ж самое не работает - нужен был вызов любого метода, чтобы статик класс инициализаировался,
    /// а на кой черт тогда конструктор?
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    private static void InitListeners()
    {
        Debug.Log("Listener added");
       // Messenger<MoveType, (Transform, bool)>.AddListener(InstanceEvents.OnAdding.ToString(), OnAdding);
    }
    //статики не могут содержать деструкторы и живут все время выполнения программы, так что усе чики-пики 
    // RemoveListener нам не нужен

    private static void OnAdding(MoveType moveType, (Transform,bool) transformAndDirection)
    {
        switch (moveType)
        {
            case MoveType.vertical: _Verticals.Add(transformAndDirection);
                break;
            //case MoveType.horisontal: _Horisontals.Add(transform, isPositiveMovement);
            //    break;
            //case MoveType.diagonal: _Diagonals.Add(transform, isPositiveMovement);
             //   break;
            //default:
        }
    }
    #endregion private region

    #region public region

    public static Grid grid;

    public static void MoveNext()
    {
        if (!grid) grid = GameObject.FindObjectOfType<Grid>(); ///если в игре будет больше одной сетки, возникнут проблемы.

        Vector3Int currPos;
        
       // Debug.Log("Verticals: " + _Verticals.Count);

        for (int i=0; i<_Verticals.Count; i++)
        {
            currPos = grid.LocalToCell(_Verticals[i].Item1.localPosition);

            if (_Verticals[i].Item2)
                currPos.x += 1;
            else
                currPos.x -= 1;
            //да-да, 0х в координатном пространтсве моего grid соответствует 0y в мировом 

            if (currPos.x == InGameSetting.GridSize.x)
            {
                _Verticals[i] = (_Verticals[i].Item1 ,false);
            }

            if (currPos.x == -InGameSetting.GridSize.x)
            {
                _Verticals[i] = (_Verticals[i].Item1, true);
            }

            _Verticals[i].Item1.localPosition = grid.GetCellCenterLocal(currPos);
            
            //Debug.Log("ver" + currPos);
        }

        /*
        foreach (KeyValuePair<Transform, bool> keyValue in _Horisontals)
        {
            currPos = grid.LocalToCell(keyValue.Key.localPosition);

            //if (currPos.y = InGameSetting.GridSize.y)
            currPos.y += 1;

            keyValue.Key.localPosition = grid.GetCellCenterLocal(currPos);
            //Debug.Log("hor" + currPos);
        }

        foreach (KeyValuePair<Transform, bool> keyValue in _Diagonals)
        {
            currPos = grid.LocalToCell(keyValue.Key.localPosition);
           // Debug.Log("diag" + currPos);

            currPos.y += 1;
            if (currPos.y %2==0)currPos.x += 1;
            keyValue.Key.localPosition = grid.GetCellCenterLocal(currPos);
        }
        */
    }
    #endregion public region
}


