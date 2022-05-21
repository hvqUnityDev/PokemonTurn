using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleHud playerHud;
    [SerializeField] private BattleUnit playerUnit;
    [SerializeField] private BattleHud enemyHud;
    [SerializeField] private BattleUnit enemyUnit;
    [SerializeField] private BattleDialogBox dialogBox;

    BattleState state;
    int currentAction = 0;
    int currentMove = 0;
    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);
        enemyHud.SetData(enemyUnit.Pokemon);

        dialogBox.SetMoveName(playerUnit.Pokemon.Moves);
        
        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared!");

        PlayerAction();
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy; 
        
        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} used {move.Base.Name}");
        
        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        
        enemyUnit.PlayHitAnimation();
        var damageDetails = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
        yield return enemyHud.UpdateHp();
        yield return ShowDamageDetails(damageDetails);
        
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} is Fainted!");
            enemyUnit.PLayFaintedAnimation();
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Pokemon.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} used {move.Base.Name}");
        
        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        
        playerUnit.PlayHitAnimation();
        var damageDetails = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon);
        yield return playerHud.UpdateHp();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} is Fainted!");
            playerUnit.PLayFaintedAnimation();
        }
        else
        {
            PlayerAction();
        }
    }


    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog($"A critical hit!");
        }

        if (damageDetails.Type > 1f)
        {
            yield return dialogBox.TypeDialog($"It's super Effective!");
        }
        else
        {
            yield return dialogBox.TypeDialog($"It's not very Effective!");
        }
    }
    public void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action."));
        dialogBox.EnableActionSelected(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableDialogText(false);
        dialogBox.EnableActionSelected(false);
        dialogBox.EnableMoveSelected(true);
    }
    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    private void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
            {
                ++currentAction;
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
        }
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                // Fight
                PlayerMove();
            }
            else if(currentAction == 1)
            {
                // Run
            }
        }
    }

    public void HandleMoveSelection()
    {
        //Debug.Log(currentMove);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
            {
                ++currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0 )
            {
                --currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1 && currentMove < playerUnit.Pokemon.Moves.Count - 2)
            {
                currentMove -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 2)
            {
                currentMove += 2;
            }
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableDialogText(true);
            dialogBox.EnableMoveSelected(false);
            StartCoroutine(PerformPlayerMove());
        }
    }

    
}
