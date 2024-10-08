using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour {

    public static Action InteractableWaitQueue;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.interactable = false;
    }
    private void Update() {
        _button.interactable =
            (InteractableWaitQueue == null &&
            GameController.Instance.CurrentState == GameController.GameState.Playing) ? true : false;
    }

}