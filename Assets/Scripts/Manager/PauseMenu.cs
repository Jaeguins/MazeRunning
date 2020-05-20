using System.Collections;
using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class PauseMenu : MonoBehaviour
    {
        public string StartMenuSceneName;
        public GameObject PauseMenuCanvas;
        public Player Player;
        public void ToStartMenu()
        {
            StartCoroutine(StartMenuRoutine());
        }


        private IEnumerator StartMenuRoutine()
        {
            
            Fader.TargetStatus = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(StartMenuSceneName);
        }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeInHierarchy);
            Cursor.lockState = PauseMenuCanvas.activeInHierarchy
                ? CursorLockMode.None
                : CursorLockMode.Locked;
            Cursor.visible = PauseMenuCanvas.activeInHierarchy;
            Player.CanMove = !PauseMenuCanvas.activeInHierarchy;
        }
    }
}