using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {

    public CardState CardState { get => _cardState; }

    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Image _backgroundImage;

    private CardState _cardState;

    public void Init(CardState cardState) {
        _cardState = cardState;
        _backgroundImage.sprite = cardState.cardData.sprite;
        _backgroundImage.color = cardState.cardData.color;
        _labelText.text = $"{_cardState.cardData.id}";
    }
}
