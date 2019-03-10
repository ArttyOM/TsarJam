using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GObjAllocator
{
    public const int WinScore = 15;


    public static List<Figure> Figures = new List<Figure>();

    public static Grid grid;
    public static TilemapCollider2D tilemapCollider;

    [RuntimeInitializeOnLoadMethod]
    private static void InitListeners()
    {
        // Debug.Log("Listeners added");
        Messenger<Figure>.AddListener(InstanceEvents.OnAdding.ToString(), OnAdding);

        Messenger <Figure>.AddListener(InstanceEvents.OnDeath.ToString(), OnDeath);
    }

    private static void OnAdding(Figure figure)
    {        
        Figures.Add(figure);
        KillSystem.KillBroadcaster();
        //Debug.Log("figure added");
    }

    private static void OnDeath(Figure figure)
    {
        //Debug.Log("remove: "+ figure.transform.name);

        Figures.Remove(figure);
    }

}

/// <summary>
/// 0) MoveType -тип фигуры
/// 1) Transform
/// 2) direction==true - обычное направление движения, ==false - противоположное
/// </summary>
public class Figure
{
    public MoveType moveType;
    public Transform transform;
    public byte direction = 0;
}