
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    public static InputController Instance;


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {//TODO: for testing, remove later
            SceneManager.LoadScene(0);
        }
    }

    public void CardMouseDown(Card card) {
        if(GameController.Instance.CurrentState!= GameController.GameState.Playing) {
            return;
        }

        if(card.CardInstance.state.GetType() == typeof(CardStateFacingDown)) {
            CardsManager.Instance.SelectCard(card);
        }
    }

}