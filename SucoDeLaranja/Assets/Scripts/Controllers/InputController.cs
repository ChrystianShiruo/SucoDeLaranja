
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    public static InputController instance;


    private void Awake() {
        instance = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {//TODO: for testing, remove later
            SceneManager.LoadScene(0);
        }
    }

    public void CardMouseDown(Card card) {

        if(card.CardInstance.state.GetType() == typeof(CardStateFacingDown)) {
            CardsManager.instance.SelectCard(card);
        }
    }

}