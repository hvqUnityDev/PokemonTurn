using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text levelText;
    [SerializeField] private HPBar hpBar;

    Pokemon _pokemon;
    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;
        nameText.text = pokemon.Base.Name;
        levelText.text = "Lvl : " + pokemon.Level;
        hpBar.SetHP((float)pokemon.HP/pokemon.MaxHP);
    }

    public IEnumerator UpdateHp()
    {
        yield return hpBar.SetHPSmooth((float)_pokemon.HP/(float)_pokemon.MaxHP);
    }
}
