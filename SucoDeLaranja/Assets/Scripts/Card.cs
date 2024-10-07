using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {

    public CardInstance CardInstance { get => _cardState; }
    public int CardId { get => _cardState.cardData.id; }

    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Renderer _cardBackground;

    private CardInstance _cardState;
    //private Action OnChangeCardState;
    private Animator _animator;


    private void OnMouseDown() {
        InputController.instance.CardMouseDown(this);
    }


    public void Init(CardInstance cardState) {
        _animator = GetComponent<Animator>();

        _cardState = cardState;
        _cardBackground.material = new Material(_cardBackground.material);
        _cardBackground.material.color = cardState.cardData.color;
        _labelText.text = $"{_cardState.cardData.id}";
        transform.localScale *= CardsManager.cardScaleMultiplier * _cardState.cardData.cellFill;
    }


    public void ShowCard() {
        _animator.SetTrigger("Flip");
        //TODO: call flip sfx
    }
    public void HideCard() {
        _animator.SetTrigger("Flip");
    }

    public void SetState(ICardState newState) {

        _cardState.state?.Exit();

        newState.Enter(this, _cardState.state);
        _cardState.state = newState;
        //OnChangeCardState?.Invoke();
    }
}
