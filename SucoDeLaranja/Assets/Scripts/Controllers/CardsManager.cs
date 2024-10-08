﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour {

    public static CardsManager instance;
    public static float cardScaleMultiplier;

    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private RectTransform _boardPanel;

    private GameData _gameData;
    private GridLayoutGroup _boardPanelLayoutGroup;
    private List<Card> _cards;
    private Card _selectedCard;


    public void Init(GameData gameData) {
        ResetBoard();
        _gameData = gameData;
        //_selectedCard = null;
        instance = this;

        _boardPanelLayoutGroup = _boardPanel.GetComponent<GridLayoutGroup>();
        _boardPanelLayoutGroup.constraintCount = _gameData.Board[0].cardArray.Length;

        SetBoardLayout(_boardPanel, new Vector2Int(_gameData.Board.Length, _gameData.Board[0].cardArray.Length));
        InstantiateCards(_gameData.Board);

        _selectedCard = GetSelectedCard();
    }


    public void FlipAllCards() {
        Debug.Log($"FlipAllCards {_cards.Count}");
        _cards.ForEach(card => card.SetState(new CardStateFacingDown()));
    }
    public void LoadAllCardStates() {
        Debug.Log($"LoadAllCardStates {_cards.Count}");
        _cards.ForEach(card => card.LoadState());
    }

    public void SelectCard(Card newCard) {
        newCard.SetState(new CardStateSelected());

        if(_selectedCard == null) {
            _selectedCard = newCard;
            Debug.Log("Selected first card");
            return;
        }
        List<Card> pair = new List<Card>();
        pair.Add(_selectedCard);
        pair.Add(newCard);

        GameController.instance.IncrementTurn();
        //compare if another card already selected        
        if(newCard.CardId == _selectedCard.CardId) {
            GameController.instance.PairScored();
            pair.ForEach(card => card.SetState(new CardStatePaired()));
        } else {
            GameController.instance.PairFailed();
            //pair.ForEach(card => card.SetState(new CardStateFacingDown()));
            StartCoroutine(SyncCardStates(typeof(CardStateFacingDown), pair));
        }

        _selectedCard = null;
    }

    private Card GetSelectedCard() {
        return _cards.Find(card => card.CardInstance.state.StateName == typeof(CardStateSelected).ToString());
    }
    private void ResetBoard() {
        Card[] children = _boardPanel.GetComponentsInChildren<Card>();
        foreach(Card card in children) {
            Destroy(card.gameObject);
        }
    }

    private void InstantiateCards(CardInstanceArray[] board) {

        int width = board.Length;
        int height = board[0].cardArray.Length;
        _cards = new List<Card>();


        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                GameObject go = Instantiate(_cardPrefab, _boardPanel);
                go.GetComponent<Card>().Init(board[x].cardArray[y]);
                _cards.Add(go.GetComponent<Card>());
            }
        }
    }

    private void SetBoardLayout(RectTransform boardPanel, Vector2Int dimensions) {
        float x = boardPanel.rect.width;
        float y = boardPanel.rect.height;
        x = x / dimensions.x;
        y = y / dimensions.y;
        cardScaleMultiplier = (x > y ? y : x) / 100f;
        _boardPanelLayoutGroup.cellSize = new Vector2(x, y);
    }

    private IEnumerator SyncCardStates(Type state, List<Card> cards) {
        Action waitFinish = () => { };
        SaveButton.InteractableWaitQueue += waitFinish;
        //wait frame for animations to start
        yield return null;

        float animationLenght = 0;
        //wait until all cards finished last state
        foreach(Card card in cards) {
            animationLenght = animationLenght > card.Animator.GetCurrentAnimatorStateInfo(0).length ? animationLenght : card.Animator.GetCurrentAnimatorStateInfo(0).length;
            //yield return new WaitWhile(() => card.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f);
        }

        yield return new WaitForSeconds(animationLenght);
        yield return new WaitForSeconds(.5f);
        cards.ForEach(card => card.SetState((CardState)Utils.CreateNewInstance(state)));
        SaveButton.InteractableWaitQueue -= waitFinish;
    }

}