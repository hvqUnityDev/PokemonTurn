using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create New Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private PokemonType type;
    [SerializeField] private int power;
    [SerializeField] private int accurracy;
    [SerializeField] private int pp;


    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accurracy
    {
        get { return accurracy; }
    }

    public int Pp
    {
        get { return pp; }
    }
}
