using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using System.Collections.Generic;

using static GObjAllocator;
using static BarrierSystem;

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
    //[RuntimeInitializeOnLoadMethod]
    //private static void InitListeners()
    //{
    //    Debug.Log("Listener added");
    //   // Messenger<MoveType, (Transform, bool)>.AddListener(InstanceEvents.OnAdding.ToString(), OnAdding);
    //}
    //статики не могут содержать деструкторы и живут все время выполнения программы, так что усе чики-пики 
    // RemoveListener нам не нужен

    //private static void OnAdding(MoveType moveType, (Transform,bool) transformAndDirection)
    //{
    //    switch (moveType)
    //    {
    //        case MoveType.vertical: _Verticals.Add(transformAndDirection);
    //            break;
    //        //case MoveType.horisontal: _Horisontals.Add(transform, isPositiveMovement);
    //        //    break;
    //        //case MoveType.diagonal: _Diagonals.Add(transform, isPositiveMovement);
    //         //   break;
    //        //default:
    //    }
    //}

        ///вернет false если на пути CalculateNextPos будет стоять коллайдер
    private static Vector3Int CalculateNextPos( MoveType moveType, Vector3Int pos, byte direction)
    {
        Vector3Int result = pos;
        //byte tmp = direction;

        if (moveType == MoveType.vertical)
        {
            VerticalInternal(ref result, direction);
        }

        if (moveType == MoveType.horisontal)
        {
            HorisontalInternal(ref result, direction);
        }

        if (moveType == MoveType.diagonal)
        {
            DiagonalInternal(ref result, direction);
        }

        return result;

    }

    private static void DiagonalInternal ( ref Vector3Int result, byte direction)
    {

        if ((direction & 0b0011) == 0b0000) //если вправо-вверх
        {
            result.y += 1;
            if (result.y % 2 == 0) result.x += 1;
        }
        else
        if ((direction & 0b0011) == 0b0010) //влево-вверх
        {
            result.y -= 1;
            if (result.y % 2 == 0) result.x += 1;
        }
        else
        if ((direction & 0b0011) == 0b0011) //влево-вниз
        {
            //Debug.Log("result: " + result.x + " " + result.y);

            if (result.y % 2 == 0) result.x -= 1;
            result.y -= 1;
            //Debug.Log("result2: "+ result.x + " " + result.y);
            
        }
        if ((direction & 0b0011) == 0b0001) //вправо-вниз
        {
            if (result.y % 2 == 0) result.x -= 1;
            result.y += 1;
        }
    }

    private static void VerticalInternal( ref Vector3Int result, byte direction)
    {
        if ((direction & 0b0001) == 0b0000)
        { result.x += 1; }
        else
        { result.x -= 1; }
    }

    private static void HorisontalInternal(ref Vector3Int result, byte direction)
    {
        if ((direction & 0b0010) == 0b0000)
        { result.y += 1; }
        else
        { result.y -= 1; }
    }


    private static void ChangeDirection(MoveType moveType, Vector3Int currPos,ref byte direction)
    {

        if (moveType == MoveType.vertical)
        {
            if ((direction & 0b0001) == 0b0001) direction = 0b0000;
            else direction = 0b0001;
        }
        if (moveType == MoveType.horisontal)
        {
            if ((direction & 0b0010) == 0b0010) direction = 0b0000;
            else direction = 0b0010;
        }

        //индусский код детектед
        if (moveType == MoveType.diagonal)
        {
            Vector3Int tmp = currPos;
            
            if ((direction & 0b0011) == 0b00)
            {
                //движемся вправо-вверх.
                //1) отскок влево-вверх
                direction = 0b10;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;
                //2) отскок вниз-вправо
                direction = 0b01;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                //3) отскок влево-вниз
                direction = 0b11;
                
                return;
            }

            if ((direction & 0b0011) == 0b01)
            {
                //движемся вправо-вниз.

                direction = 0b00;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                direction = 0b11;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                direction = 0b0010;

                return;
            }

            if ((direction & 0b0011) == 0b10)
            {
                //движемся влево-вверх.

                direction = 0b0011;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                direction = 0b0000;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                direction = 0b0001;
                 
                return;
            }

            if ((direction & 0b0011) == 0b0011)
            {
                //движемся влево-вниз.

                direction = 0b10;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                direction = 0b01;
                tmp = CalculateNextPos(moveType, currPos, direction);
                if (!IsBlocked(tmp)) return;

                direction = 0b00;

                return;
            }

        }
    }

    #endregion private region

    #region public region


    public static void MoveNext(Grid grid)
    {

        Vector3Int currPos;
        Vector3Int nextPos;

        for (int i = 0; i < Figures.Count; i++)
        {
            //currPos = grid.LocalToCell(GObjAllocator.Figures[i].transform.localPosition);
            currPos = grid.LocalToCell(Figures[i].transform.localPosition);

            nextPos = CalculateNextPos(Figures[i].moveType, currPos, Figures[i].direction);

            if (IsBlocked(nextPos))
             {
                Debug.Log("WayLocked");
                ChangeDirection(Figures[i].moveType, currPos, ref Figures[i].direction);
                nextPos = CalculateNextPos(Figures[i].moveType, currPos, Figures[i].direction);
            }

            Debug.Log("CurrPos: "+currPos+" NextPos: "+ nextPos);

            Figures[i].transform.localPosition = grid.GetCellCenterLocal(nextPos);

            //Debug.Log("Проверка IsBlocked: " +IsBlocked(currPos));
        }
    }
    #endregion public region
}


