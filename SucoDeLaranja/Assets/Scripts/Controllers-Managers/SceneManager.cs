using UnityEngine;
using System.Collections;
using System;


namespace SucoDeLaranja {
    public class SceneManager : MonoBehaviour {

        public static SceneManager Instance;

        private void Awake() {
            if(Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void LoadGameScene(int id, bool loadSave) {
            StartCoroutine(StartGameRoutine(id, loadSave));
        }
        public void LoadScene(int id) {
            StartCoroutine(LoadRoutine(id));
        }

        private IEnumerator LoadRoutine(int id) {
            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(id);
            while(!asyncLoad.isDone) {
                yield return null;
            }
        }

        private IEnumerator StartGameRoutine(int id, bool loadSave) {

            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(id);
            while(!asyncLoad.isDone) {
                yield return null;
            }

            yield return null;
            if(!GameController.Instance) {
                yield break;
            }
            if(loadSave) {
                GameController.Instance.LoadGame();
            } else {
                GameController.Instance.NewGame();
            }


        }
    }
}
