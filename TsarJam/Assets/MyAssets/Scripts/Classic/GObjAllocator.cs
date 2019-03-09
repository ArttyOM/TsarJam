using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GObjAllocator
{
    public static List<Figure> Figures = new List<Figure>();

    [RuntimeInitializeOnLoadMethod]
    private static void InitListeners()
    {
        Debug.Log("Listener added");
         Messenger<Figure>.AddListener(InstanceEvents.OnAdding.ToString(), OnAdding);
    }

    private static void OnAdding(Figure figure)
    {
        Figures.Add(figure);
    }


}

/// <summary>
/// 1) Transform
/// 2) direction==true - обычное направление движения, ==false - противоположное
/// </summary>
public struct Figure
{
    public MoveType moveType;
    public Transform transform;
    public bool direction;
}