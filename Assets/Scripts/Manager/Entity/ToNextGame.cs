using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager.Entity {

    //보물상자
    public class ToNextGame : InteractableObject {
        /// <summary>
        /// 커서가 올라왔을대
        /// </summary>
        public override void OnMouseOver() //마우스 올라왔을때 행동, QuizButtonInteractable에 설명 있음
        {
            base.OnMouseOver();
            if (Vector3.Distance(Camera.main.transform.position, transform.position) > maxInteractDistance) return;
            if (Input.GetKey(KeyCode.E)) StartCoroutine(NextGameRoutine());
        }
        /// <summary>
        /// 새 게임 시작
        /// </summary>
        /// <returns></returns>
        private IEnumerator NextGameRoutine() //페이드 아웃->게임씬 다시로딩
        {
            Fader.TargetStatus = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        [Header("입자효과")] public ParticleSystem particle;
        [Header("하이라이트")] public GameObject highLight;

        public void Initialize(MainSceneManager manager) {
            if (manager.Particle) {
                particle.Play();
            } else {
                particle.gameObject.SetActive(false);
                highLight.SetActive(true);
            }
        }
    }

}