using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {

    public CardInstance CardInstance { get => _cardInstance; }
    public int CardId { get => _cardInstance.cardData.id; }
    public Animator Animator { get => _animator; }

    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Renderer _cardBackground;

    private CardInstance _cardInstance;
    //private Action OnChangeCardState;
    private Animator _animator;
    private List<CardState> _stateRoutineQueue;

    private void OnMouseDown() {
        InputController.instance.CardMouseDown(this);
    }


    public void Init(CardInstance cardState) {
        _stateRoutineQueue = new List<CardState>();
        _animator = GetComponent<Animator>();
        StartCoroutine(StateMachineRoutine());
        _cardInstance = cardState;
        _cardBackground.material = new Material(_cardBackground.material);
        _cardBackground.material.color = cardState.cardData.color;
        _labelText.text = $"{_cardInstance.cardData.id}";
        transform.localScale *= CardsManager.cardScaleMultiplier * _cardInstance.cardData.cellFill;

    }


    public void ShowCard() {
        _animator.SetBool("Show", true);
        //TODO: call flip sfx
    }
    public void HideCard() {
        _animator.SetBool("Show", false);
    }

    //FSM
    public void SetState(CardState newState) {
        _stateRoutineQueue.Add(newState);
    }

    private IEnumerator StateMachineRoutine() {
        while(true) {
            if(_stateRoutineQueue.Count > 0) {
                _cardInstance.state?.Exit();

                _stateRoutineQueue[0].Enter(this, _cardInstance.state);

                yield return _stateRoutineQueue[0].Execute();

                _cardInstance.state = _stateRoutineQueue[0];
                _stateRoutineQueue.RemoveAt(0);
            } else {
                yield return null;
            }
        }
    }

}
