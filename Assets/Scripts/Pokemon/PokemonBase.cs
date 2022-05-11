using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_base_stats_(Generation_VIII-present)

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create New Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField]
    private string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite fontSprite;

    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;
    
    //Base stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDesfense;
    [SerializeField] int speed;

    [SerializeField] private List<LearnableMove> learnableMoves;
    
    
    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }
    
    public int MaxHP
    {
        get { return maxHP; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int SpAttack
    {
        get { return spAttack; }
    }
    public int SpDesfense
    {
        get { return spDesfense; }
    }
    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

    public Sprite FontSprite
    {
        get { return fontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

}
[System.Serializable]
public class LearnableMove
{
    [SerializeField] private MoveBase moveBase;
    [SerializeField] private int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}


public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}
