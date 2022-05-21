using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private PokemonBase _base;
    [SerializeField] private int level;
    [SerializeField] private bool isPlayerUnit;

    private Image image;
    private Vector3 origalPos;
    private Color origalColor;
    
    public Pokemon Pokemon { get; set; }

    private void Awake()
    {
        image = GetComponent<Image>();
        origalPos = image.transform.localPosition;
        origalColor = image.color;
    }

    public void SetUp()
    {
        Pokemon = new Pokemon(_base, level);
        if (isPlayerUnit)
        {
            image.sprite = Pokemon.Base.BackSprite;
        }
        else
        {
            image.sprite = Pokemon.Base.FontSprite;
        }
        PlayEnterAnimation();
        
    }

    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
        {
            image.transform.localPosition = new Vector3(-1500, origalPos.y);
        }
        else
        {
            image.transform.localPosition = new Vector3(1500, origalPos.y);
        }

        image.transform.DOLocalMoveX(origalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
        {
            sequence.Append(image.transform.DOLocalMoveX(origalPos.x + 150f, 0.25f));    
        }
        else
        {
            sequence.Append(image.transform.DOLocalMoveX(origalPos.x - 150f, 0.25f));
        }

        sequence.Append(image.transform.DOLocalMoveX(origalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(origalColor, 0.1f));

    }
    public void PLayFaintedAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(origalPos.x - 100f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
