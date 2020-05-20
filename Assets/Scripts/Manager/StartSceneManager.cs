using System.Collections;
using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager {

    //시작 씬 관리자
    public class StartSceneManager : MonoBehaviour {
        [Header("플레이씬 이름")] public string PlaySceneName;
        [Header("페이드 관리자")] public Fader FadePrefab;
        /// <summary>
        /// 초기화
        /// </summary>
        public void Start() {
            if (Fader.Instance == null) Instantiate(FadePrefab); //페이드 관리자가 없으면 만들기
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //밝아지게
            Fader.TargetStatus = false;
        }
        /// <summary>
        /// 게임 시작
        /// </summary>
        public void StartGame() {
            StartCoroutine(StartRoutine());
        }
        /// <summary>
        /// 나가기
        /// </summary>
        public void ExitGame() => Application.Quit();
        /// <summary>
        /// 게임 시작시 행동방식->페이드아웃 후 게임씬 로드
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartRoutine() {
            Fader.TargetStatus = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(PlaySceneName);
        }
    }

}