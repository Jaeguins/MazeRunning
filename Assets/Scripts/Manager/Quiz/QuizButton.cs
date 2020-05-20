using UnityEngine;

namespace Scripts.Manager.Quiz {

    //개념적 퀴즈버튼
    public class QuizButton : MonoBehaviour {
        /// <summary>
        /// 관리자
        /// </summary>
        private Quiz manager;
        /// <summary>
        /// 벽 앞면,뒷면 해당버튼
        /// </summary>
        [Header("실작동 부분")] [SerializeField] private QuizButtonInteractable frontTooltip,
                                                                           backTooltip;
        public bool IsCorrect; //정답여부
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="quiz">퀴즈</param>
        /// <param name="value">지문</param>
        /// <param name="isCorrect">정답여부</param>
        public void Initialize(Quiz quiz, string value, bool isCorrect) {
            manager = quiz;
            frontTooltip.Initialize(this, value); //실질 버튼 초기화
            backTooltip.Initialize(this, value);
            IsCorrect = isCorrect;
        }
        /// <summary>
        /// 버튼 클릭 시 행동
        /// </summary>
        public void Click() {
            manager.Solve(IsCorrect); //풀이 시도
        }
    }

}