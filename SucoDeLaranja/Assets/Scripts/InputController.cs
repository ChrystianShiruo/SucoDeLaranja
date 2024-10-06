
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour{



    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {//TODO: for testing, remove later
            SceneManager.LoadScene(0);
        }
    }
}