using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleHud playerHud;
    [SerializeField] private BattleUnit playerUnit;

    private void Start()
    {
        SetUpBattle();
    }

    public void SetUpBattle()
    {
        playerUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);
    }
}
