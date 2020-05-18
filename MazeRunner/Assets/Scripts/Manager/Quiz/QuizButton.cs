using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Quiz
{
    public class QuizButton:MonoBehaviour
    {
        private Quiz manager;
        [SerializeField] private QuizButtonInteractable frontTooltip,backTooltip;
        public bool IsCorrect;
        public void Initialize(Quiz quiz,string value,bool isCorrect)
        {
            manager = quiz;
            frontTooltip.Initialize(this,value);
            backTooltip.Initialize(this,value);
            IsCorrect = isCorrect;
            
        }

        public void Click()
        {
            manager.Solve(IsCorrect);
        }
    }
}