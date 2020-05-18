using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Quiz
{
    public class QuizButtonInteractable : InteractableObject
    {
        private QuizButton Button;
        private Text text;
        public void Initialize(QuizButton btn, string value)
        {
            Button = btn;
            text = GetComponentInChildren<Text>();
            text.text = value;
        }
        public override void OnMouseOver()
        {
            base.OnMouseOver();
            if (Vector3.Distance(Camera.main.transform.position, transform.position) > maxInteractDistance) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Button.Click();
            }
        }
    }
}