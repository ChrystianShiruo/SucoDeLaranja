using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour {

    public static Action InteractableWaitQueue;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.interactable = false;
    }
    private void Start() {
        CheckIfDataExists();
        DataManager.OnSaveGameData += CheckIfDataExists;
    }

    private void CheckIfDataExists() {
        _button.interactable = DataManager.SaveExists();
    }

    private void OnDestroy() {
        DataManager.OnSaveGameData -= CheckIfDataExists;
    }
}

