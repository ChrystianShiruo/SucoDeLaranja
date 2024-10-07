using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {

    public CardState CardState { get => _cardState; }

    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Renderer _cardBackground;

    private CardState _cardState;

    public void Init(CardState cardState) {
        _cardState = cardState;
        _cardBackground.material = new Material(_cardBackground.material);
        _cardBackground.material.color = cardState.cardData.color;

        _labelText.text = $"{_cardState.cardData.id}";

        transform.localScale *= UIController.cardScaleMultiplier * _cardState.cardData.cellFill;
    }
}
