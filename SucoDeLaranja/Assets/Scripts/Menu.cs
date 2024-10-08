using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void ClickNewGameButton() {
        SucoDeLaranja.SceneManager.Instance.LoadGameScene((SceneManager.GetActiveScene().buildIndex + 1), false);
    }

    public void ClickLoadButton() {
        SucoDeLaranja.SceneManager.Instance.LoadGameScene((SceneManager.GetActiveScene().buildIndex + 1), true);
    }
}
