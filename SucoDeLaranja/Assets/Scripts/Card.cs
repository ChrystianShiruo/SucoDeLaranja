using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {

    public CardInstance CardInstance { get => _cardState; }
    public int CardId { get => _cardState.cardData.id; }
    public Animator Animator { get => _animator; }

    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Renderer _cardBackground;

    private CardInstance _cardState;
    //private Action OnChangeCardState;
    private Animator _animator;
    private List<ICardState> _stateRoutineQueue;

    private void OnMouseDown() {
        InputController.instance.CardMouseDown(this);
    }


    public void Init(CardInstance cardState) {
        _stateRoutineQueue = new List<ICardState>();
        _animator = GetComponent<Animator>();
        StartCoroutine(StateMachineRoutine());
        _cardState = cardState;
        _cardBackground.material = new Material(_cardBackground.material);
        _cardBackground.material.color = cardState.cardData.color;
        _labelText.text = $"{_cardState.cardData.id}";
        transform.localScale *= CardsManager.cardScaleMultiplier * _cardState.cardData.cellFill;
    }


    public void ShowCard() {
        _animator.SetBool("Show", true);
        //TODO: call flip sfx
    }
    public void HideCard() {
        _animator.SetBool("Show", false);
    }

    //FSM
    public void SetState(ICardState newState) {
        _stateRoutineQueue.Add(newState);
    }

    private IEnumerator StateMachineRoutine() {
        while(true) {
            if(_stateRoutineQueue.Count > 0) {
                _cardState.state?.Exit();

                _stateRoutineQueue[0].Enter(this, _cardState.state);

                yield return _stateRoutineQueue[0].Execute();

                _cardState.state = _stateRoutineQueue[0];
                _stateRoutineQueue.RemoveAt(0);
            } else {
                yield return null;
            }
        }
    }

}
