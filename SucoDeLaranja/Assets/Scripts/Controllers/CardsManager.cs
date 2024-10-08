using System;
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
        _gameData = gameData;
        _selectedCard = null;
        instance = this;

        _boardPanelLayoutGroup = _boardPanel.GetComponent<GridLayoutGroup>();
        _boardPanelLayoutGroup.constraintCount = _gameData.Board[0].cardArray.Length;

        SetBoardLayout(_boardPanel, new Vector2Int(_gameData.Board.Length, _gameData.Board[0].cardArray.Length));
        InstantiateCards(_gameData.Board);

    }

    public void FlipAllCards() {
        _cards.ForEach(card => card.SetState(new CardStateFacingDown()));
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
            StartCoroutine(SyncCardStates(typeof(CardStatePaired), pair));
        } else {
            GameController.instance.PairFailed();            
            StartCoroutine(SyncCardStates(typeof(CardStateFacingDown), pair));
        }

        _selectedCard = null;
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
        Debug.Log($"{x}, {y}");
        x = x / dimensions.x;
        y = y / dimensions.y;
        cardScaleMultiplier = (x > y ? y : x) / 100f;
        _boardPanelLayoutGroup.cellSize = new Vector2(x, y);
    }

    //TODO: not working properly, redo caching if animation ended on Card class
    private IEnumerator SyncCardStates(Type state, List<Card> cards) {

        //wait frame for animations to start
        yield return null;

        //wait until all cards finished last state
        foreach(Card card in cards) {
            yield return new WaitWhile(() => card.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f);
        }

        yield return new WaitForSeconds(1f);
        cards.ForEach(card => card.SetState((CardState)Utils.GetInstance(state)));

    }

}