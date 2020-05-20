using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Manager.Quiz
{
    [CreateAssetMenu(fileName = "Quiz", menuName = "custom/quiz data")]
    public class QuizData : ScriptableObject, IQuizDataAcquireable
    {
        public string Problem, Answer;
        public List<string> WrongAnswers;
        public string ProblemText => Problem;
        public string GetCorrectAnswer => Answer;

        public string GetWrongAnswer(int index) => WrongAnswers[Mathf.Clamp(index,0,WrongAnswers.Count-1)];
    }
}