using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Manager.Quiz {

    //퀴즈 데이터
    [CreateAssetMenu(fileName = "Quiz", menuName = "custom/quiz data")]
    public class QuizData : ScriptableObject, IQuizDataAcquireable {
        [Header("문제")] public string Problem;
        [Header("정답")] public string Answer;
        [Header("오답")] public List<string> WrongAnswers;
        /// <summary>
        /// 문제 텍스트
        /// </summary>
        public string ProblemText => Problem;
        /// <summary>
        /// 정답 텍스트
        /// </summary>
        public string GetCorrectAnswer => Answer;
        /// <summary>
        /// 오답 텍스트
        /// </summary>
        /// <param name="index">인덱스</param>
        /// <returns></returns>
        public string GetWrongAnswer(int index) => WrongAnswers[Mathf.Clamp(index, 0, WrongAnswers.Count - 1)];
    }

}