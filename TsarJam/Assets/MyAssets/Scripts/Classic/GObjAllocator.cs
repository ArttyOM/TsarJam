using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GObjAllocator
{

    public static List<Figure> Figures = new List<Figure>();

    public static Grid grid;
    public static TilemapCollider2D tilemapCollider;

    [RuntimeInitializeOnLoadMethod]
    private static void InitListeners()
    {
        Debug.Log("Listener added");
         Messenger<Figure>.AddListener(InstanceEvents.OnAdding.ToString(), OnAdding);
    }

    private static void OnAdding(Figure figure)
    {        
        Figures.Add(figure);
        Debug.Log("figure added");
    }


}

/// <summary>
/// 1) Transform
/// 2) direction==true - обычное направление движения, ==false - противоположное
/// </summary>
public class Figure
{
    public MoveType moveType;
    public Transform transform;
    public byte direction = 0;
}