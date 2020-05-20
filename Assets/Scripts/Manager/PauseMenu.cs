using System.Collections;
using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager {

    //일시정지 메뉴
    public class PauseMenu : MonoBehaviour {
        [Header("메인화면 씬 이름")] public string StartMenuSceneName;
        [Header("캔버스 오브젝트")] public GameObject PauseMenuCanvas;
        [Header("플레이어")] public Player Player;
        /// <summary>
        /// 메인메뉴로 복귀
        /// </summary>
        public void ToStartMenu() {
            StartCoroutine(StartMenuRoutine());
        }

        /// <summary>
        /// 메인메뉴로 복귀시 행동 -> 페이드 아웃 후 메인메뉴 씬 로드
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartMenuRoutine() {
            Fader.TargetStatus = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(StartMenuSceneName);
        }
        /// <summary>
        /// ESC 입력 감지 후 커서,메뉴,플레이어 이동여부 설정
        /// </summary>
        public void Update() {
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