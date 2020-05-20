using System.Collections;
using System.Collections.Generic;
using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Quiz {

    //퀴즈
    public class Quiz : MonoBehaviour {
        /// <summary>
        /// 관리자
        /// </summary>
        private MainSceneManager manager;
        /// <summary>
        /// 퀴즈번호, 정답번호
        /// </summary>
        private int quizIndex,
                    correctAnswerIndex;
        [Header("버튼들")] [SerializeField] private List<QuizButton> buttons;
        [Header("벽 모델")] [SerializeField] private GameObject wallModel;
        [Header("파괴 이펙트")] [SerializeField] private ParticleSystem effect;
        [Header("앞뒤 지문")] [SerializeField] private Text frontText,
                                                        backText;
        [Header("파괴되는시간")] [SerializeField] private float clearTime = 2f;
        [Header("마법진 색 변경")] [SerializeField] private Color clearColor = new Color(.5f, 0, 1);

        /// <summary>
        /// 버튼 번호 당지문 번호
        /// </summary>
        private int[] wrongIndex;
        /// <summary>
        /// 퀴즈 데이터
        /// </summary>
        private QuizData data => manager.WallPoolManager.quizData[quizIndex];
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="manager"></param>
        public void Initialize(MainSceneManager manager) {
            this.manager = manager;

#region 문제 생성

            quizIndex = (int) (Random.value * manager.WallPoolManager.quizData.Count); //퀴즈 번호 뽑기
            correctAnswerIndex = (int) (Random.value * buttons.Count); //랜덤한 위치에 정답 위치
            wrongIndex = new int[(buttons.Count < data.WrongAnswers.Count + 1) ? buttons.Count : data.WrongAnswers.Count + 1]; //문제수와 오답수 중 더 작은걸로 지문 풀 형성
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < wrongIndex.Length; i++) //겹치지 않게 랜덤하게 지정
            {
                if (i == correctAnswerIndex) continue; //정답이면 패쓰
                int tmp = (int) (Random.value * data.WrongAnswers.Count);
                while (set.Contains(tmp)) tmp = (int) (Random.value * data.WrongAnswers.Count);
                set.Add(tmp);
                wrongIndex[i] = tmp;
            }

#endregion

#region 하위 오브젝트에 문제 적용

            for (int i = 0; i < buttons.Count; i++) {
                if (i < wrongIndex.Length) {
                    buttons[i].gameObject.SetActive(true);
                    if (i == correctAnswerIndex)
                        buttons[i].Initialize(this, data.GetCorrectAnswer, true); //정답일때
                    else
                        buttons[i].Initialize(this, data.GetWrongAnswer(wrongIndex[i]), false); //오답일때
                } else
                    buttons[i].gameObject.SetActive(false); //지문이 다떨어졌을때
            }
            frontText.text = data.ProblemText;
            backText.text = data.ProblemText;

#endregion
        }

#region 풀이

        /// <summary>
        /// 풀이 도전
        /// </summary>
        /// <param name="isCorrect"></param>
        public void Solve(bool isCorrect) {
            if (isCorrect)
                StartCoroutine(BreakingRoutine()); //정답
            else {
                StartCoroutine(BackToStart()); //오답
            }
        }
        /// <summary>
        /// 오답시 시작점으로
        /// </summary>
        /// <returns></returns>
        private IEnumerator BackToStart() { //페이드아웃->컨트롤 막기->시작점으로 텔레포트->컨트롤 풀기->페이드 인
            Fader.TargetStatus = true;
            manager.Player.CanMove = false;
            yield return new WaitForSeconds(2f);
            manager.Player.transform.position = manager.MazeManager.nodes[manager.start.x, manager.start.y].NodePos + Vector3.up * .1f;
            manager.Player.CanMove = true;
            Fader.TargetStatus = false;
        }
        /// <summary>
        /// 정답시 벽 파괴
        /// </summary>
        /// <returns></returns>
        private IEnumerator BreakingRoutine() { //그림색 변경->모델 끄기->잔해 이펙트 켜기
            float time = clearTime;
            Image[] images = GetComponentsInChildren<Image>(); //하위 중 그림 전체
            while (time > 0) {
                foreach (Image t in images) {
                    t.color = Color.Lerp(clearColor, Color.black, time * 1.5f / clearTime);
                }
                time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            wallModel.SetActive(false);
            if(manager.Particle)
                effect.Play();
        }

#endregion
    }

}