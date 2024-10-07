
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    public static InputController instance;

    public Card _selectedCard;

    private void Awake() {
        instance = this;
        _selectedCard = null;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {//TODO: for testing, remove later
            SceneManager.LoadScene(0);
        }
    }

    public void CardMouseDown(Card card) {

        if(card.CardInstance.state.GetType() == typeof(CardStateFacingDown)) {
            SelectCard(card);
        }
    }

    private void SelectCard(Card card) {
        card.SetState(newState: new CardStateSelected());

        if(_selectedCard == null) {
            _selectedCard = card;
            Debug.Log("Selected first card");

            return;
        }
        //++ turns
        //compare if another card already selected        
        if(card.CardId == _selectedCard.CardId) {
            //paired! +matches; +score; +combo
            card.SetState(new CardStatePaired());
            _selectedCard.SetState(new CardStatePaired());
            Debug.Log("Paired!!");
        } else {
            //not paired; reset combo; flip cards to back again
            card.SetState(new CardStateFacingDown());
            _selectedCard.SetState(new CardStateFacingDown());
            Debug.Log("Not paired!!");
        }

        _selectedCard = null;
    }
}