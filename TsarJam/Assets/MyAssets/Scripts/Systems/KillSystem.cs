using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GObjAllocator;

public static class KillSystem
{
    public static Dictionary<Vector3Int, OnOnePoint> BusyCells = new Dictionary<Vector3Int, OnOnePoint>();
    public static void KillBroadcaster()
    {
        BusyCells = new Dictionary<Vector3Int, OnOnePoint>();

        Vector3Int pos;
        for (int i = 0; i < Figures.Count; i++)
        {
            pos = grid.LocalToCell(Figures[i].transform.localPosition);
            if (!BusyCells.ContainsKey(pos))
            {
                BusyCells.Add(pos, new OnOnePoint());
            }
            switch (Figures[i].moveType)
            {
                case MoveType.diagonal:
                    BusyCells[pos].countOfDiagonals++;
                    break;
                case MoveType.vertical:
                    BusyCells[pos].countOfVerticals++;
                    break;
                case MoveType.horisontal:
                    BusyCells[pos].countOfHorisontals++;
                    break;
            }

        }

        foreach (var onOnePoint in BusyCells)
        {
            if ((onOnePoint.Value.countOfDiagonals>0) && (onOnePoint.Value.countOfHorisontals>0) && (onOnePoint.Value.countOfVerticals>0))
            {
                // grid.CellToLocal(onOnePoint.Key);
                Messenger<Vector3Int, MoveType>.Broadcast("Kill", onOnePoint.Key, MoveType.diagonal);
                Messenger<Vector3Int, MoveType>.Broadcast("Kill", onOnePoint.Key, MoveType.horisontal);
                Messenger<Vector3Int, MoveType>.Broadcast("Kill", onOnePoint.Key, MoveType.vertical);
                break;
            }
            else
            if ((onOnePoint.Value.countOfDiagonals > 0) && (onOnePoint.Value.countOfHorisontals > 0))
            {
                Messenger<Vector3Int, MoveType>.Broadcast("Kill", onOnePoint.Key, MoveType.diagonal);
                break;
            }
            else 
            if ((onOnePoint.Value.countOfDiagonals > 0) && (onOnePoint.Value.countOfVerticals > 0))
            {

                Messenger<Vector3Int, MoveType>.Broadcast("Kill", onOnePoint.Key, MoveType.vertical);
                break;
            }
            else
            if ((onOnePoint.Value.countOfHorisontals > 0) && (onOnePoint.Value.countOfVerticals > 0))
            {
                Messenger<Vector3Int, MoveType>.Broadcast("Kill", onOnePoint.Key, MoveType.horisontal);
                break;
            }
        }

    }

    public class OnOnePoint
    {
        public int countOfVerticals = 0;
    public int countOfHorisontals = 0;
    public int countOfDiagonals = 0;
    }
}
