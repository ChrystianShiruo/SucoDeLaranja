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
        public void LoadGameScene(int id, Vector2Int layout) {
            StartCoroutine(StartGameRoutine(id, layout));
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
        private IEnumerator StartGameRoutine(int id) {
            yield return LoadRoutine(id);

            yield return null;
            if(!GameController.Instance) {
                yield break;
            }
        }
        private IEnumerator StartGameRoutine(int id, bool loadSave) {

            yield return StartGameRoutine(id);

            if(loadSave) {
                GameController.Instance.LoadGame();
            } else {
                GameController.Instance.NewGame();
            }
        }
        private IEnumerator StartGameRoutine(int id, Vector2Int layout) {

            yield return StartGameRoutine(id);


            GameController.Instance.NewGame(layout);
        }
    }
}
