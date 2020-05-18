using System.Collections;
using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class StartSceneManager:MonoBehaviour
    {
        public string PlaySceneName;
        public Fader FadePrefab;
        public void Start()
        {
            if (Fader.Instance == null) Instantiate(FadePrefab);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Fader.TargetStatus = false;
        }
        public void StartGame()
        {
            StartCoroutine(StartRoutine());
            
        }

        public void ExitGame() => Application.Quit();

        private IEnumerator StartRoutine()
        {
            Fader.TargetStatus = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(PlaySceneName);
        }
    }
}
